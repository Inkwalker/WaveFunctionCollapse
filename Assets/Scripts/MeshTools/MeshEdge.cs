using UnityEngine;
using System.Collections.Generic;

namespace WaveFunctionCollapse.MeshTools
{
    [System.Serializable]
    public class MeshEdge
    {
        [SerializeField] Vector3[] vertices;
        [SerializeField] int[] edges;

        [SerializeField] Vector3 direction;

        public Vector3[] Vertices { get { return vertices; } }
        public int[] Edges { get { return edges; } }
        public int EdgesCount { get { return edges.Length / 2; } }
        public Vector3 Direction { get { return direction; } }

        public MeshEdge(Vector3[] vertices, int[] edges, Vector3 direction)
        {
            this.vertices = vertices;
            this.edges = edges;

            this.direction = direction;
        }

        public Vector3[] GetEdgeVertices(int edge)
        {
            int indexV0 = edge * 2;
            int indexV1 = indexV0 + 1;

            Vector3 v0 = vertices[edges[indexV0]];
            Vector3 v1 = vertices[edges[indexV1]];

            return new Vector3[] { v0, v1 };
        }
    }
}
