using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VisualGraphing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Rectangle> rectangles;
        private List<Line> lines;
        public bool rctClicked;
        public bool rct2Clicked;
        Point oldMousePos;

        public MainWindow()
        {
            InitializeComponent();
            rectangles = new();
            lines = new();
            rctClicked = false;
        }

        private void btnPnlAdd_Click(object sender, RoutedEventArgs e)
        {
            var newRect = testRect(rectangles.Count);
            rectangles.Add(newRect);
            grid0.Children.Add(newRect);
        }

        private Rectangle testRect(int count)
        {
            Rectangle newRect = new();
            newRect.Margin = new Thickness(count * 20, count * 20, 0, 0);
            newRect.Name = "rct" + count;
            newRect.Width = 20;
            newRect.Height = 20;
            newRect.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 255));
            return newRect;
        }

        private void btnRctRmv_Click(object sender, RoutedEventArgs e)
        {
            if (rectangles.Count != 0) {
                grid0.Children.Remove(rectangles[0]);
                for (int i = 1; i < rectangles.Count; i++) {
                    rectangles[i - 1] = rectangles[i];
                }
                rectangles.RemoveAt(rectangles.Count - 1);
            }
        }

        private void btnLine_Click(object sender, RoutedEventArgs e)
        {
            grid0.Children.Add(testLine());
        }

        private Line testLine() {
            var line = new Line();
            line.X1 = 5;
            line.Y1 = 5;
            line.X2 = 100;
            line.Y2 = 100;
            line.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            line.StrokeThickness = 4;
            return line;
        }

        private void btnGraphView_Click(object sender, RoutedEventArgs e)
        {
            Graph graph = new Graph();
            // Verts
            List<Vertex> verts = new();
            var vertA = new Vertex("A", 5, 5);
            var vertB = new Vertex("B", 100, 100);
            var vertC = new Vertex("C", 200, 37);
            var vertD = new Vertex("D", 20, 70);
            verts.Add(vertA);
            verts.Add(vertB);
            verts.Add(vertC);
            verts.Add(vertD);
            foreach (Vertex v in verts) {
                graph.AddVertex(v);
            }
            //Edges
            List<(Vertex, Vertex)> edges = new();
            edges.Add((vertA, vertB));
            edges.Add((vertB, vertC));
            edges.Add((vertA, vertD));
            foreach (var edge in edges) {
                graph.AddEdge(edge);
            }
            //Closing
            graph.DisplayGraph(ref grdGraph, 20);
        }

        #region MovePanel

        private void rctMove_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            oldMousePos = Mouse.GetPosition(rctMove);
            rctClicked = true;
        }

        private void rctMove_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            rctClicked = false;
        }

        private void window_MouseMove(object sender, MouseEventArgs e)
        {
            // Controls rctMovement
            if (rctClicked) {
                Point newMousePos = Mouse.GetPosition(rctMove);
                var difference = CalculatePointDifference(oldMousePos, newMousePos);
                var newRctPos = GetNewRctPos(rctMove, difference);
                var newRctContentPos = GetNewRctPos(rctMoveContent, difference);
                rctMove.Margin = newRctPos;
                rctMoveContent.Margin = newRctContentPos;
                //oldMousePos = newMousePos;
            }

            if (rct2Clicked) {
                Point newMousePos = Mouse.GetPosition(cnvMove);
                var difference = CalculatePointDifference(oldMousePos, newMousePos);
                var newRctPos = GetNewRctPos2(rctMove2, difference);
                Canvas.SetLeft(rctMove2, newRctPos.Left);
                Canvas.SetTop(rctMove2, newRctPos.Top);
            }
        }

        Point CalculatePointDifference(Point oldPos, Point newPos)
        {
            var difference = new Point {
                X = newPos.X - oldPos.X,
                Y = newPos.Y - oldPos.Y
            };
            return difference;
        }

        Thickness GetNewRctPos(Rectangle rct, Point diff)
        {
            double newLeft = ClampMargin(rct.Margin.Left + diff.X, grdMove.Width - rct.Width);
            double newTop = ClampMargin(rct.Margin.Top + diff.Y, grdMove.Height - rct.Height);
            return new Thickness(newLeft, newTop, 0, 0);
        }

        double ClampMargin(double input, double maxVal) {
            if (input < 0) {
                return 0;
            }
            if (input > maxVal) {
                return maxVal;
            }
            return input;
        }
        #endregion

        #region Move Panel 2
        private void rctMove2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            oldMousePos = Mouse.GetPosition(rctMove2);
            rct2Clicked = true;
        }

        private void rctMove2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            rct2Clicked = false;
        }

        Thickness GetNewRctPos2(Rectangle rct, Point diff)
        {
            double newLeft = ClampMargin(rct.Margin.Left + diff.X, cnvMove.Width - rct.Width);
            double newTop = ClampMargin(rct.Margin.Top + diff.Y, cnvMove.Height - rct.Height);
            return new Thickness(newLeft, newTop, 0, 0);
        }
        #endregion

        private void btnTestBar_Click(object sender, RoutedEventArgs e)
        {
            TestBar tstBar = new();
            tstBar.Show();
        }

        private void btnZoom_Click(object sender, RoutedEventArgs e)
        {
            Font_Testing fontShit = new Font_Testing();
            fontShit.Show();
        }
    }
}
