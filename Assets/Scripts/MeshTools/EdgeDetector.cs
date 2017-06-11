using System.Collections.Generic;
using UnityEngine;

namespace WaveFunctionCollapse.MeshTools
{
    public static class EdgeDetector
    {
        public static Edge[] FindMeshEdges(Vector3 axis, Mesh mesh)
        {
            var vertices = GetBoundryVertices(axis, mesh);
            vertices = RemoveDubles(vertices);

            Edge[] result = FindEdges(vertices).ToArray();

            return result;
        }

        private static List<Vertex> GetBoundryVertices(Vector3 axis, Mesh mesh)
        {
            Vector3 meshSize = mesh.bounds.extents;

            Vector3 axisCenter = Vector3.Project(mesh.bounds.center, axis);
            Vector3 axisDistance = Vector3.Project(meshSize, axis);

            if (Vector3.Dot(axisDistance, axis) < 0)
            {
                axisDistance = -axisDistance;
            }

            axisDistance += axisCenter;

            Vector3[] vertices = mesh.vertices;
            int[] triangles = mesh.triangles;
            
            List<Vertex> edge = new List<Vertex>();

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 vertexDistance = Vector3.Project(vertices[i], axis);
                if (vertexDistance == axisDistance)
                {
                    Vertex edgeVertex = new Vertex(vertices[i]);

                    for (int j = 0; j < triangles.Length; j++)
                    {
                        if (triangles[j] == i)
                        {
                            int triangleIndex = j / 3;
                            edgeVertex.triangles.Add(triangleIndex);
                        }
                    }

                    edge.Add(edgeVertex);
                }
            }

            return edge;
        }

        private static List<Vertex> RemoveDubles(List<Vertex> vertices)
        {
            var unprocessed = new List<Vertex>(vertices);
            var result = new List<Vertex>();

            while (unprocessed.Count > 0)
            {
                Vertex vertex = unprocessed[0];
                unprocessed.RemoveAt(0);

                var doubles = unprocessed.FindAll((v) => v.position == vertex.position);

                for (int i = 0; i < doubles.Count; i++)
                {
                    vertex.triangles.UnionWith(doubles[i].triangles);
                    unprocessed.Remove(doubles[i]);
                }

                result.Add(vertex);
            }

            return result;
        }

        private static List<Edge> FindEdges(List<Vertex> vertices)
        {
            var edges = new List<Edge>();

            for (int i = 0; i < vertices.Count; i++)
            {
                for (int j = i + 1; j < vertices.Count; j++)
                {
                    HashSet<int> common = new HashSet<int>(vertices[i].triangles);
                    common.IntersectWith(vertices[j].triangles);
                    if (common.Count == 1)
                    {
                        var edge = new Edge(vertices[i].position, vertices[j].position, common.Count);
                        edges.Add(edge);
                    }
                }
            }

            return edges;
        }

        private class Vertex
        {
            public Vector3 position;
            public HashSet<int> triangles;

            public Vertex(Vector3 pos)
            {
                position = pos;
                triangles = new HashSet<int>();
            }
        }

        public struct Edge
        {
            public Vector3[] vertices;
            public int triangles;

            public Edge(Vector3 v1, Vector3 v2, int triangles)
            {
                vertices = new Vector3[2];
                vertices[0] = v1;
                vertices[1] = v2;

                this.triangles = triangles;
            }
        }
    }
}