using UnityEngine;
using WaveFunctionCollapse.MeshTools;

public class EdgeDebuger : MonoBehaviour
{
    public MeshFilter meshFilter;
    public Vector3 axis;
    public Bounds tileBounds = new Bounds(Vector3.zero, Vector3.one);

    private void OnDrawGizmosSelected()
    {
        if (meshFilter == null) return;

        Mesh mesh = meshFilter.sharedMesh;
        Transform transform = meshFilter.transform;

        Gizmos.color = Color.white;

        Gizmos.DrawWireCube(transform.TransformPoint(tileBounds.center), tileBounds.size);

        var edge = EdgeDetector.FindMeshEdge(axis, tileBounds, mesh);

        Gizmos.color = Color.yellow;
        for (int i = 0; i < edge.EdgesCount; i++)
        {
            Vector3[] edgeVertices = edge.GetEdgeVertices(i);

            Vector3 worldV0 = transform.TransformPoint(edgeVertices[0]);
            Vector3 worldV1 = transform.TransformPoint(edgeVertices[1]);

            Gizmos.DrawLine(worldV0, worldV1);

            Gizmos.DrawSphere(worldV0, 0.01f);
            Gizmos.DrawSphere(worldV1, 0.01f);
        }
    }
}
