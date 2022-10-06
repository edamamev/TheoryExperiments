using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

public struct Vertex {
    public int X;
    public int Y;
    public string Data;

    public Vertex(string data, int xPos = 0, int yPos = 0) {
        Data = data;
        X = xPos;
        Y = yPos;
    }
}

public class Graph {
    public HashSet<Vertex> Vertices { get; private set; }
    public HashSet<(Vertex, Vertex)> Edges { get; private set; }
    public HashSet<(Vertex, Vertex)> EdgesPrime { get; private set; }
    public Graph() {
        Vertices = new();
        Edges = new();
        EdgesPrime = new();
    }

    #region Vertex Management
    public bool AddVertex(Vertex vertex) {
        if (Vertices.Contains(vertex)) return false;
        Vertices.Add(vertex);
        return true;
    }

    public bool RemoveVertex(Vertex vertex) {
        if (!Vertices.Contains(vertex)) return false;
        Vertices.Remove(vertex);
        return true;
    }
    #endregion

    #region Edge Management
    public HashSet<(Vertex, Vertex)> GetEdgeMasterSet() {
        var masterSet = Edges;
        masterSet.UnionWith(EdgesPrime);
        return masterSet;
    }

    public bool AddEdge((Vertex, Vertex) edge) {
        if (!Edges.Contains(edge)) {
            if (!EdgesPrime.Contains(edge)) {
                Edges.Add(edge);
                EdgesPrime.Add((edge.Item2, edge.Item1));
                return true;
            }
        }
        return false;
    }

    public bool RemoveEdge((Vertex, Vertex) edge) {
        var edgePrime = (edge.Item2, edge.Item1);
        if (Edges.Contains(edge)) {
            Edges.Remove(edge);
            EdgesPrime.Remove(edgePrime);
        }
        if (EdgesPrime.Contains(edge)) {
            EdgesPrime.Remove(edge);
            Edges.Remove(edgePrime);
            return true;
        }
        return false;
    }
    #endregion

    #region Display

    public void DisplayGraph(ref Grid grid, int vertSize) {
        foreach ((Vertex, Vertex) edge in Edges) {
            grid.Children.Add(GetEdgeLine(edge.Item1, edge.Item2, vertSize/2));
        }
        foreach (Vertex vert in Vertices) {
            grid.Children.Add(GetVertDisplay(vert, vertSize));
        }
    }

    private Line GetEdgeLine(Vertex v1, Vertex v2, int offset) {
        Line line = new();
        line.X1 = v1.X + offset;
        line.Y1 = v1.Y + offset;
        line.X2 = v2.X + offset;
        line.Y2 = v2.Y + offset;
        line.Stroke = new SolidColorBrush(Color.FromRgb(255,0,0));
        line.StrokeThickness = 2;
        return line;
    }

    private Rectangle GetVertDisplay(Vertex vert, int vertSize) {
        Rectangle rect = new();
        rect.Margin = new Thickness(vert.X, vert.Y, 0, 0);
        rect.Name = vert.Data;
        rect.HorizontalAlignment = HorizontalAlignment.Left;
        rect.VerticalAlignment = VerticalAlignment.Top;
        rect.Width = vertSize;
        rect.Height = vertSize;
        rect.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        return rect;
    }

    #endregion
}
