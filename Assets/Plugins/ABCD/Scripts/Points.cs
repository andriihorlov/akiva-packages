using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ABCDEssentials
{
    public static class Points
    {
        public static List<Vector3> Circle(float radius, int density, Vector3 axisUp)
        {
            var positions = new List<Vector3>(new Vector3[density]);

            var localA = new Vector3(axisUp.y, axisUp.z, axisUp.x);
            var localB = Vector3.Cross(axisUp, localA);

            var step = 2.0f * Mathf.PI / density;
            for (var i = 0; i < density; i++)
            {
                var theta = i * step;
                var x = radius * Mathf.Cos(theta);
                var y = radius * Mathf.Sin(theta);

                positions[i] = x * localA + y * localB;
            }

            return positions;
        }

        public static List<Vector3> Plane(int width, int height, Vector3 axisUp)
        {
            var localA = new Vector3(axisUp.y, axisUp.x, axisUp.z);
            var localB = Vector3.Cross(axisUp, localA);

            var vertices = new Vector3[width * height];

            for (var y = 0; y < height; y++)
            {
                var indexOffset = y * width;
                for (var x = 0; x < width; x++)
                {
                    var norm = new Vector2(x, y) / new Vector2(width - 1, height - 1);

                    vertices[indexOffset + x] = (norm.x * 0.5f - 1.0f) * localA + (norm.y * 0.5f - 1.0f) * localB;
                }
            }

            return vertices.ToList();
        }

        public static List<Vector3> Box(int width, int height, int depth, Vector3 axisUp)
        {
            var vertices = new Vector3[width * height * depth];

            return vertices.ToList();
        }
    }
}