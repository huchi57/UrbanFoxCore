using System;
using UnityEngine;

namespace UrbanFox
{
    public static class GizmosExtensions
    {
        public struct MatrixScope : IDisposable
        {
            private readonly Matrix4x4 _preScopeMatrix;

            public MatrixScope(Matrix4x4 newMatrix)
            {
                _preScopeMatrix = Gizmos.matrix;
                Gizmos.matrix = newMatrix;
            }

            public void Dispose()
            {
                Gizmos.matrix = _preScopeMatrix;
            }
        }

        public struct ColorScope : IDisposable
        {
            private readonly Color _preScopeColor;

            public ColorScope(Color newColor)
            {
                _preScopeColor = Gizmos.color;
                Gizmos.color = newColor;
            }

            public void Dispose()
            {
                Gizmos.color = _preScopeColor;
            }
        }

        public static void DrawColoredRay(Vector3 from, Vector3 direction, Color color)
        {
            using (var _ = new ColorScope(color))
            {
                Gizmos.DrawRay(from, direction);
            }
        }

        public static void DrawColoredRay(Ray ray, Color color)
        {
            using (var _ = new ColorScope(color))
            {
                Gizmos.DrawRay(ray);
            }
        }

        public static void DrawColoredLine(Vector3 from, Vector3 to, Color color)
        {
            using (var _ = new ColorScope(color))
            {
                Gizmos.DrawLine(from, to);
            }
        }

        public static void DrawColoredWireSphere(Vector3 center, float radius, Color color)
        {
            using (var _ = new ColorScope(color))
            {
                Gizmos.DrawWireSphere(center, radius);
            }
        }

        public static void DrawColoredSphere(Vector3 center, float radius, Color color)
        {
            using (var _ = new ColorScope(color))
            {
                Gizmos.DrawSphere(center, radius);
            }
        }

        public static void DrawColoredWireCube(Vector3 center, Quaternion rotation, Vector3 size, Color color)
        {
            using (var _ = new MatrixScope(Matrix4x4.TRS(center, rotation, size)))
            {
                using (var __ = new ColorScope(color))
                {
                    Gizmos.DrawWireCube(center, size);
                }
            }
        }

        public static void DrawColoredCube(Vector3 center, Quaternion rotation, Vector3 size, Color color)
        {
            using (var _ = new MatrixScope(Matrix4x4.TRS(center, rotation, size)))
            {
                using (var __ = new ColorScope(color))
                {
                    Gizmos.DrawCube(center, size);
                }
            }
        }

        public static void DrawColoredWireMesh(Mesh mesh, int submeshIndex, Vector3 position, Quaternion rotation, Vector3 scale, Color color)
        {
            using (var __ = new ColorScope(color))
            {
                Gizmos.DrawWireMesh(mesh, submeshIndex, position, rotation, scale);
            }
        }

        public static void DrawColoredMesh(Mesh mesh, int submeshIndex, Vector3 position, Quaternion rotation, Vector3 scale, Color color)
        {
            using (var __ = new ColorScope(color))
            {
                Gizmos.DrawMesh(mesh, submeshIndex, position, rotation, scale);
            }
        }
    }
}
