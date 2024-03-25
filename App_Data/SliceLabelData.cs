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

    #region Print Label Classes

    //public class PrintLabelFields
    //{
    //    public string DefaultDTAddress { get; set; }
    //    public string MarketID { get; set; }
    //    public string sShipToName { get; set; }
    //    public string sShipToCompany { get; set; }
    //    public string mktname { get; set; }
    //    public string accountid { get; set; }
    //    public string salesperson { get; set; }
    //    public string accountdescr { get; set; }
    //    public string AccountAddress { get; set; }        
    //    public string sShipToAddr1 { get; set; }
    //    public string sShipToCity { get; set; }
    //    public string sShipToState { get; set; }
    //    public string sShipToZip { get; set; }
    //    public int qty { get; set; }
    //    public int sWoNumber { get; set; }
    //    public int JobID { get; set; }
    //    public int SOKey { get; set; }
    //    public string itemid { get; set; }
    //    public string designdescr { get; set; }
    //    public int shippingmethodid { get; set; }
    //    public string ShipMethDesc { get; set; }
    //    public string pk { get; set; }
    //}

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
    }


    public static SliceLabelFields GetSliceLabelLabelData(DataRow reader)
    {
        SliceLabelFields sld = new SliceLabelFields();
        // {
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



    //public static PrintLabelFields GetPrintLabelData(DataRow data, int JobID)
    //{
    //    //int accountid, qty, sWoNumber, SOKey, DesignTrackerID, shippingmethodid;

    //    //int.TryParse(data["accountid"].ToString().Trim(), out accountid);
    //    //int.TryParse(data["qty"].ToString().Trim(), out qty);
    //    //int.TryParse(data["sWoNumber"].ToString().Trim(), out sWoNumber);
    //    //int.TryParse(data["SOKey"].ToString().Trim(), out SOKey);
    //    //int.TryParse(data["shippingmethodid"].ToString().Trim(), out shippingmethodid);
    //    //DesignTrackerID = JobID;


    //        string accountAddress = string.Empty;
    //        try
    //        {
    //        //object value = data["AccountAddress"];
    //        //if (value == DBNull.Value)
    //        //{
    //        //    accountAddress = string.Empty;
    //        //}
    //        //else
    //        //{
    //        accountAddress = data["AccountAddress"].ToString().Trim();
    //            //}

    //        }
    //        catch(Exception ex)
    //        { }

    //        PrintLabelFields pld = new PrintLabelFields
    //        {
    //        MarketID = data["MarketID"].ToString().Trim(),
    //        DefaultDTAddress = data["DefaultDTAddress"].ToString().Trim(),
    //        sShipToName = data["sShipToName"].ToString().Trim(),
    //        sShipToCompany = data["sShipToCompany"].ToString().Trim(),
    //        mktname = data["mktname"].ToString().Trim(),
    //        accountid = data["accountid"].ToString().Trim(),
    //        salesperson = data["salesperson"].ToString().Trim(),
    //        accountdescr = data["accountdescr"].ToString().Trim(),
    //        AccountAddress = accountAddress,
    //        qty = int.Parse(data["qty"].ToString().Trim()),
    //        sWoNumber = int.Parse(data["sWoNumber"].ToString().Trim()),
    //        JobID = JobID,
    //        SOKey = int.Parse(data["SOKey"].ToString().Trim()),
    //        itemid = data["itemid"].ToString().Trim(),
    //        designdescr = data["designdescr"].ToString().Trim(),
    //        shippingmethodid = int.Parse(data["shippingmethodid"].ToString().Trim()),
    //        ShipMethDesc = data["ShipMethDesc"].ToString().Trim(),
    //        pk = data["pk"].ToString().Trim(),
    //        sShipToAddr1 = data["sShipToAddr1"].ToString().Trim(),
    //        sShipToCity = data["sShipToCity"].ToString().Trim(),
    //        sShipToState = data["sShipToState"].ToString().Trim(),
    //        sShipToZip = data["sShipToZip"].ToString().Trim()
    //    };

    //    return pld;
    //}

    #endregion
}