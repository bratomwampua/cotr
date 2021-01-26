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

    public sealed class CommodityPositionIndexMap : ClassMap<CommodityPosition>
    {
        public CommodityPositionIndexMap()
        {
            Map(m => m.MarketAndExchangeName).Index(0);
            Map(m => m.ReportDate).Index(2);

            Map(m => m.ProdMercLong).Index(8);
            Map(m => m.ProdMercShort).Index(9);

            Map(m => m.SwapLong).Index(10);
            Map(m => m.SwapShort).Index(11);

            Map(m => m.MMoneyLong).Index(13);
            Map(m => m.MMoneyShort).Index(14);

            Map(m => m.OtherReportLong).Index(16);
            Map(m => m.OtherReportShort).Index(17);

            Map(m => m.TotalReportLong).Index(19);
            Map(m => m.TotalReportShort).Index(20);

            Map(m => m.NonReportLong).Index(21);
            Map(m => m.NonReportShort).Index(22);
        }
    }
}