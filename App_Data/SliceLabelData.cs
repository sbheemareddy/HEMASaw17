using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PrintLabelData
/// </summary>
/// 

[Serializable]
public class SliceLabelData
{
    public SliceLabelData()
    {
    }
    public class SliceLabelFields
    {
        public string Material { get; set; }
        public string SliceBatch { get; set; }
        public string BlockBatch { get; set; }
        public int WorkOrder { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public decimal DensityPCF { get; set; }
        public decimal DensityPSF { get; set; }
        public decimal MinThickness { get; set; }
        public decimal MaxThickness { get; set; }
        public decimal AvgThickness { get; set; }
        public string SliceNum { get; set; }
        public int Quantity { get; set; }
        public string SONumber { get; set; }
        public string MfgDate { get; set; }
        public string OperatorName { get; set; }
        public string DocNum { get; set; }
        public string VisualPartID { get; set; }
        public string SalesOrder { get; set; }
        public string DenInToleranceRange { get; set; }
        public string ThickInToleranceRange { get; set; }
    }
    public static SliceLabelFields GetSliceLabelLabelData(DataRow reader)
    {
        SliceLabelFields sld = new SliceLabelFields();
        // {
        int Qty;
        int.TryParse(reader["Scheduled_Qty"].ToString(),out Qty);
        sld.Material = reader["Material"].ToString();
        sld.SliceBatch = reader["Slice_Batch"].ToString();
        sld.BlockBatch = reader["Block_Batch"].ToString();
        sld.WorkOrder = int.Parse(reader["WorkOrder"].ToString());
        sld.ReleaseDate = (DateTime)reader["Release_Date"];
        sld.DensityPCF = TryParseDecimal(reader["DensityPCF"]);
        sld.DensityPSF = TryParseDecimal(reader["DensityPSF"]);
        sld.MinThickness = TryParseDecimal(reader["MinThk"]);
        sld.MaxThickness = TryParseDecimal(reader["MaxThk"]);
        sld.AvgThickness = TryParseDecimal(reader["AvgThk"]);
        sld.SliceNum = reader["SliceNum"].ToString();
        sld.Description = reader["Description"].ToString();
        sld.Quantity = Qty;
        sld.SONumber = reader["SalesOrder"].ToString(); ;
        sld.MfgDate = reader["Mfg_date"].ToString(); ;
        sld.OperatorName = reader["OperatorName"].ToString(); 
        sld.DocNum = reader["DocNum"].ToString();
        sld.VisualPartID = reader["VisualPartID"].ToString();
        sld.SalesOrder = reader["SalesOrder"].ToString();
        sld.DenInToleranceRange = reader["DenInToleranceRange"].ToString();
        sld.ThickInToleranceRange = reader["ThickInToleranceRange"].ToString();
        //};

        return sld;
    }
    private static decimal TryParseDecimal(object value)
    {
        if (value != null && decimal.TryParse(value.ToString(), out decimal result))
        {
            return result;
        }
        else
        {
            return 0.00m;
        }
    }

}