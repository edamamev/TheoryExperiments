using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortest_Path_Problem
{
    public class Graphs
    {
        #region Neighbourhood of a Vertex
        /// <summary>
        /// Return the neighbourhood of a vertex u in graph (V, E)
        /// </summary>
        /// <param name="V"></param>
        /// <param name="E"></param>
        /// <param name="u"></param>
        /// <returns></returns>
        HashSet<object> N(HashSet<object> V, HashSet<(object, object)> E, object u) {
            var neighbourhood = new HashSet<object>();
            foreach (object v in V) {
                if (E.Contains((u, v))){
                    neighbourhood.Add(v);
                }
            }
            return neighbourhood;
        }
        #endregion

        #region Neighbourhood of a set of Vertices
        /// <summary>
        /// Return the neighbourhood of a set of vertices S in graph (V, E)
        /// </summary>
        /// <param name="V"></param>
        /// <param name="E"></param>
        /// <param name="S"></param>
        /// <returns></returns>
        HashSet<object> NS(HashSet<object> V, HashSet<(object, object)> E, HashSet<object> S) {
            var neighbourhood = new HashSet<object>();
            foreach (object v in V) {
                foreach (object u in S) {
                    if (E.Contains((u, v))) {
                        neighbourhood.Add(v);
                    }
                }
            }
            return neighbourhood;
        }
        #endregion

        #region Distance Classes
        List<HashSet<object>> DistanceClasses(HashSet<object> V, HashSet<(object, object)> E, object u) {
            HashSet<object> uSet = new HashSet<object>();
            uSet.Add(u);
            var D = new List<HashSet<object>>();
            return DistanceClassesR(V, E, D);
        }

        List<HashSet<object>> DistanceClassesR(HashSet<object> V, HashSet<(object, object)> E, List<HashSet<object>> D) {
            var lastClass = D[D.Count];
            var Vnew = new HashSet<object>();
            foreach (var item in lastClass) {
                Vnew.Remove(item);
            }
            if (Vnew.Count == 0) return D;
            D.Add(NS(V, E, lastClass));
            return DistanceClassesR(V, E, D);
        }
        #endregion

        #region Span Tree
        public Dictionary<object, object> SpanTree(HashSet<object> V, HashSet<(object, object)> E, object r) {
            var parents = new Dictionary<object, object>();
            V.Remove(r);
            var D = new HashSet<object>();
            D.Add(r);
            parents.Add(r, null);
            SpanTreeR(ref V, ref E, ref D, ref parents);
            return parents;
        }

        void SpanTreeR(ref HashSet<object> V, ref HashSet<(object, object)> E, ref HashSet<object> D, ref Dictionary<object, object> parents) {
            var Dnew = NS(V, E, D);
            if (Dnew.Count == 0) return;
            foreach (object v in Dnew) {
                parents[v] = N(D, E, v).ToArray()[0];
            }
            foreach (object v in Dnew) {
                V.Remove(v);
            }
            SpanTreeR(ref V, ref E, ref Dnew, ref parents);
        }

        public void PathFromTree(Dictionary<object, object> parents, object destination, ref List<object> path) {
            var parent = parents[destination];
            if (parent == null) return;
            path.Add(parent);
            PathFromTree(parents, parent, ref path);
        }
        #endregion

        #region Dijkstra's Algorithm
        
        /// <summary>
        /// Given a graph (V, E) with a weight function, returns functions 'parents' and 'distance' for future pathfinding.
        /// </summary>
        /// <param name="V">A Set of Vertices.</param>
        /// <param name="E">A Set of Edges.</param>
        /// <param name="weights">A Weight Function of 'E'.</param>
        /// <param name="root">The root vertex.</param>
        /// <param name="parents">Relates vertex 'u' to 'v' such that the relation is the shortest path to the root.</param>
        /// <param name="distance">Relates vertex 'u' to a total weighted distance from the root.</param>
        public void ShortestWeightedPath(HashSet<object> V, HashSet<(object, object)> E, ref Dictionary<(object, object), int> weights, object root, out Dictionary<object, object> parents, out Dictionary<object, int> distance) {
            //Setup
            HashSet<object> S = new ();
            distance = new();
            List<object> Q = new();
            parents = new();
            foreach (object v in V) {
                Q.Add(v);
            }
            foreach (object v in V) {
                distance[v] = int.MaxValue;
            }
            parents[root] = null;
            distance[root] = 0;
            SortQueueByWeight(ref Q, distance);
            object w;

            //Dijkstras Algorithm
            while (true) {
                // 'w' being the next vertice to check.
                // Currently set to the next shortest path in the priority queue 'Q'
                // Up for optimisation, but works for now
                w = Q[0];
                var vN = validNeighbours(V, E, S, w);
                // Foreach neighbour of 'w' that hasn't had it's shortest path solved.
                foreach (object v in vN) {
                    var dist = distance[w] + GetWeightOfEdge((v, w), ref weights);
                    if (distance[v] >= dist) {
                        distance[v] = dist;
                        parents[v] = w;
                    }
                }
                // Reorganise Priority Queue by the next node with the shortest current distance.
                SortQueueByWeight(ref Q, distance);
                S.Add(w);
                Q.Remove(w);
                if (Q.Count == 0) break;
            }
        }

        public void SortQueueByWeight(ref List<object> Q, Dictionary<object, int> t){
            Q = Q.OrderBy(x => t[x]).ToList();
        }

        public void RemoveSolvedVertsFromQueue(ref List<object> Q, HashSet<object> S) {
            for (int i = 0; i < Q.Count; i++) {
                if (S.Contains(Q[i])) {
                    Q.Remove(Q[i]);
                }
            }
        }
        HashSet<object> validNeighbours(HashSet<object> V, HashSet<(object, object)> E, HashSet<object> S, object u) {
            HashSet<object> neighbours = new();
            foreach (object neighbour in N(V, E, u)) {
                if (!S.Contains(neighbour)) neighbours.Add(neighbour);
            }
            return neighbours;
        }

        int GetWeightOfEdge((object, object) edge, ref Dictionary<(object, object), int> weights) {
            if (weights.ContainsKey(edge)) return weights[edge];
            return weights[(edge.Item2, edge.Item1)];
        }
        #endregion
    }
}
