using UnityEngine;
using UnityEditor;

namespace UrbanFox.Editor
{
    public static partial class HandlesExtensions
    {
#pragma warning disable IDE0063 // Simplified usings are only available on C# 8.0+
        #region WireCube
        public static void DrawRotatedWireCube(Vector3 center, Quaternion rotation, Vector3 size)
        {
            using (var _ = new MatrixScope(Matrix4x4.TRS(center, rotation, size)))
            {
                Handles.DrawWireCube(Vector3.zero, Vector3.one);
            }
        }

        public static void DrawColoredWireCube(Vector3 center, Vector3 size, Color color)
        {
            using (var _ = new ColorScope(color))
            {
                Handles.DrawWireCube(center, size);
            }
        }

        public static void DrawColoredRotatedWireCube(Vector3 center, Quaternion rotation, Vector3 size, Color color)
        {
            using (var _ = new ColorScope(color))
            { 
                DrawRotatedWireCube(center, rotation, size);
            }
        }
        #endregion

        #region WireArc and WireDisc
        public static void DrawColoredWireArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius, float thickness, Color color)
        {
            using (var _ = new ColorScope(color))
            {
                Handles.DrawWireArc(center, normal, from, angle, radius, thickness);
            }
        }

        public static void DrawColoredWireArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius, Color color)
        {
            using (var _ = new ColorScope(color))
            {
                Handles.DrawWireArc(center, normal, from, angle, radius);
            }
        }

        public static void DrawColoredWireDisc(Vector3 center, Vector3 normal, float radius, float thickness, Color color)
        {
            using (var _ = new ColorScope(color))
            {
                Handles.DrawWireDisc(center, normal, radius, thickness);
            }
        }

        public static void DrawColoredWireDisc(Vector3 center, Vector3 normal, float radius, Color color)
        {
            using (var _ = new ColorScope(color))
            {
                Handles.DrawWireDisc(center, normal, radius);
            }
        }
        #endregion

        #region SolidArc and SolidDisc
        public static void DrawColoredSolidArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius, Color color)
        {
            using (var _ = new ColorScope(color))
            {
                Handles.DrawSolidArc(center, normal, from, angle, radius);
            }
        }

        public static void DrawColoredSolidDisc(Vector3 center, Vector3 normal, float radius, Color color)
        {
            using (var _ = new ColorScope(color))
            {
                Handles.DrawSolidDisc(center, normal, radius);
            }
        }
        #endregion

        #region Line and Lines
        public static void DrawColoredLine(Vector3 point1, Vector3 point2, float thickness, Color color)
        {
            using (var _ = new ColorScope(color))
            {
                Handles.DrawLine(point1, point2, thickness);
            }
        }

        public static void DrawColoredLine(Vector3 point1, Vector3 point2, Color color)
        {
            DrawColoredLine(point1, point2, 0, color);
        }

        public static void DrawColoredLines(Vector3[] lineSegments, Color color)
        {
            if (!lineSegments.IsNullOrEmpty())
            {
                using (var _ = new ColorScope(color))
                {
                    Handles.DrawLines(lineSegments);
                }
            }
        }

        public static void DrawColoredLines(Vector3[] lineSegments, int[] segmentIndices, Color color)
        {
            if (!lineSegments.IsNullOrEmpty())
            {
                using (var _ = new ColorScope(color))
                {
                    Handles.DrawLines(lineSegments, segmentIndices);
                }
            }
        }
        #endregion

        #region DottedLine and DottedLines
        public static void DrawColoredDottedLine(Vector3 point1, Vector3 point2, float screenSpacePixelSize, Color color)
        {
            using (var _ = new ColorScope(color))
            {
                Handles.DrawDottedLine(point1, point2, screenSpacePixelSize);
            }
        }

        public static void DrawColoredDottedLines(Vector3[] lineSegments, float screenSpacePixelSize, Color color)
        {
            if (!lineSegments.IsNullOrEmpty())
            {
                using (var _ = new ColorScope(color))
                {
                    Handles.DrawDottedLines(lineSegments, screenSpacePixelSize);
                }
            }
        }

        public static void DrawColoredDottedLines(Vector3[] lineSegments, int[] segmentIndicies, float screenSpacePixelSize, Color color)
        {
            if (!lineSegments.IsNullOrEmpty())
            {
                using (var _ = new ColorScope(color))
                {
                    Handles.DrawDottedLines(lineSegments, segmentIndicies, screenSpacePixelSize);
                }
            }
        }
        #endregion

        #region PolyLines
        public static void DrawColoredPolyLines(Vector3[] points, Color color)
        {
            if (!points.IsNullOrEmpty())
            {
                using (var _ = new ColorScope(color))
                {
                    Handles.DrawPolyLine(points);
                }
            }
        }

        public static void DrawColoredPolyLines(Color color, params Vector3[] points)
        {
            DrawColoredPolyLines(points, color);
        }
        #endregion

        #region AAConvexPolygon
        public static void DrawColoredAAConvexPolygon(Vector3[] points, Color color)
        {
            if (!points.IsNullOrEmpty())
            {
                using (var _ = new ColorScope(color))
                {
                    Handles.DrawAAConvexPolygon(points);
                }
            }
        }

        public static void DrawColoredAAConvexPolygon(Color color, params Vector3[] points)
        {
            DrawColoredAAConvexPolygon(points, color);
        }
        #endregion
#pragma warning restore IDE0063
    }
}
