using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HEMASaw.DAO
{
    public class QRCodeData
    {
        public string QRCodeDate { get; set; }
        public int WO { get; set; }
        public string BlockBatch { get; set; }
        public string SliceBatch { get; set; }
        public string Saw { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public double Ave { get; set; }
        public double SliceNum { get; set; }
        public string BlockNumber { get; set; }

        // Constructor
        public QRCodeData(int wo, string blockBatch, string slicebatch, string saw, double min, double max, double ave,string qRCodeDate)
        {
            WO = wo;
            BlockBatch = blockBatch;
            SliceBatch = slicebatch;
            Saw = saw;
            Min = min;
            Max = max;
            Ave = ave;
            QRCodeDate = qRCodeDate;
        }

        public QRCodeData()
        {
        }
    }

}