using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VisualGraphing {
    class InteractiveGUI {

        class GUIManager {
            public List<MoveableObject> Objects { get; private set; }
            public GUIManager() {
                Objects = new();
            }

            public void AddObject(MoveableObject moveableObject) {
                Objects.Add(moveableObject);
            }

            public void ZoomCanvas(double zoomPercent, Canvas canvas, Point mousePos) {
                double desiredWidth = canvas.Width * zoomPercent;
                double desiredHeight = canvas.Height * zoomPercent;

                var a0 = canvas.Margin.Left;
                var b0 = canvas.Margin.Left + canvas.ActualWidth;
                var a1 = canvas.Margin.Top;
                var b1 = canvas.Margin.Top + canvas.ActualHeight;

                var LRRATIO = GetRatios(a0, b0, mousePos.X);
                var TBRATIO = GetRatios(a1, b1, mousePos.Y);

                double tMarg = (desiredHeight - canvas.Height) * TBRATIO.f;
                double lMarg = (desiredWidth - canvas.Width) * LRRATIO.f;
                double bMarg = (desiredHeight - canvas.Height) * TBRATIO.g;
                double rMarg = (desiredWidth - canvas.Width) * LRRATIO.g;

                double newTop = canvas.Margin.Top - tMarg;
                double newBottom = canvas.Margin.Bottom - bMarg;
                double newLeft = canvas.Margin.Left - lMarg;
                double newRight = canvas.Margin.Right - rMarg;

                canvas.Width = desiredWidth;
                canvas.Height = desiredHeight;
                canvas.Margin = new Thickness(newLeft, newTop, newRight, newBottom);
                //IF THIS CODE EVER BREAKS WITH NaN as the problem
                // You've resized the canvas in the designer.
                // Make sure you've resized it in the properties panel, otherwise you'll see
                // '(Auto)' too many times.
            }

            private (double f, double g) GetRatios(double a, double b, double c)  {
                var f = (c - a) / (b - a);
                var g = (b - c) / (b - a);
                return (f, g);
            }

        }

        class MoveableObject {
            
        }

        Rectangle GetHandle() {
            return new Rectangle();
        }

    }
}
