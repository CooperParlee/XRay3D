using System;
using OpenCvSharp;
using System.Windows.Media.Imaging;
using OpenCvSharp.Extensions;
using System.Drawing;
using System.Drawing.Imaging;

public class OpenCVTracer
{
	public static BitmapSource CVTrace(string uri)
    {
        /*
         * Based upon implementation found here:
         * https://www.codeproject.com/Articles/1085960/Getting-Started-With-OpenCvSharp
        */
        Mat src = new Mat(uri, ImreadModes.Color); //This image would NOT be in color, but we will do the conversion anyways
        Mat grayFilter = new Mat();
        Mat clearEdge = new Mat();
        Mat filtered = new Mat();

        Mat canny = new Mat();

        //TODO: Create scrollbar for hystersis threshold values below.
        Cv2.Canny(src, canny, 32, 192);
        Bitmap bmpOut = canny.ToBitmap();
        bmpOut.Save(String.Format(uri + " {0}.bmp", DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss")), ImageFormat.Bmp);
        return null;
    } 
}
