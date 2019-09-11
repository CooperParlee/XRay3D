using Dicom;
using Dicom.Imaging;
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


namespace X_Ray_Visualizer_WPF
{
    class DisplayManagement
    {
        System.Windows.Controls.Image original;
        System.Windows.Controls.Image processed;

        public DisplayManagement(System.Windows.Controls.Image original, System.Windows.Controls.Image processed)
        {
            ImageManager.SetImplementation(WPFImageManager.Instance);

            this.original = original; //Passes the objects that this class will be changing.
            this.processed = processed;
        }

        public BitmapImage ConvertWriteableBitmapToBitmapImage(WriteableBitmap wbm)
        {
            BitmapImage bmImage = new BitmapImage();
            using (MemoryStream stream = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(wbm));
                encoder.Save(stream);
                bmImage.BeginInit();
                bmImage.CacheOption = BitmapCacheOption.OnLoad;
                bmImage.StreamSource = stream;
                bmImage.EndInit();
                bmImage.Freeze();
            }
            return bmImage;
        }

        public void PreviewImage(WriteableBitmap image)
        {
            //BitmapSource ims = Imaging.CreateBitmapSourceFromHBitmap(image.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            BitmapSource ims = ConvertWriteableBitmapToBitmapImage(image);
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

        public static WriteableBitmap DICOMToBmp(DicomFile dicom)
        {
            DicomImage dicomImage = new DicomImage(dicom.Dataset);
            WriteableBitmap bmp = dicomImage.RenderImage().As<WriteableBitmap>();

            return bmp;
        }
    }
}
