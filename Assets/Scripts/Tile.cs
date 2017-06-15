using UnityEngine;
using WaveFunctionCollapse.MeshTools;
using WaveFunctionCollapse.Extensions;

namespace WaveFunctionCollapse
{
    [SelectionBase]
    public class Tile : MonoBehaviour
    {
        [SerializeField] Bounds bounds = new Bounds(Vector3.zero, Vector3.one);
        [SerializeField] MeshFilter mainMesh;

        [SerializeField][HideInInspector] MeshEdge[] edges;

        public MeshEdge[] Edges { get { return edges; } }

        [ContextMenu("Update Mesh Edges")]
        public void UpdateMeshEdges()
        {
            edges = new MeshEdge[6];

            edges[0] = EdgeDetector.FindMeshEdge(Vector3.up,   bounds, mainMesh.sharedMesh);
            edges[1] = EdgeDetector.FindMeshEdge(Vector3.down, bounds, mainMesh.sharedMesh);

            edges[2] = EdgeDetector.FindMeshEdge(Vector3.right, bounds, mainMesh.sharedMesh);
            edges[3] = EdgeDetector.FindMeshEdge(Vector3.left,  bounds, mainMesh.sharedMesh);

            edges[4] = EdgeDetector.FindMeshEdge(Vector3.forward, bounds, mainMesh.sharedMesh);
            edges[5] = EdgeDetector.FindMeshEdge(Vector3.back,    bounds, mainMesh.sharedMesh);
        }

        public MeshEdge GetEdge(Vector3 worldDirection)
        {
            Vector3 localDir = transform.InverseTransformDirection(worldDirection);

            foreach (var edge in edges)
            {
                if (edge.Direction == localDir) return edge;
            }

            Debug.LogWarning("Can't find edge for vector " + worldDirection.ToString());
            return null;
        }

        private void OnDrawGizmosSelected()
        {
            GizmosExt.DrawCube(transform.TransformPoint(bounds.center), bounds.size, transform.rotation);
        }
    }
}
