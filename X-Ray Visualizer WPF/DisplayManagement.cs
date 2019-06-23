﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EvilDICOM.Core;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Image;


namespace X_Ray_Visualizer_WPF
{
    class DisplayManagement
    {
        System.Windows.Controls.Image original;
        System.Windows.Controls.Image processed;
        public DisplayManagement(System.Windows.Controls.Image original, System.Windows.Controls.Image processed)
        {
            this.original = original;
            this.processed = processed;
        }

        public void PreviewImage(Bitmap image)
        {
            ImageSource ims = Imaging.CreateBitmapSourceFromHBitmap(image.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            original.Source = ims;
        }

        public static Bitmap DICOMToBmp(DICOMObject dicom)
        {
            int width = DimensionX(dicom);
            int length = DimensionY(dicom);
            byte[] stream = dicom.PixelStream.ToArray();

            Console.WriteLine("-=-" + Environment.NewLine + "Beginning read from DICOM file" + Environment.NewLine + "Length: " + length + Environment.NewLine + "Width: " + width + "\nContaining a total of " + stream.Length + "\n-=-");
            

            Bitmap bmp = new Bitmap(width, length);

            int pixel = 0;
            // 0 > 256
            for(int x = 0; x < width; x++)
            {
                Console.WriteLine("x " + x);
                for (int y = 0; y < length; y++)
                {
                    Console.WriteLine(stream[pixel]);
                    System.Drawing.Color color = System.Drawing.Color.FromArgb(stream[pixel], stream[pixel], stream[pixel]);
                    bmp.SetPixel(x, y, color);
                }
            }
            return bmp;
        }

        public static int DimensionX (DICOMObject dicom)
        {
            return Convert.ToInt32(dicom.FindFirst(TagHelper.Columns.CompleteID).DData);
        }

        public static int DimensionY(DICOMObject dicom)
        {
            return Convert.ToInt32(dicom.FindFirst(TagHelper.Rows.CompleteID).DData);
        }
    }
}
