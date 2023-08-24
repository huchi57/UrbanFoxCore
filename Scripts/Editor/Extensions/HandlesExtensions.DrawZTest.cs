using UnityEngine;
using UnityEngine.Rendering;

namespace UrbanFox.Editor
{
    public static partial class HandlesExtensions
    {
        private const float _occludeColorAlpha = 0.25f;
        private static readonly Color _defaultColor = Color.white;
        private static readonly Color _occludeColor = Color.gray;

        #region WireCube
        public static void DrawZTestWireCube(Vector3 center, Vector3 size, Color defaultColor, Color occludeColor)
        {
            using (var _ = new ZTestScope(CompareFunction.LessEqual))
            {
                DrawColoredWireCube(center, size, defaultColor);
            }
            using (var _ = new ZTestScope(CompareFunction.Greater))
            {
                DrawColoredWireCube(center, size, occludeColor);
            }
        }

        public static void DrawZTestWireCube(Vector3 center, Vector3 size, Color defaultColor)
        {
            DrawZTestWireCube(center, size, defaultColor, defaultColor.SetAlpha(_occludeColorAlpha));
        }

        public static void DrawZTestWireCube(Vector3 center, Vector3 size)
        {
            DrawZTestWireCube(center, size, _defaultColor, _occludeColor);
        }

        public static void DrawZTestRotatedWireCube(Vector3 center, Quaternion rotation, Vector3 size, Color defaultColor, Color occludeColor)
        {
            using (var _ = new ZTestScope(CompareFunction.LessEqual))
            {
                DrawColoredRotatedWireCube(center, rotation, size, defaultColor);
            }
            using (var _ = new ZTestScope(CompareFunction.Greater))
            {
                DrawColoredRotatedWireCube(center, rotation, size, occludeColor);
            }
        }

        public static void DrawZTestRotatedWireCube(Vector3 center, Quaternion rotation, Vector3 size, Color defaultColor)
        {
            DrawZTestRotatedWireCube(center, rotation, size, defaultColor, defaultColor.SetAlpha(_occludeColorAlpha));
        }

        public static void DrawZTestRotatedWireCube(Vector3 center, Quaternion rotation, Vector3 size)
        {
            DrawZTestRotatedWireCube(center, rotation, size, _defaultColor, _occludeColor);
        }
        #endregion

        #region WireArc and WireDisc
        public static void DrawZTestWireArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius, float thickness, Color defaultColor, Color occludeColor)
        {
            using (var _ = new ZTestScope(CompareFunction.LessEqual))
            {
                DrawColoredWireArc(center, normal, from, angle, radius, thickness, defaultColor);
            }
            using (var _ = new ZTestScope(CompareFunction.Greater))
            {
                DrawColoredWireArc(center, normal, from, angle, radius, thickness, occludeColor);
            }
        }

        public static void DrawZTestWireArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius, float thickness, Color defaultColor)
        {
            DrawZTestWireArc(center, normal, from, angle, radius, thickness, defaultColor, defaultColor.SetAlpha(_occludeColorAlpha));
        }

        public static void DrawZTestWireArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius, Color defaultColor, Color occludeColor)
        {
            DrawZTestWireArc(center, normal, from, angle, radius, 0, defaultColor, occludeColor);
        }

        public static void DrawZTestWireArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius, Color defaultColor)
        {
            DrawZTestWireArc(center, normal, from, angle, radius, 0, defaultColor, defaultColor.SetAlpha(_occludeColorAlpha));
        }

        public static void DrawZTestWireArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius, float thickness)
        {
            DrawZTestWireArc(center, normal, from, angle, radius, thickness, _defaultColor, _occludeColor);
        }

        public static void DrawZTestWireArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius)
        {
            DrawZTestWireArc(center, normal, from, angle, radius, 0, _defaultColor, _occludeColor);
        }

        public static void DrawZTestWireDisc(Vector3 center, Vector3 normal, float radius, float thickness, Color defaultColor, Color occludeColor)
        {
            using (var _ = new ZTestScope(CompareFunction.LessEqual))
            {
                DrawColoredWireDisc(center, normal, radius, thickness, defaultColor);
            }
            using (var _ = new ZTestScope(CompareFunction.Greater))
            {
                DrawColoredWireDisc(center, normal, radius, thickness, occludeColor);
            }
        }

        public static void DrawZTestWireDisc(Vector3 center, Vector3 normal, float radius, float thickness, Color defaultColor)
        {
            DrawZTestWireDisc(center, normal, radius, thickness, defaultColor, defaultColor.SetAlpha(_occludeColorAlpha));
        }

        public static void DrawZTestWireDisc(Vector3 center, Vector3 normal, float radius, float thickness)
        {
            DrawZTestWireDisc(center, normal, radius, thickness, _defaultColor, _occludeColor);
        }

        public static void DrawZTestWireDisc(Vector3 center, Vector3 normal, float radius, Color defaultColor, Color occludeColor)
        {
            DrawZTestWireDisc(center, normal, radius, 0, defaultColor, occludeColor);
        }

        public static void DrawZTestWireDisc(Vector3 center, Vector3 normal, float radius, Color defaultColor)
        {
            DrawZTestWireDisc(center, normal, radius, 0, defaultColor, defaultColor.SetAlpha(_occludeColorAlpha));
        }

        public static void DrawZTestWireDisc(Vector3 center, Vector3 normal, float radius)
        {
            DrawZTestWireDisc(center, normal, radius, 0, _defaultColor, _occludeColor);
        }
        #endregion

        #region SolidArc and SolidDisc
        public static void DrawZTestSolidArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius, Color defaultColor, Color occludeColor)
        {
            using (var _ = new ZTestScope(CompareFunction.LessEqual))
            {
                DrawColoredSolidArc(center, normal, from, angle, radius, defaultColor);
            }
            using (var _ = new ZTestScope(CompareFunction.Greater))
            {
                DrawColoredSolidArc(center, normal, from, angle, radius, occludeColor);
            }
        }

        public static void DrawZTestSolidArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius, Color defaultColor)
        {
            DrawZTestSolidArc(center, normal, from, angle, radius, defaultColor, defaultColor.SetAlpha(_occludeColorAlpha));
        }

        public static void DrawZTestSolidArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius)
        {
            DrawZTestSolidArc(center, normal, from, angle, radius, _defaultColor, _occludeColor);
        }

        public static void DrawZTestSolidDisc(Vector3 center, Vector3 normal, float radius, Color defaultColor, Color occludeColor)
        {
            using (var _ = new ZTestScope(CompareFunction.LessEqual))
            {
                DrawColoredSolidDisc(center, normal, radius, defaultColor);
            }
            using (var _ = new ZTestScope(CompareFunction.Greater))
            {
                DrawColoredSolidDisc(center, normal, radius, occludeColor);
            }
        }

        public static void DrawZTestSolidDisc(Vector3 center, Vector3 normal, float radius, Color defaultColor)
        {
            DrawZTestSolidDisc(center, normal, radius, defaultColor, defaultColor.SetAlpha(_occludeColorAlpha));
        }

        public static void DrawZTestSolidDisc(Vector3 center, Vector3 normal, float radius)
        {
            DrawZTestSolidDisc(center, normal, radius, _defaultColor, _occludeColor);
        }
        #endregion

        #region Line and Lines
        public static void DrawZTestLine(Vector3 point1, Vector3 point2, float thickness, Color defaultColor, Color occludeColor)
        {
            using (var _ = new ZTestScope(CompareFunction.LessEqual))
            {
                DrawColoredLine(point1, point2, thickness, defaultColor);
            }
            using (var _ = new ZTestScope(CompareFunction.Greater))
            {
                DrawColoredLine(point1, point2, thickness, occludeColor);
            }
        }

        public static void DrawZTestLine(Vector3 point1, Vector3 point2, float thickness, Color defaultColor)
        {
            DrawZTestLine(point1, point2, thickness, defaultColor, defaultColor.SetAlpha(_occludeColorAlpha));
        }

        public static void DrawZTestLine(Vector3 point1, Vector3 point2, Color defaultColor, Color occludeColor)
        {
            DrawZTestLine(point1, point2, 0, defaultColor, occludeColor);
        }

        public static void DrawZTestLine(Vector3 point1, Vector3 point2, Color defaultColor)
        {
            DrawZTestLine(point1, point2, 0, defaultColor, defaultColor.SetAlpha(_occludeColorAlpha));
        }

        public static void DrawZTestLine(Vector3 point1, Vector3 point2, float thickness)
        {
            DrawZTestLine(point1, point2, thickness, _defaultColor, _occludeColor);
        }

        public static void DrawZTestLine(Vector3 point1, Vector3 point2)
        {
            DrawZTestLine(point1, point2, 0, _defaultColor, _occludeColor);
        }

        public static void DrawZTestLines(Vector3[] lineSegments, Color defaultColor, Color occludeColor)
        {
            if (!lineSegments.IsNullOrEmpty())
            {
                using (var _ = new ZTestScope(CompareFunction.LessEqual))
                {
                    DrawColoredLines(lineSegments, defaultColor);
                }
                using (var _ = new ZTestScope(CompareFunction.Greater))
                {
                    DrawColoredLines(lineSegments, occludeColor);
                }
            }
        }

        public static void DrawZTestLines(Vector3[] lineSegments, Color defaultColor)
        {
            DrawZTestLines(lineSegments, defaultColor, defaultColor.SetAlpha(_occludeColorAlpha));
        }

        public static void DrawZTestLines(Vector3[] lineSegments)
        {
            DrawZTestLines(lineSegments, _defaultColor, _occludeColor);
        }

        public static void DrawZTestLines(Vector3[] lineSegments, int[] segmentIndices, Color defaultColor, Color occludeColor)
        {
            if (!lineSegments.IsNullOrEmpty())
            {
                using (var _ = new ZTestScope( CompareFunction.LessEqual))
                {
                    DrawColoredLines(lineSegments, segmentIndices, defaultColor);
                }
                using (var _ = new ZTestScope( CompareFunction.Greater))
                {
                    DrawColoredLines(lineSegments, segmentIndices, occludeColor);
                }
            }
        }

        public static void DrawZTestLines(Vector3[] lineSegments, int[] segmentIndices, Color defaultColor)
        {
            DrawZTestLines(lineSegments, segmentIndices, defaultColor, defaultColor.SetAlpha(_occludeColorAlpha));
        }

        public static void DrawZTestLines(Vector3[] lineSegments, int[] segmentIndices)
        {
            DrawZTestLines(lineSegments, segmentIndices, _defaultColor, _occludeColor);
        }
        #endregion

        #region DottedLine and DottedLines
        public static void DrawZTestDottedLine(Vector3 point1, Vector3 point2, float screenSpacePixelSize, Color defaultColor, Color occludeColor)
        {
            using (var _ = new ZTestScope(CompareFunction.LessEqual))
            {
                DrawColoredDottedLine(point1, point2, screenSpacePixelSize, defaultColor);
            }
            using (var _ = new ZTestScope(CompareFunction.Greater))
            {
                DrawColoredDottedLine(point1, point2, screenSpacePixelSize, occludeColor);
            }
        }

        public static void DrawZTestDottedLine(Vector3 point1, Vector3 point2, float screenSpacePixelSize, Color defaultColor)
        {
            DrawZTestDottedLine(point1, point2, screenSpacePixelSize, defaultColor, defaultColor.SetAlpha(_occludeColorAlpha));
        }

        public static void DrawZTestDottedLine(Vector3 point1, Vector3 point2, float screenSpacePixelSize)
        {
            DrawZTestDottedLine(point1, point2, screenSpacePixelSize, _defaultColor, _occludeColor);
        }

        public static void DrawZTestDottedLines(Vector3[] lineSegments, float screenSpacePixelSize, Color defaultColor, Color occludeColor)
        {
            if (!lineSegments.IsNullOrEmpty())
            {
                using (var _ = new ZTestScope(CompareFunction.LessEqual))
                {
                    DrawColoredDottedLines(lineSegments, screenSpacePixelSize, defaultColor);
                }
                using (var _ = new ZTestScope(CompareFunction.Greater))
                {
                    DrawColoredDottedLines(lineSegments, screenSpacePixelSize, occludeColor);
                }
            }
        }

        public static void DrawZTestDottedLines(Vector3[] lineSegments, float screenSpacePixelSize, Color defaultColor)
        {
            DrawZTestDottedLines(lineSegments, screenSpacePixelSize, defaultColor, defaultColor.SetAlpha(_occludeColorAlpha));
        }

        public static void DrawZTestDottedLines(Vector3[] lineSegments, float screenSpacePixelSize)
        {
            DrawZTestDottedLines(lineSegments, screenSpacePixelSize, _defaultColor, _occludeColor);
        }

        public static void DrawZTestDottedLines(Vector3[] lineSegments, int[] segmentIndicies, float screenSpacePixelSize, Color defaultColor, Color occludeColor)
        {
            if (!lineSegments.IsNullOrEmpty())
            {
                using (var _ = new ZTestScope(CompareFunction.LessEqual))
                {
                    DrawColoredDottedLines(lineSegments, segmentIndicies, screenSpacePixelSize, defaultColor);
                }
                using (var _ = new ZTestScope(CompareFunction.Greater))
                {
                    DrawColoredDottedLines(lineSegments, segmentIndicies, screenSpacePixelSize, occludeColor);
                }
            }
        }

        public static void DrawZTestDottedLines(Vector3[] lineSegments, int[] segmentIndicies, float screenSpacePixelSize, Color defaultColor)
        {
            DrawZTestDottedLines(lineSegments, segmentIndicies, screenSpacePixelSize, defaultColor, defaultColor.SetAlpha(_occludeColorAlpha));
        }

        public static void DrawZTestDottedLines(Vector3[] lineSegments, int[] segmentIndicies, float screenSpacePixelSize)
        {
            DrawZTestDottedLines(lineSegments, segmentIndicies, screenSpacePixelSize, _defaultColor, _occludeColor);
        }
        #endregion

        #region PolyLines
        public static void DrawZTestPolyLines(Vector3[] points, Color defaultColor, Color occludeColor)
        {
            if (!points.IsNullOrEmpty())
            {
                using (var _ = new ZTestScope(CompareFunction.LessEqual))
                {
                    DrawColoredPolyLines(points, defaultColor);
                }
                using (var _ = new ZTestScope(CompareFunction.Greater))
                {
                    DrawColoredPolyLines(points, occludeColor);
                }
            }
        }

        public static void DrawZTestPolyLines(Vector3[] points, Color defaultColor)
        {
            DrawZTestPolyLines(points, defaultColor, defaultColor.SetAlpha(_occludeColorAlpha));
        }

        public static void DrawZTestPolyLines(Color defaultColor, Color occludeColor, params Vector3[] points)
        {
            DrawZTestPolyLines(points, defaultColor, occludeColor);
        }

        public static void DrawZTestPolyLines(params Vector3[] points)
        {
            DrawZTestPolyLines(points, _defaultColor, _occludeColor);
        }
        #endregion

        #region AAConvexPolygon
        public static void DrawZTestAAConvexPolygon(Vector3[] points, Color defaultColor, Color occludeColor)
        {
            if (!points.IsNullOrEmpty())
            {
                using (var _ = new ZTestScope(CompareFunction.LessEqual))
                {
                    DrawColoredAAConvexPolygon(points, defaultColor);
                }
                using (var _ = new ZTestScope(CompareFunction.Greater))
                {
                    DrawColoredAAConvexPolygon(points, occludeColor);
                }
            }
        }

        public static void DrawZTestAAConvexPolygon(Vector3[] points, Color defaultColor)
        {
            DrawZTestAAConvexPolygon(points, defaultColor, defaultColor.SetAlpha(_occludeColorAlpha));
        }

        public static void DrawZTestAAConvexPolygon(Color defaultColor, Color occludeColor, params Vector3[] points)
        {
            DrawZTestAAConvexPolygon(points, defaultColor, occludeColor);
        }

        public static void DrawZTestAAConvexPolygon(params Vector3[] points)
        {
            DrawZTestAAConvexPolygon(points, _defaultColor, _occludeColor);
        }
        #endregion
    }
}
