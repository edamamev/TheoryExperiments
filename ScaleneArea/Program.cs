using System;
using System.Collections.Generic;

namespace ScaleneArea {

    internal class Program {
        
        // Only points that are a part of the Triangle
        public static Dictionary<char, (double x, double y)> points;
        public static HashSet<(char, char)> edges;

        static void Main(string[] args)
        {
            // Initialise Points and Edges
            points = new();
            points.Add('A', (2, 5));
            points.Add('B', (1, 1));
            points.Add('C', (5, 2));

            edges = new();
            edges.Add(('A', 'C'));
            edges.Add(('A', 'B'));
            edges.Add(('B', 'C'));
            // End P&E Init
            Dictionary<(char,char), double> sideLengths = new();

            // Find the base of the triangle (Edge with longest length)
            (char, char) baseSide = new();
            bool baseEmpty = true;
            foreach ((char, char) edge in edges) {
                sideLengths.Add(edge, GetSideLength(edge));
                if (baseEmpty) {
                    baseSide = edge;
                    baseEmpty = false;
                }
                else {
                    if (sideLengths[edge] > sideLengths[baseSide]) {
                        baseSide = edge;
                    }
                }
            }

            // x) Find the linear equation for that sidelength
            (double gradient, double heightAdjustment) baseLinearEquation = (0,0);
            baseLinearEquation.gradient = RiseOverRun(points[baseSide.Item1], points[baseSide.Item2]);
            // Example Point
            (double x, double y) examplePoint = points[baseSide.Item1];
            baseLinearEquation.heightAdjustment = examplePoint.y - LinearEquation(examplePoint.x, baseLinearEquation.gradient, 0);

            // x + 1) Find the linear equation for baseLength Perpendicular
            (double gradient, double heightAdjustment) perpLinearEquation = (0,0);
            perpLinearEquation.gradient = (-1 / baseLinearEquation.gradient);
            // Example Point
            examplePoint = points[GetComplimentaryPoint(baseSide)];
            perpLinearEquation.heightAdjustment = examplePoint.y - LinearEquation(examplePoint.x, perpLinearEquation.gradient, 0);

            // Now to find our new point, point D.
            points.Add('D', IntersectCoordinates(baseLinearEquation, perpLinearEquation));
            double triangleArea = 0;
            triangleArea = CalculateTriangleArea(sideLengths[baseSide], CalculateSideLength(examplePoint, points['D']));
            Console.WriteLine("Triangle Area = " + triangleArea.ToString());

        }

        static double GetSideLength((char, char) edge) {
            (double, double) point0 = points[edge.Item1];
            (double, double) point1 = points[edge.Item2];
            return CalculateSideLength(point0, point1);
        }

        static double CalculateSideLength((double x, double y) point0, (double x, double y) point1) {
            double abstract0 = Math.Pow((Math.Max(point0.Item1, point1.Item1) - Math.Min(point0.Item1, point1.Item1)), 2);
            double abstract1 = Math.Pow((Math.Max(point0.Item2, point1.Item2) - Math.Min(point0.Item2, point1.Item2)), 2);
            return Math.Sqrt(abstract0 + abstract1);
        }

        static double RiseOverRun((double x, double y) point0, (double x, double y) point1) {
            // Change in Height
            // ----------------
            // Change in Distance
            double changeInHeight = (Math.Max(point0.y, point1.y) - Math.Min(point0.y, point1.y));
            double changeInDistance = (Math.Max(point0.x, point1.x) - Math.Min(point0.x, point1.x));
            return changeInHeight / changeInDistance;
        }

        static double LinearEquation(double input, double gradient, double heightAdjustment) {
            double output = input * gradient;
            output += heightAdjustment;
            return output;
        }

        // Uses Point of Intersection formula.
        // https://www.vedantu.com/formula/point-of-intersection-formula
        static (double, double) IntersectCoordinates((double gradient, double heightAdjustment) baseEquation, (double gradient, double heightAdjustment) perpEquation) {
            (double a, double b, double c) v1 = (baseEquation.gradient, -1, baseEquation.heightAdjustment);
            (double a, double b, double c) v2 = (perpEquation.gradient, -1, perpEquation.heightAdjustment);
            double intX = ((v1.b * v2.c) - (v2.b * v1.c)) / ((v1.a * v2.b) - (v2.a * v1.b));
            double intY = ((v2.a * v1.c) - (v1.a * v2.c)) / ((v1.a * v2.b) - (v2.a * v1.b));
            return (intX, intY);
        }

        static char GetComplimentaryPoint((char, char) edge) {
            foreach (KeyValuePair<char, (double, double)> entry in points) {
                if (entry.Key != edge.Item1 && entry.Key != edge.Item2) return entry.Key;
            }
            return '.';
        }

        static double CalculateTriangleArea(double b, double h) {
            double area = b * h;
            area *= 0.5;
            return area;
        }

    }
}
