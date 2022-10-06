using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VisualGraphing
{
    public class CanvasManager {

        public Canvas canvas;
        public List<Note> notes;


        public CanvasManager(Canvas canvas)
        {
            this.canvas = canvas;
            notes = new();
        }


    }

    public class CanvasObject {
        public Point worldCoordinates;
        public Point maxBound;
        public Rectangle display;

        public CanvasObject() {
            display = GetDefaultRect();
        }

        private Rectangle GetDefaultRect() {
            Rectangle newRect = new() {
                Width = 20,
                Height = 20,
                Fill = new SolidColorBrush(Color.FromRgb(0, 0, 255))
            };
            return newRect;
        }
    }

    public class Note : CanvasObject {
    }
}
