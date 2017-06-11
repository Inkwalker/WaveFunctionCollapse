using UnityEngine;
using WaveFunctionCollapse.MeshTools;

public class EdgeDebuger : MonoBehaviour
{
    public MeshFilter meshFilter;
    public Vector3 axis;

    private void OnDrawGizmosSelected()
    {
        if (meshFilter == null) return;

        Mesh mesh = meshFilter.sharedMesh;
        Transform transform = meshFilter.transform;

        var edges = EdgeDetector.FindMeshEdges(axis, mesh);

        Gizmos.color = Color.red;
        foreach (var edge in edges)
        {
            Vector3 worldV0 = transform.TransformPoint(edge.vertices[0]);
            Vector3 worldV1 = transform.TransformPoint(edge.vertices[1]);

            Gizmos.DrawLine(worldV0, worldV1);

            Gizmos.DrawSphere(worldV0, 0.01f);
            Gizmos.DrawSphere(worldV1, 0.01f);
        }

        //Debug.Log("Vertices found: " + vertices.Length);
    }
}
