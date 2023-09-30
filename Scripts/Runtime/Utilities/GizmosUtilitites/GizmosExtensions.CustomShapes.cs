using UnityEngine;

namespace UrbanFox
{
    public static partial class GizmosExtensions
    {
        public static void DrawWireDisc(Vector3 center, Vector3 normal, float radius, Color color)
        {
            var forwardDirection = normal.GetPerpendicularVector();
            using (new GizmosScope(center, Quaternion.LookRotation(forwardDirection, normal), radius * Vector3.one, color))
            {
                var p0 = Vector3.forward;
                for (int angle = 0; angle < 360; angle++)
                {
                    var p1 = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
                    Gizmos.DrawLine(p0, p1);
                    p0 = p1;
                }
            }
        }

        public static void DrawWireDisc(Vector3 center, Vector3 normal, float radius)
        {
            DrawWireDisc(center, normal, radius, Gizmos.color);
        }
    }
}
