
using System;
using System.Collections.Generic;

namespace Shortest_Path_Problem
{
    class Program
    {
        static void Main(string[] args)
        {
            WeightedGraph g = new(GetVertices(), GetEdges(), GetWeightFunction());

            var vertices = new HashSet<object>(GetVertices());
            var edges = new HashSet<(object, object)>(GetEdges());
            var weights = GetWeightFunction();
            Dictionary<object, object> parents;
            Dictionary<object, int> distance;
            g.
            g.ShortestWeightedPathToDestination();
            g.ShortestWeightedPath(vertices, edges, ref weights, 'A', out parents, out distance);
            List<object> path = new();
            g.PathFromTree(parents, 'E', ref path);
            foreach (object entry in path) {
                Console.WriteLine(entry);
            }
        }

        static HashSet<object> GetVertices() {
            object[] verts = { 'A', 'B', 'C', 'D', 'E', 'F' };
            HashSet<object> output = new();
            foreach (object vert in verts) {
                output.Add(vert);
            }
            return output;
        }

        static HashSet<(object, object)> GetEdges() {
            (object, object)[] uniqueEdges = { ('A', 'D'), ('D', 'C'), ('C', 'E'), ('C', 'F'), ('A', 'B'), ('B', 'C'), ('B', 'F') };
            HashSet<(object, object)> output = new();
            foreach ((object, object) edge in uniqueEdges) {
                output.Add((edge.Item1, edge.Item2));
                output.Add((edge.Item2, edge.Item1));
            }
            return output;
        }

        static Dictionary<(object, object), float> GetWeightFunction() {
            Dictionary<(object, object), float> weights = new Dictionary<(object, object), float>();
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
