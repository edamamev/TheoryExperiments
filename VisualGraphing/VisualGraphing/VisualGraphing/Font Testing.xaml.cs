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
using System.Windows.Shapes;

namespace VisualGraphing {
    /// <summary>
    /// Interaction logic for Font_Testing.xaml
    /// </summary>
    public partial class Font_Testing : Window {
        public Font_Testing()
        {
            InitializeComponent();
        }

        private void window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Point mousePos = e.GetPosition(grid0);
            double percent = (e.Delta > 0) ? 1.1 : 0.9;
            ZoomCanvas(percent, mousePos);
            ZoomCanvasText(percent, mousePos);
            //ZoomText(percent, e.GetPosition(cnvZoom));

        }

        private void fuckingMouse() {
            
        }

        private void ZoomCanvasText(double zoomPercent, Point mousePos) {

            // Assuming all labels have Canvas.Left and Canvas.Top fields
            double desiredWidth = lbl0.Width * zoomPercent;
            double desiredHeight = lbl0.Height * zoomPercent;

            //Canvas.SetLeft(lbl0, a0 * zoomPercent);
            //Canvas.SetTop(lbl0, a1 * zoomPercent);

            //double tMarg = (desiredHeight - lbl0.Height) * zoomPercent;
            //double lMarg = (desiredWidth - lbl0.Width) * zoomPercent;

            //double newTop = lbl0.Height - tMarg;
            //double newLeft = lbl0.Height - lMarg;

            lbl0.Height = desiredHeight;
            lbl0.Width = desiredWidth;

            var left = Canvas.GetLeft(lbl0);
            var top = Canvas.GetTop(lbl0);

            Canvas.SetLeft(lbl0, left);
            Canvas.SetTop(lbl0, top);

            //Canvas.SetLeft(lbl0, newLeft);
            //Canvas.SetTop(lbl0, newTop);
            lbl0.FontSize = GetNewFontSize(lbl0.FontSize, 100);
        }

        private double GetNewFontSize(double fontSize, double viewPortSize) {
            return (fontSize * 100 / viewPortSize) + 5;
        }

        private void ZoomCanvas(double zoomPercent, Point mousePos)
        {
            double desiredWidth = cnvZoom.Width * zoomPercent;
            double desiredHeight = cnvZoom.Height * zoomPercent;

            var a0 = cnvZoom.Margin.Left;
            var b0 = cnvZoom.Margin.Left + cnvZoom.ActualWidth;
            var a1 = cnvZoom.Margin.Top;
            var b1 = cnvZoom.Margin.Top + cnvZoom.ActualHeight;

            var LRRATIO = GetRatios(a0, b0, mousePos.X);
            var TBRATIO = GetRatios(a1, b1, mousePos.Y);

            double tMarg = (desiredHeight - cnvZoom.Height) * TBRATIO.f;
            double lMarg = (desiredWidth - cnvZoom.Width) * LRRATIO.f;
            double bMarg = (desiredHeight - cnvZoom.Height) * TBRATIO.g;
            double rMarg = (desiredWidth - cnvZoom.Width) * LRRATIO.g;

            double newTop = cnvZoom.Margin.Top - tMarg;
            double newBottom = cnvZoom.Margin.Bottom - bMarg;
            double newLeft = cnvZoom.Margin.Left - lMarg;
            double newRight = cnvZoom.Margin.Right - rMarg;

            //Set the values I spose
            cnvZoom.Width = desiredWidth;
            cnvZoom.Height = desiredHeight;
            cnvZoom.Margin = new Thickness(newLeft, newTop, newRight, newBottom);
            //IF THIS CODE EVER BREAKS WITH NaN as the problem
            // You've resized the canvas in the designer.
            // Make sure you've resized it in the properties panel, otherwise you'll see
            // '(Auto)' too many times.
        }

        /// <summary>
        /// I wish I could explain this code.
        /// If you lose the paper, I stg I will find and kill you from the past.
        /// *sigh*
        /// Fine.
        ///     a       c       b
        ///     |       |       |
        ///     |       |       |
        ///     |       |       |
        ///     0       50      100
        ///     
        ///     a being the lowest bound of the object
        ///     b being the higher bound of the object
        ///     c being the origin point.
        /// 
        ///     This function calculates the ratio between a and b for use in repositioning
        ///     the object after its resizing to create perspective.
        ///     
        ///     a           b       c
        ///     |           |       |
        ///     |           |       |
        ///     |           |       |
        ///     0           100     125
        ///     
        /// 
        /// </summary>
        /// <param name="a">Positive Vectored Line</param>
        /// <param name="b">Negative Vectored Line</param>
        /// <param name="c">Origin</param>
        /// <returns>'f' relates to a, 'g' relates to b</returns>
        private (double f, double g) GetRatios(double a, double b, double c) {
            var f = (c - a) / (b - a);
            var g = (b - c) / (b - a);
            return (f, g);
        }

        private void ZoomText(double zoomPercent) {
            //lbl0.FontSize *= zoomPercent;
            //lbl1.FontSize *= zoomPercent;
            //lbl2.FontSize *= zoomPercent;
            //txtBox0.FontSize *= zoomPercent;
        }
        private Line fuckLine(Point point1, Point point2) {
            var outLine = new Line();
            outLine.X1 = point1.X;
            outLine.Y1 = point1.Y;
            outLine.X2 = point2.X;
            outLine.Y2 = point2.Y;
            outLine.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            outLine.StrokeThickness = 1;
            return outLine;
        }

    }
}
