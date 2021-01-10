using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.Configuration;

namespace Cotr.CotAPI
{
    public class CommodityPosition
    {
        public string MarketAndExchangeName { get; set; }
        public string ReportDate { get; set; }

        public int ProdMercLong { get; set; }
        public int ProdMercShort { get; set; }

        public int SwapLong { get; set; }
        public int SwapShort { get; set; }

        public int MMoneyLong { get; set; }
        public int MMoneyShort { get; set; }

        public int OtherReportLong { get; set; }
        public int OtherReportShort { get; set; }

        public int TotalReportLong { get; set; }
        public int TotalReportShort { get; set; }

        public int NonReportLong { get; set; }
        public int NonReportShort { get; set; }
    }

    public sealed class CommodityPositionMap : ClassMap<CommodityPosition>
    {
        public CommodityPositionMap()
        {
            Map(m => m.MarketAndExchangeName).Name("Market_and_Exchange_Names");
            Map(m => m.ReportDate).Name("Report_Date_as_YYYY-MM-DD");

            Map(m => m.ProdMercLong).Name("Prod_Merc_Positions_Long_All");
            Map(m => m.ProdMercShort).Name("Prod_Merc_Positions_Short_All");

            Map(m => m.SwapLong).Name("Swap_Positions_Long_All");
            Map(m => m.SwapShort).Name("Swap__Positions_Short_All");

            Map(m => m.MMoneyLong).Name("M_Money_Positions_Long_All");
            Map(m => m.MMoneyShort).Name("M_Money_Positions_Short_All");

            Map(m => m.OtherReportLong).Name("Other_Rept_Positions_Long_All");
            Map(m => m.OtherReportShort).Name("Other_Rept_Positions_Short_All");

            Map(m => m.TotalReportLong).Name("Tot_Rept_Positions_Long_All");
            Map(m => m.TotalReportShort).Name("Tot_Rept_Positions_Short_All");

            Map(m => m.NonReportLong).Name("NonRept_Positions_Long_All");
            Map(m => m.NonReportShort).Name("NonRept_Positions_Short_All");
        }
    }
}