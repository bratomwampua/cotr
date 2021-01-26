using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.Configuration;

namespace Cotr.CotAPI
{
    public class FinancialPosition
    {
        public string MarketAndExchangeName { get; set; }
        public string ReportDate { get; set; }

        public int DealerLong { get; set; }
        public int DealerShort { get; set; }

        public int AssetMgrLong { get; set; }
        public int AssetMgrShort { get; set; }

        public int LevMoneyLong { get; set; }
        public int LevMoneyShort { get; set; }

        public int OtherReportLong { get; set; }
        public int OtherReportShort { get; set; }

        public int TotalReportLong { get; set; }
        public int TotalReportShort { get; set; }

        public int NonReportLong { get; set; }
        public int NonReportShort { get; set; }
    }

    public sealed class FinancialPositionMap : ClassMap<FinancialPosition>
    {
        public FinancialPositionMap()
        {
            Map(m => m.MarketAndExchangeName).Name("Market_and_Exchange_Names");
            Map(m => m.ReportDate).Name("Report_Date_as_YYYY-MM-DD");

            Map(m => m.DealerLong).Name("Dealer_Positions_Long_All");
            Map(m => m.DealerShort).Name("Dealer_Positions_Short_All");

            Map(m => m.AssetMgrLong).Name("Asset_Mgr_Positions_Long_All");
            Map(m => m.AssetMgrShort).Name("Asset_Mgr_Positions_Short_All");

            Map(m => m.LevMoneyLong).Name("Lev_Money_Positions_Long_All");
            Map(m => m.LevMoneyShort).Name("Lev_Money_Positions_Short_All");

            Map(m => m.OtherReportLong).Name("Other_Rept_Positions_Long_All");
            Map(m => m.OtherReportShort).Name("Other_Rept_Positions_Short_All");

            Map(m => m.TotalReportLong).Name("Tot_Rept_Positions_Long_All");
            Map(m => m.TotalReportShort).Name("Tot_Rept_Positions_Short_All");

            Map(m => m.NonReportLong).Name("NonRept_Positions_Long_All");
            Map(m => m.NonReportShort).Name("NonRept_Positions_Short_All");
        }
    }

    public sealed class FinancialPositionIndexMap : ClassMap<FinancialPosition>
    {
        public FinancialPositionIndexMap()
        {
            Map(m => m.MarketAndExchangeName).Index(0);
            Map(m => m.ReportDate).Index(2);

            Map(m => m.DealerLong).Index(8);
            Map(m => m.DealerShort).Index(9);

            Map(m => m.AssetMgrLong).Index(11);
            Map(m => m.AssetMgrShort).Index(12);

            Map(m => m.LevMoneyLong).Index(14);
            Map(m => m.LevMoneyShort).Index(15);

            Map(m => m.OtherReportLong).Index(17);
            Map(m => m.OtherReportShort).Index(18);

            Map(m => m.TotalReportLong).Index(20);
            Map(m => m.TotalReportShort).Index(21);

            Map(m => m.NonReportLong).Index(22);
            Map(m => m.NonReportShort).Index(23);
        }
    }
}