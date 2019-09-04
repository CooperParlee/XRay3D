using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilDICOM.Core;
using EvilDICOM.Core.Helpers;

namespace X_Ray_Visualizer_WPF
{
    class SimpleDicom
    {
        int width { get; }
        int length { get;  }

        /// <summary>
        /// A DICOM wrapper for more simplistic parameters of DICOMs as used in the X-Ray
        /// Visualizer program, such as width, length, etc.
        /// </summary>
        /// <param name="dicom"></param>
        public SimpleDicom(DICOMObject dicom)
        {
            width = Convert.ToInt32(dicom.FindFirst(TagHelper.Columns.CompleteID).DData);
            length = Convert.ToInt32(dicom.FindFirst(TagHelper.Rows.CompleteID).DData);
        }

        public int GetDimensionX()
        {
            return width;
        }

        public int GetDimensionY()
        {
            return length;
        }
    }
}
