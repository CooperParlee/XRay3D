using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilDICOM.Core;
using Microsoft.Win32;

namespace X_Ray_Visualizer_WPF
{
    class DICOMProcessing
    {
        private DisplayManagement display;

        public DICOMProcessing(DisplayManagement display)
        {
            this.display = display;
        }
 
        public DICOMObject[] FindDicomFile()
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
                    DICOMObject[] dcm = new DICOMObject[openFile.FileNames.Length];
                    
                    foreach (string uri in openFile.FileNames)
                    {
                        Console.WriteLine("Reading from " + uri);
                        dcm[Array.IndexOf(dcm, null)] = DICOMObject.Read(uri);
                    }
                    display.PreviewImage(DisplayManagement.DICOMToBmp(dcm[0]));
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
