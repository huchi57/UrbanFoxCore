using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UrbanFox
{
    public static class GeometryExtensions
    {
        /// <summary>
        /// Clamp a vector between two vectors. Angles are calculated in acute angles (i.e. angles that are no greater than 180 degrees).
        /// </summary>
        public static Vector3 ClampBetween(this Vector3 vector, Vector3 rangeA, Vector3 rangeB)
        {
            // Cache vector magnitude
            var targetMagnitude = vector.magnitude;

            // Normalize vectors
            rangeA = Vector3.ClampMagnitude(rangeA, 1);
            rangeB = Vector3.ClampMagnitude(rangeB, 1);
            var normalAB = Vector3.Cross(rangeA, rangeB);
            vector = Vector3.ClampMagnitude(Vector3.ProjectOnPlane(vector, normalAB), 1);

            // Get angle between range vectors
            var angleAB = Vector3.Angle(rangeA, rangeB);
            var angleA = Vector3.Angle(rangeA, vector);
            var angleB = Vector3.Angle(rangeB, vector);

            // Check if the vector is in the acute angle part of the two range vectors
            // HACK: Workaround floating points
            if (Mathf.FloorToInt(angleA) + Mathf.FloorToInt(angleB) <= Mathf.CeilToInt(angleAB))
            {
                if (angleA < angleAB && angleB < angleAB)
                {
                    return targetMagnitude * vector;
                }
            }

            return angleA > angleB ? targetMagnitude * rangeB : targetMagnitude * rangeA;
        }

        public static bool Is2DPointInTriangle(this Vector2 point, Vector2 triangleVertex0, Vector2 triangleVertex1, Vector2 triangleVertex2)
        {
            if (!IsSameSide(triangleVertex0, triangleVertex2, point, triangleVertex1))
            {
                return false;
            }
            if (!IsSameSide(triangleVertex2, triangleVertex1, point, triangleVertex0))
            {
                return false;
            }
            if (!IsSameSide(triangleVertex1, triangleVertex0, point, triangleVertex2))
            {
                return false;
            }
            return true;

            static bool IsSameSide(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
            {
                return ((a.y - b.y) * (c.x - a.x) + (b.x - a.x) * (c.y - a.y)) * ((a.y - b.y) * (d.x - a.x) + (b.x - a.x) * (d.y - a.y)) < 0;
            }
        }

        public static bool Is2DPointInTriangle(this Vector2 point, params Vector2[] triangleVertices)
        {
            if (triangleVertices.IsNullOrEmpty() || triangleVertices.Length != 3)
            {
                return false;
            }
            return Is2DPointInTriangle(point, triangleVertices[0], triangleVertices[1], triangleVertices[2]);
        }

        public static bool Is2DPointInTriangle(this Vector2 point, IList<Vector2> triangleVertices)
        {
            if (triangleVertices.IsNullOrEmpty() || triangleVertices.Count != 3)
            {
                return false;
            }
            return Is2DPointInTriangle(point, triangleVertices[0], triangleVertices[1], triangleVertices[2]);
        }

        public static bool Is2DPointInConvexPolygon(this Vector2 point, params Vector2[] vertices)
        {
            if (vertices == null || vertices.Length == 0)
            {
                return false;
            }

            // Special case for 1 point.
            if (vertices.Length == 1)
            {
                return point.IsApproximately(vertices[0]);
            }

            // Special case for 2 points (a line).
            if (vertices.Length == 2)
            {
                return (Vector2.Distance(point, vertices[0]) + Vector2.Distance(point, vertices[1])).IsApproximately(Vector2.Distance(vertices[0], vertices[1]));
            }

            // Special case for 3 points (a triangle).
            if (vertices.Length == 3)
            {
                return Is2DPointInTriangle(point, vertices[0], vertices[1], vertices[2]);
            }

            // General case (shapes with 4 or more vertices can be break down into different triangles).
            for (int i = 0; i < vertices.Length - 2; i++)
            {
                for (int j = i + 1; j < vertices.Length - 1; j++)
                {
                    for (int k = j + 1; k < vertices.Length; k++)
                    {
                        if (point.Is2DPointInTriangle(vertices[i], vertices[j], vertices[k]))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool Is2DPointInConvexPolygon(this Vector2 point, IList<Vector2> vertices)
        {
            if (vertices.IsNullOrEmpty())
            {
                return false;
            }
            return Is2DPointInTriangle(point, vertices.ToArray());
        }

        public static bool IsPointInCylinder(this Vector3 point, Vector3 surface1Center, Vector3 surface2Center, float cylinderRadius)
        {
            var pointProjectedOnCylinderAxis = surface1Center + Vector3.Project(point - surface1Center, surface2Center - surface1Center);
            if (Vector3.Dot(pointProjectedOnCylinderAxis - surface1Center, surface2Center - surface1Center) <= 0)
            {
                return false;
            }
            if (Vector3.Dot(pointProjectedOnCylinderAxis - surface2Center, surface1Center - surface2Center) <= 0)
            {
                return false;
            }
            return Vector3.Distance(pointProjectedOnCylinderAxis, point) < cylinderRadius;
        }

        public static bool IsPointInTetrahedron(this Vector3 point, Vector3 shapeVertex0, Vector3 shapeVertex1, Vector3 shapeVertex2, Vector3 shapeVertex3)
        {
            // A tetrahedron (triangular pyramid) is consisted with 4 triangles.
            var plane = new Plane(shapeVertex0, shapeVertex1, shapeVertex2);
            if (!plane.SameSide(point, shapeVertex3))
            {
                return false;
            }
            plane = new Plane(shapeVertex0, shapeVertex1, shapeVertex3);
            if (!plane.SameSide(point, shapeVertex2))
            {
                return false;
            }
            plane = new Plane(shapeVertex0, shapeVertex2, shapeVertex3);
            if (!plane.SameSide(point, shapeVertex1))
            {
                return false;
            }
            plane = new Plane(shapeVertex1, shapeVertex2, shapeVertex3);
            if (!plane.SameSide(point, shapeVertex0))
            {
                return false;
            }

            // Special case, tetrahedron height = 0: it is essentially a plane.
            if (Vector3.Distance(shapeVertex0, plane.ClosestPointOnPlane(shapeVertex0)).IsApproximatelyZero())
            {
                return false;
            }
            return true;
        }

        public static bool IsPointInTetrahedron(this Vector3 point, params Vector3[] shapeVertices)
        {
            if (shapeVertices.IsNullOrEmpty() || shapeVertices.Length != 4)
            {
                return false;
            }
            return point.IsPointInTetrahedron(shapeVertices[0], shapeVertices[1], shapeVertices[2], shapeVertices[3]);
        }

        public static bool IsPointInTetrahedron(this Vector3 point, IList<Vector3> shapeVertices)
        {
            if (shapeVertices.IsNullOrEmpty() || shapeVertices.Count != 4)
            {
                return false;
            }
            return point.IsPointInTetrahedron(shapeVertices[0], shapeVertices[1], shapeVertices[2], shapeVertices[3]);
        }

        public static bool IsPointInConvexPolygon(this Vector3 point, params Vector3[] shapeVertices)
        {
            if (shapeVertices.IsNullOrEmpty())
            {
                return false;
            }

            // Special case for 1 point.
            if (shapeVertices.Length == 1)
            {
                return Vector3.Distance(point, shapeVertices[0]).IsApproximatelyZero();
            }

            // Special case for 2 points (a line).
            if (shapeVertices.Length == 2)
            {
                var pointProjectedOnLine = shapeVertices[0] + Vector3.Project(point - shapeVertices[0], shapeVertices[1] - shapeVertices[0]);
                return Vector3.Distance(point, pointProjectedOnLine).IsApproximatelyZero();
            }

            // Special case for 3 points (a plane).
            if (shapeVertices.Length == 3)
            {
                var plane = new Plane(shapeVertices[0], shapeVertices[1], shapeVertices[2]);
                return Vector3.Distance(point, plane.ClosestPointOnPlane(point)).IsApproximatelyZero();
            }

            // Base case (a tetrahedron).
            if (shapeVertices.Length == 4)
            {
                return point.IsPointInTetrahedron(shapeVertices);
            }

            // General case (shapes with 5 or more vertices can be break down into different tetrahedrons).
            for (int i = 0; i < shapeVertices.Length - 3; i++)
            {
                for (int j = i + 1; j < shapeVertices.Length - 2; j++)
                {
                    for (int k = j + 1; k < shapeVertices.Length - 1; k++)
                    {
                        for (int l = k + 1; l < shapeVertices.Length; l++)
                        {
                            if (point.IsPointInTetrahedron(shapeVertices[i], shapeVertices[j], shapeVertices[k], shapeVertices[l]))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static bool IsPointInConvexPolygon(this Vector3 point, IList<Vector3> shapeVertices)
        {
            if (shapeVertices.IsNullOrEmpty())
            {
                return false;
            }
            return point.IsPointInConvexPolygon(shapeVertices.ToArray());
        }
    }
}
