using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Shapes;


public class WSAssets{
    public class WSManager{
    
    Canvas camera {get; set;} //Update Access in future
    double currentZoom;
    List<WSNote> notes;
    double pixelsPerUnit;
    int zoomPresets = {200, 150, 125, 100, 80, 60, 40, 20, 10};

    public WSManager(Canvas Camera, double PixelsPerUnit = 100;){
        camera = Camera;
        currentZoom = 100;
        notes = new();
        pixelsPerUnit = PixelsPerUnit;
    }
    

    Point oldMousePos;

    public void CameraZoom(bool scrolledZoom = false, bool delta = false;){
        if (scrolledZoom){
            if ()
        }
    }

    private void rctMove_MouseLeftButtonDown(object sender, MouseEventButtonArgs e){
        oldMousePos = Mouse.GetPosition();
    }


}

    public class WSNote{
        public WSNote(){

        }
    }

    #region Shape Prefabs
    public Line GridLine {
        var line = new();
        line.Stroke = new SolidColorBrush(Color.FromRgb(40, 40, 40));
        line.StrokeThickness = 0.1;
        return line;
    }
    #endregion
}