using UnityEngine;
using WaveFunctionCollapse.MeshTools;

public class EdgeDebuger : MonoBehaviour
{
    public MeshFilter meshFilter;
    public Vector3 axis;
    public Bounds tileBounds = new Bounds(Vector3.zero, Vector3.one);

    [ContextMenu("Print Edges Hash")]
    private void PrintEdgesHash()
    {
        Mesh mesh = meshFilter.sharedMesh;

        var edgeXp = EdgeDetector.FindMeshEdge(Vector3.right,   tileBounds, mesh);
        var edgeXn = EdgeDetector.FindMeshEdge(Vector3.left,    tileBounds, mesh);
        var edgeYp = EdgeDetector.FindMeshEdge(Vector3.up,      tileBounds, mesh);
        var edgeYn = EdgeDetector.FindMeshEdge(Vector3.down,    tileBounds, mesh);
        var edgeZp = EdgeDetector.FindMeshEdge(Vector3.forward, tileBounds, mesh);
        var edgeZn = EdgeDetector.FindMeshEdge(Vector3.back,    tileBounds, mesh);

        Debug.Log("X+ " + edgeXp.GetHashCode() + " | X- " + edgeXn.GetHashCode());
        Debug.Log("Y+ " + edgeYp.GetHashCode() + " | Y- " + edgeYn.GetHashCode());
        Debug.Log("Z+ " + edgeZp.GetHashCode() + " | Z- " + edgeZn.GetHashCode());
    }

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
