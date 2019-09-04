using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        DICOMObject dicomDisplayed = null;
        public DisplayManagement(System.Windows.Controls.Image original, System.Windows.Controls.Image processed)
        {
            this.original = original; //Passes the objects that this class will be changing.
            this.processed = processed;
        }

        public void PreviewImage(Bitmap image)
        {
            BitmapSource ims = Imaging.CreateBitmapSourceFromHBitmap(image.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            using (var fileStream = new FileStream("C:/Users/Thinkchicken/Desktop/bitmap.png", FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(ims));
                encoder.Save(fileStream);
                Console.WriteLine("Saving file");
            }
            //ims.
            original.Source = ims;
        }

        public static async Task<Bitmap> DICOMToBmpAsync(DICOMObject dicom)
        {
            return await Task.Factory.StartNew(() => DICOMToBmp(dicom));
        }
        public static Bitmap DICOMToBmp(DICOMObject dicom)
        {
            SimpleDicom wrapper = new SimpleDicom(dicom);

            int width = wrapper.GetDimensionX();
            int length = wrapper.GetDimensionY();

            List<byte> stream = (List<byte>)dicom.FindFirst(TagHelper.PixelData).DData_;

            Console.WriteLine("-=-" + Environment.NewLine + "Beginning read from DICOM file" + Environment.NewLine + "Length: " + length + Environment.NewLine + "Width: " + width + "\nContaining a total of " + stream.Count + "\n-=-");

            Bitmap bmp = new Bitmap(width, length);

            int pixel = 0;
            // 0 > 256
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < length; y++)
                {
                    //pixel = x + y * width;
                    Console.WriteLine(stream[pixel]);
                    System.Drawing.Color color = System.Drawing.Color.FromArgb(stream[pixel], stream[pixel], stream[pixel]);
                    bmp.SetPixel(x, y, color);
                    pixel++;
                }
            }
            return bmp;
        }
    }
}
