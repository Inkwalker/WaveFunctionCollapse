using System.Collections.Generic;
using UnityEngine;

namespace WaveFunctionCollapse.MeshTools
{
    public static class EdgeDetector
    {
        public static MeshEdge FindMeshEdge(Vector3 direction, Bounds tileBounds, Mesh mesh)
        {
            var vertices = GetBoundryVertices(direction, tileBounds, mesh);
            vertices = RemoveDubles(vertices);

            MeshEdge edge = FindEdge(vertices, direction);

            return edge;
        }

        private static List<Vertex> GetBoundryVertices(Vector3 direction, Bounds tileBounds, Mesh mesh)
        {
            Vector3 tileSize = tileBounds.extents;

            Vector3 dirCenterProjection = Vector3.Project(tileBounds.center, direction);
            Vector3 dirSizeProjection = Vector3.Project(tileSize, direction);

            if (Vector3.Dot(dirSizeProjection, direction) < 0)
            {
                dirSizeProjection = -dirSizeProjection;
            }

            dirSizeProjection += dirCenterProjection;

            Vector3[] vertices = mesh.vertices;
            int[] triangles = mesh.triangles;
            
            List<Vertex> edge = new List<Vertex>();

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 vertexDistance = Vector3.Project(vertices[i], direction);
                if (vertexDistance == dirSizeProjection)
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

        private static MeshEdge FindEdge(List<Vertex> vertices, Vector3 direction)
        {
            var edgeVertices = new List<Vector3>();
            var edges = new List<int>();

            for (int i = 0; i < vertices.Count; i++)
            {
                for (int j = i + 1; j < vertices.Count; j++)
                {
                    HashSet<int> common = new HashSet<int>(vertices[i].triangles);
                    common.IntersectWith(vertices[j].triangles);
                    if (common.Count == 1)
                    {
                        Vector3 v1 = vertices[i].position;
                        Vector3 v2 = vertices[j].position;

                        if (edgeVertices.Contains(v1) == false) edgeVertices.Add(v1);
                        if (edgeVertices.Contains(v2) == false) edgeVertices.Add(v2);

                        int indexV1 = edgeVertices.IndexOf(v1);
                        int indexV2 = edgeVertices.IndexOf(v2);

                        edges.Add(indexV1);
                        edges.Add(indexV2);
                    }
                }
            }

            var meshEdge = new MeshEdge(edgeVertices.ToArray(), edges.ToArray(), direction);

            return meshEdge;
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
    }
}