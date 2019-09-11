using Dicom;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace X_Ray_Visualizer_WPF
{
    class DicomManager
    {
        DisplayManagement display;

        public DicomManager(DisplayManagement display)
        {
            this.display = display;
        }
        public DicomFile[] OpenDicomSearch()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            try
            {
                openFile.InitialDirectory = "c:\\";
                openFile.Filter = "Dicom Files (*.dcm)|*.dcm";
                openFile.FilterIndex = 1;
                openFile.RestoreDirectory = true;
                openFile.Multiselect = true;

                if (openFile.ShowDialog() == true)
                {
                    DicomFile[] dcm = new DicomFile[openFile.FileNames.Length];

                    foreach (string uri in openFile.FileNames)
                    {
                        Console.WriteLine("Reading from " + uri);
                        dcm[Array.IndexOf(dcm, null)] = DicomFile.Open(uri);
                    }
                    WriteableBitmap bitmap = DisplayManagement.DICOMToBmp(dcm[0]);
                    Console.WriteLine("Not held 2");
                    display.PreviewImage(bitmap);
                    Console.Read();
                    return dcm;
                }

                return null;
            }

            finally
            {
                openFile = null;
            }
        }
    }
}
