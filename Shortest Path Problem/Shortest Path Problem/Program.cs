
using System;
using System.Collections.Generic;

namespace Shortest_Path_Problem
{
    class Program
    {
        static void Main(string[] args)
        {
            Graphs g = new Graphs();
            var vertices = new HashSet<object>(GetVertices());
            var edges = new HashSet<(object, object)>(GetEdges());
            var weights = GetWeightFunction();
            Dictionary<object, object> parents;
            Dictionary<object, int> distance;
            g.ShortestWeightedPath(vertices, edges, ref weights, 'A', out parents, out distance);
            List<object> path = new();
            g.PathFromTree(parents, 'E', ref path);
            foreach (object entry in path) {
                Console.WriteLine(entry);
            }
        }

        static object[] GetVertices() {
            object[] output = { 'A', 'B', 'C', 'D', 'E', 'F' };
            return output;
        }

        static (object, object)[] GetEdges() {
            (object, object)[] uniqueEdges = { ('A', 'D'), ('D', 'C'), ('C', 'E'), ('C', 'F'), ('A', 'B'), ('B', 'C'), ('B', 'F') };
            var len = uniqueEdges.Length;
            var output = new (object, object)[2 * len];
            for (int i = 0; i < len; i++) {
                output[i] = uniqueEdges[i];
                output[i + len] = (uniqueEdges[i].Item2, uniqueEdges[i].Item1);
            }
            return output;
        }

        static Dictionary<(object, object), int> GetWeightFunction() {
            Dictionary<(object, object), int> weights = new Dictionary<(object, object), int>();
            weights.Add(('A', 'D'), 2);
            weights.Add(('A', 'B'), 1);
            weights.Add(('D', 'C'), 2);
            weights.Add(('E', 'C'), 4);
            weights.Add(('C', 'F'), 4);
            weights.Add(('B', 'F'), 3);
            weights.Add(('B', 'C'), 5);
            return weights;
        }
    }
}
