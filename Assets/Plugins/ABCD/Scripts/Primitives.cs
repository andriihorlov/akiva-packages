using System.Collections.Generic;
using UnityEngine;

namespace ABCDEssentials
{
    public static class Primitives
    {
        public static Mesh Plane(int width, int height, Vector3 axisUp)
        {
            var mesh = new Mesh();

            var localA = new Vector3(axisUp.y, axisUp.x, axisUp.z);
            var localB = Vector3.Cross(axisUp, localA);

            var vertices = new Vector3[width * height];
            var triangles = new int[3 * 2 * (width - 1) * (height - 1)];

            var trianglesCount = 0;
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var index = x + y * width;

                    var tx = y / (width - 1.0f) * 2.0f - 1.0f;
                    var tz = x / (height - 1.0f) * 2.0f - 1.0f;

                    vertices[index] = axisUp + tx * localA + tz * localB;

                    if (x != width - 1 && y != height - 1)
                    {
                        var v0 = index;
                        var v1 = index + 1;
                        var v2 = v0 + width;
                        var v3 = v1 + width;

                        MakeTriangleQuad(ref triangles, ref trianglesCount, v0, v1, v2, v3);
                    }
                }
            }
            
//            var vertices = Points.Plane(width, height, axisUp);
//            var triangles = new int[3 * 2 * (width - 1) * (height - 1)];
//            
//            var trianglesCount = 0;
//            for (var y = 0; y < height - 1; y++)
//            {
//                for (var x = 0; x < width - 1; x++)
//                {
//                    var index = x + y * width;
//
//                    var v0 = index;
//                    var v1 = index + 1;
//                    var v2 = v0 + width;
//                    var v3 = v1 + width;
//
//                    MakeTriangleQuad(ref triangles, ref trianglesCount, v0, v1, v2, v3);
//                }
//            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            return mesh;
        }

        private static void MakeTriangleQuad(ref int[] triangles, ref int counter, int v0, int v1, int v2, int v3)
        {
            var offset = counter * 6;
            ++counter;

            triangles[offset] = v0;
            triangles[offset + 1] = v3;
            triangles[offset + 2] = v1;

            triangles[offset + 3] = v0;
            triangles[offset + 4] = v2;
            triangles[offset + 5] = v3;
        }
    }

    public static class Utils
    {
        public static List<Mesh> SplitByTriangles(Mesh source)
        {
            var meshes = new List<Mesh>();

            for (var i = 0; i < source.triangles.Length / 3; i += 3)
            {
                var vertices = new Vector3[3];
                for (var j = 0; j < 3; j++)
                    vertices[j] = source.vertices[source.triangles[i + j]];


                var mesh = new Mesh
                {
                    vertices = vertices,
                    triangles = new int[3] {0, 1, 2}
                };
                mesh.RecalculateNormals();

                meshes.Add(mesh);
            }

            return meshes;
        }
    }

    public static class Lined
    {
        public static Mesh Circle(float radius, int density, Vector3 axisUp)
        {
            var mesh = new Mesh();

            var vertices = Points.Circle(radius, density, axisUp);
            var indices = new int[vertices.Count + 1];

            for (var i = 0; i < indices.Length - 1; i++)
            {
                indices[i] = i;
            }

            indices[indices.Length - 1] = 0;

            mesh.vertices = vertices.ToArray();
            mesh.SetIndices(indices, MeshTopology.LineStrip, 0);

            return mesh;
        }
    }
}