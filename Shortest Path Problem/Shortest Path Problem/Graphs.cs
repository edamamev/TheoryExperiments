using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortest_Path_Problem
{
    public class Graph {

        public HashSet<object> Vertices { get; private set; }
        public HashSet<(object, object)> Edges { get; private set; }

        #region Constructors
        public Graph() { }

        public Graph(HashSet<object> Vertices = null, HashSet<(object, object)> Edges = null) {
            this.Vertices = Vertices;
            this.Edges = Edges;
        }
        #endregion

        #region Conversions
        public WeightedGraph ToWeightedGraph() {
            WeightedGraph weightedGraph = new(this);
            weightedGraph.SetGraph(this);
            return weightedGraph;
        }
        #endregion

        #region Vertex and Edge Management
        public virtual void SetGraph(Graph graph) {
            Vertices = graph.Vertices;
            Edges = graph.Edges;
        }

        public virtual void AddVertex(object vertex) {
            Vertices.Add(vertex);
        }

        public virtual void RemoveVertex(object vertex) {
            Vertices.Remove(vertex);
            foreach ((object, object) edge in Edges) {
                if (edge.Item1 == vertex) { Edges.Remove(edge); continue; }
                if (edge.Item2 == vertex) { Edges.Remove(edge); continue; }
                //Catch Error Here
            }
        }

        public virtual void AddEdge((object, object) edge) {
            if (!Vertices.Contains(edge.Item1) && !Vertices.Contains(edge.Item2)) { /*Catch Error*/ return; }
            Edges.Add(edge);
        }

        public virtual void RemoveEdge((object, object) edge) {
            Edges.Remove(edge);
        }
        #endregion

        #region Neighbourhoods
        public HashSet<object> N(HashSet<object> V, object u)
        {
            var neighbourhood = new HashSet<object>();
            foreach (object v in V)
            {
                if (Edges.Contains((u, v)))
                {
                    neighbourhood.Add(v);
                }
            }
            return neighbourhood;
        }

        HashSet<object> NS(HashSet<object> S)
        {
            var neighbourhood = new HashSet<object>();
            foreach (object v in Vertices)
            {
                foreach (object u in S)
                {
                    if (Edges.Contains((u, v)))
                    {
                        neighbourhood.Add(v);
                    }
                }
            }
            return neighbourhood;
        }
        #endregion

        #region SSP
        public List<object> SolveShortestPath(object root, object destination)
        {
            var parents = SpanTree(Vertices, Edges, root);
            List<object> shortestPath = new();
            PathFromTree(parents, destination, ref shortestPath);
            return shortestPath;
        }

        void PathFromTree(Dictionary<object, object> parents, object destination, ref List<object> path)
        {
            var parent = parents[destination];
            if (parent == null) return;
            path.Add(parent);
            PathFromTree(parents, parent, ref path);
        }
        #endregion

        #region DistanceClasses
        public List<HashSet<object>> GetDistanceClasses(object u) {
            HashSet<object> uSet = new HashSet<object>();
            uSet.Add(u);
            var D = new List<HashSet<object>>();
            return DistanceClassesR(Vertices, Edges, D);
        }

        List<HashSet<object>> DistanceClassesR(HashSet<object> V, HashSet<(object, object)> E, List<HashSet<object>> D) {
            var lastClass = D[D.Count];
            var Vnew = new HashSet<object>();
            foreach (var item in lastClass)
            {
                Vnew.Remove(item);
            }
            if (Vnew.Count == 0) return D;
            D.Add(NS(lastClass));
            return DistanceClassesR(Vertices, Edges, D);
        }
        #endregion

        #region Span Tree
        Dictionary<object, object> SpanTree(HashSet<object> V, HashSet<(object, object)> E, object r)
        {
            var parents = new Dictionary<object, object>();
            V.Remove(r);
            var D = new HashSet<object>();
            D.Add(r);
            parents.Add(r, null);
            SpanTreeR(ref V, ref E, ref D, ref parents);
            return parents;
        }

        void SpanTreeR(ref HashSet<object> V, ref HashSet<(object, object)> E, ref HashSet<object> D, ref Dictionary<object, object> parents)
        {
            var Dnew = NS(D);
            if (Dnew.Count == 0) return;
            foreach (object v in Dnew)
            {
                parents[v] = N(Vertices, v).ToArray()[0];
            }
            foreach (object v in Dnew)
            {
                V.Remove(v);
            }
            SpanTreeR(ref V, ref E, ref Dnew, ref parents);
        }
        #endregion

    }

    public class WeightedGraph : Graph {

        public Dictionary<(object, object), float> Weights { get; private set; }

        #region Constructors
        public WeightedGraph() { }

        public WeightedGraph(Graph graph = null) {
            SetGraph(graph);
        }

        public WeightedGraph(HashSet<object> Vertices = null, HashSet<(object, object)> Edges = null, Dictionary<(object, object), float> Weights = null) {
            SetGraph(new Graph(Vertices, Edges));
            this.Weights = Weights;
        }
        #endregion

        #region Overrides
        public override void SetGraph(Graph graph) {
            base.SetGraph(graph);
            foreach ((object, object) edge in Edges)
            {
                Weights[edge] = int.MaxValue;
            }
        }

        public override void AddEdge((object, object) edge) {
            base.AddEdge(edge);
            Weights[edge] = int.MaxValue;
        }

        public override void RemoveEdge((object, object) edge) {
            base.RemoveEdge(edge);
            Weights.Remove(edge);
        }

        #endregion

        #region Conversions
        public Graph ToGraph() {
            Graph graph = new();
            graph.SetGraph(this);
            return graph;
        }
        #endregion

        #region Weight Management
        public float GetWeight((object, object) edge) {
            return Weights[edge];
        }

        public void UpdateWeight((object, object) edge, float value) {
            Weights[edge] = value;
        }
        #endregion

        #region Weighted Pathfinding
        /// <summary>
        /// Djikstra's Terminating once the whole graph has been processed.
        /// </summary>
        /// <param name="weights"></param>
        /// <param name="root"></param>
        /// <param name="parents"></param>
        /// <param name="distance"></param>
        public void SpanShortestWeightedPath(ref Dictionary<(object, object), float> weights, object root, out Dictionary<object, object> parents, out Dictionary<object, float> distance)
        {
            //Setup
            HashSet<object> S = new();
            distance = new();
            List<object> Q = new();
            parents = new();
            foreach (object v in Vertices)
            {
                Q.Add(v);
            }
            foreach (object v in Vertices)
            {
                distance[v] = int.MaxValue;
            }
            parents[root] = null;
            distance[root] = 0;
            SortQueueByWeight(ref Q, distance);
            object w;

            //Dijkstras Algorithm
            while (true)
            {
                // 'w' being the next vertice to check.
                // Currently set to the next shortest path in the priority queue 'Q'
                w = Q[0];
                var vN = validNeighbours(Vertices, S, w);
                // Foreach neighbour of 'w' that hasn't had it's shortest path solved.
                foreach (object v in vN)
                {
                    var dist = distance[w] + GetWeightOfEdge((v, w), weights);
                    if (distance[v] >= dist)
                    {
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
        
        /// <summary>
        /// Djikstra's Terminating once the destination has been processed.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="destination"></param>
        /// <param name="parents"></param>
        /// <param name="distance"></param>
        public void ShortestWeightedPathToDestination(object root, object destination, out Dictionary<object, object> parents, out Dictionary<object, float> distance)
        {
            //Setup
            HashSet<object> S = new();
            distance = new();
            List<object> Q = new();
            parents = new();
            foreach (object v in Vertices)
            {
                Q.Add(v);
            }
            foreach (object v in Vertices)
            {
                distance[v] = int.MaxValue;
            }
            parents[root] = null;
            distance[root] = 0;
            SortQueueByWeight(ref Q, distance);
            object w;

            //Dijkstras Algorithm
            while (true)
            {
                // 'w' being the next vertice to check.
                // Currently set to the next shortest path in the priority queue 'Q'
                w = Q[0];
                var vN = validNeighbours(Vertices, S, w);
                // Foreach neighbour of 'w' that hasn't had it's shortest path solved.
                foreach (object v in vN)
                {
                    var dist = distance[w] + GetWeightOfEdge((v, w), Weights);
                    if (distance[v] >= dist)
                    {
                        distance[v] = dist;
                        parents[v] = w;
                    }
                }
                // Reorganise Priority Queue by the next node with the shortest current distance.
                SortQueueByWeight(ref Q, distance);
                S.Add(w);
                Q.Remove(w);
                if (w == destination) break;
            }
        }
        #endregion

        #region Supporting Functions
        public void SortQueueByWeight(ref List<object> Q, Dictionary<object, float> t)
        {
            Q = Q.OrderBy(x => t[x]).ToList();
        }

        public void RemoveSolvedVertsFromQueue(ref List<object> Q, HashSet<object> S)
        {
            for (int i = 0; i < Q.Count; i++)
            {
                if (S.Contains(Q[i]))
                {
                    Q.Remove(Q[i]);
                }
            }
        }

        HashSet<object> validNeighbours(HashSet<object> V, HashSet<object> S, object u)
        {
            HashSet<object> neighbours = new();
            foreach (object neighbour in N(V, u))
            {
                if (!S.Contains(neighbour)) neighbours.Add(neighbour);
            }
            return neighbours;
        }

        float GetWeightOfEdge((object, object) edge, Dictionary<(object, object), float> weights)
        {
            if (weights.ContainsKey(edge)) return weights[edge];
            return weights[(edge.Item2, edge.Item1)];
        }
        #endregion
    }

    public class HeuristicGraph : WeightedGraph {
        /// <summary>
        /// I think something important to note which was very only briefly suggested is that if your distance-to-goal heuristic always underestimates you will always find the shortest path, but if not then the path you get may not be the shortest (which for some problems may be suitable).
        /// If you underestimate too much then the benefits of A* diminish and you'll explore more and more of the graph. Additionally, Dijkstra is a generalisation of A* where the distance-to-goal is always underestimated as 0.
        /// </summary>
        public Dictionary<object, float> a;

    }
    
}
