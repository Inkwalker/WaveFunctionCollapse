using UnityEngine;
using System.Collections;

namespace WaveFunctionCollapse.Extensions
{
    public static class GizmosExt
    {
        public static void DrawCube(Vector3 center, Vector3 size, Quaternion rotation)
        {
            var points = new Vector3[8];

            points[0] = rotation * (new Vector3( size.x, size.y,  size.z) * 0.5f) + center;
            points[1] = rotation * (new Vector3( size.x, size.y, -size.z) * 0.5f) + center;
            points[2] = rotation * (new Vector3(-size.x, size.y,  size.z) * 0.5f) + center;
            points[3] = rotation * (new Vector3(-size.x, size.y, -size.z) * 0.5f) + center;

            points[4] = rotation * (new Vector3( size.x, -size.y,  size.z) * 0.5f) + center;
            points[5] = rotation * (new Vector3( size.x, -size.y, -size.z) * 0.5f) + center;
            points[6] = rotation * (new Vector3(-size.x, -size.y,  size.z) * 0.5f) + center;
            points[7] = rotation * (new Vector3(-size.x, -size.y, -size.z) * 0.5f) + center;

            //Top
            Gizmos.DrawLine(points[0], points[1]);
            Gizmos.DrawLine(points[0], points[2]);
            Gizmos.DrawLine(points[1], points[3]);
            Gizmos.DrawLine(points[3], points[2]);

            //Bottom
            Gizmos.DrawLine(points[4], points[5]);
            Gizmos.DrawLine(points[4], points[6]);
            Gizmos.DrawLine(points[5], points[7]);
            Gizmos.DrawLine(points[7], points[6]);

            //Sides
            Gizmos.DrawLine(points[0], points[4]);
            Gizmos.DrawLine(points[1], points[5]);
            Gizmos.DrawLine(points[2], points[6]);
            Gizmos.DrawLine(points[3], points[7]);
        }
    }
}
