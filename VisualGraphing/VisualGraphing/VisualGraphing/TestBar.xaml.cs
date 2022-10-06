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

namespace VisualGraphing
{
    /// <summary>
    /// Interaction logic for TestBar.xaml
    /// </summary>
    public partial class TestBar : Window
    {
        bool mouseDown = false;
        Point mouseDownLoc;
        bool rctClicked;
        Point oldMousePos;

        public TestBar()
        {
            InitializeComponent();
            rctClicked = false;
        }

        private void cnvMove_MouseMove(object sender, MouseEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                if (!mouseDown) {
                    mouseDown = true;
                    mouseDownLoc = new Point(Mouse.GetPosition(mainWindow).X, Mouse.GetPosition(mainWindow).Y);
                }
                Point point = new Point(mainWindow.Left + Mouse.GetPosition(mainWindow).X - mouseDownLoc.X, mainWindow.Top + Mouse.GetPosition(mainWindow).Y - mouseDownLoc.Y);
                mainWindow.Left = point.X;
                mainWindow.Top = point.Y;
            }
        }

        private void cnvMove_MouseUP(object sender, MouseButtonEventArgs e) {
            mouseDown = false;
        }
        private void mainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (rctClicked) {
                Point newMousePos = Mouse.GetPosition(cnvMove);
                var difference = CalculatePointDifference(oldMousePos, newMousePos);
                var newRctPos = GetNewRctPos(mainWindow, difference);
                mainWindow.Left = newRctPos.Left;
                mainWindow.Top = newRctPos.Top;
            }
        }

        private void cnvMove_LeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            oldMousePos = e.GetPosition(cnvMove);
            rctClicked = true;
        }

        private void cnvMove_LeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            rctClicked = false;
        }

        Point CalculatePointDifference(Point oldPos, Point newPos) {
            var difference = new Point {
                X = newPos.X - oldPos.X,
                Y = newPos.Y - oldPos.Y
            };
            return difference;
        }

        Thickness GetNewRctPos(Window win, Point diff) {
            double newLeft = win.Left + diff.X;
            double newTop = win.Top + diff.Y;
            return new Thickness(newLeft, newTop, 0, 0);
        }

        double ClampMargin(double input, double maxVal) {
            if (input < 0) {
                return 0;
            }
            if (input > maxVal){
                return maxVal;
            }
            return input;
        }

    }
}
