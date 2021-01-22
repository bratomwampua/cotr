using System;
using System.Net;
using System.IO;
using System.IO.Compression;
using CsvHelper;
using System.Globalization;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

using System.Diagnostics;

namespace Cotr.CotAPI
{
    public class CotDataRepository
    {
        private static readonly string wDir = ConfigurationManager.AppSettings["wDir"];

        private static readonly string archiveUrl = ConfigurationManager.AppSettings["archiveUrl"];

        private static readonly string commodityArcFileName = ConfigurationManager.AppSettings["commodityArcFileName"];
        private static readonly string commodityArcFileCsv = ConfigurationManager.AppSettings["commodityArcFileCsv"];

        private static readonly string financialArcFileName = ConfigurationManager.AppSettings["financialArcFileName"];
        private static readonly string financialArcFileCsv = ConfigurationManager.AppSettings["financialArcFileCsv"];

        private static readonly string dicMarketSymbolFileName = ConfigurationManager.AppSettings["dicMarketSymbolFileName"];

        public bool GetCotData()
        {
            throw new NotImplementedException();
        }

        private void Download(string url, string fileName, string csvFileName)
        {
            string pathZip = Path.Combine(wDir, fileName);
            string pathCsv = Path.Combine(wDir, csvFileName);

            WebClient client = new WebClient();
            client.DownloadFile(url + fileName, pathZip);

            if (File.Exists(pathCsv)) File.Delete(pathCsv);

            ZipFile.ExtractToDirectory(pathZip, wDir);
        }

        private List<CommodityPosition> ReadCommodityFile()
        {
            using (var reader = new StreamReader(Path.Combine(wDir + commodityArcFileCsv)))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.RegisterClassMap<CommodityPositionMap>();
                var records = csv.GetRecords<CommodityPosition>().ToList();

                return records;
            }
        }

        private List<FinancialPosition> ReadFinacialFile()
        {
            using (var reader = new StreamReader(Path.Combine(wDir + financialArcFileCsv)))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.RegisterClassMap<FinancialPositionMap>();
                var records = csv.GetRecords<FinancialPosition>().ToList();

                return records;
            }
        }

        public List<Position> FormatData(List<CommodityPosition> commodityRecords,
                                         List<FinancialPosition> financialRecords)
        {
            List<Position> positions = new List<Position>();

            commodityRecords.ForEach(el =>
                {
                    List<TraderPosition> traderPositions = new List<TraderPosition>
                    {
                        new TraderPosition("ProdMerc", "long", (uint)el.ProdMercLong),
                        new TraderPosition("ProdMerc", "short", (uint)el.ProdMercShort),

                        new TraderPosition("Swap", "long", (uint)el.SwapLong),
                        new TraderPosition("Swap", "short", (uint)el.SwapShort),

                        new TraderPosition("MMoney", "long", (uint)el.MMoneyLong),
                        new TraderPosition("MMoney", "short", (uint)el.MMoneyShort),

                        new TraderPosition("OtherReport", "long", (uint)el.OtherReportLong),
                        new TraderPosition("OtherReport", "short", (uint)el.OtherReportShort),

                        new TraderPosition("TotalReport", "long", (uint)el.TotalReportLong),
                        new TraderPosition("TotalReport", "short", (uint)el.TotalReportShort),

                        new TraderPosition("NonReport", "long", (uint)el.NonReportLong),
                        new TraderPosition("NonReport", "short", (uint)el.NonReportShort)
                    };

                    positions.Add(new Position("commodity", el.MarketAndExchangeName,
                                  DateTime.Parse(el.ReportDate), traderPositions));
                }
            );

            financialRecords.ForEach(el =>
            {
                List<TraderPosition> traderPositions = new List<TraderPosition>
                {
                    new TraderPosition("Dealer", "long", (uint)el.DealerLong),
                    new TraderPosition("Dealer", "short", (uint)el.DealerShort),

                    new TraderPosition("AssetMgr", "long", (uint)el.AssetMgrLong),
                    new TraderPosition("AssetMgr", "short", (uint)el.AssetMgrShort),

                    new TraderPosition("LevMoney", "long", (uint)el.LevMoneyLong),
                    new TraderPosition("LevMoney", "short", (uint)el.LevMoneyShort),

                    new TraderPosition("OtherReport", "long", (uint)el.OtherReportLong),
                    new TraderPosition("OtherReport", "short", (uint)el.OtherReportShort),

                    new TraderPosition("TotalReport", "long", (uint)el.TotalReportLong),
                    new TraderPosition("TotalReport", "short", (uint)el.TotalReportShort),

                    new TraderPosition("NonReport", "long", (uint)el.NonReportLong),
                    new TraderPosition("NonReport", "short", (uint)el.NonReportShort)
                };

                positions.Add(new Position("financial", el.MarketAndExchangeName,
                              DateTime.Parse(el.ReportDate), traderPositions));
            });

            return positions;
        }

        public List<Position> GetCotArchiveData()
        {
            Download(archiveUrl, commodityArcFileName, commodityArcFileCsv);
            Download(archiveUrl, financialArcFileName, financialArcFileCsv);

            List<CommodityPosition> commodityRecords = ReadCommodityFile();
            List<FinancialPosition> financialRecords = ReadFinacialFile();

            List<Position> data = FormatData(commodityRecords, financialRecords);

            return data;
        }

        private List<MarketSymbol> ReadMarketSymbolFile()
        {
            using (var reader = new StreamReader(Path.Combine(wDir + dicMarketSymbolFileName)))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<MarketSymbol>().ToList();

                return records;
            }
        }

        public List<MarketSymbol> GetMarketSymbols()
        {
            List<MarketSymbol> symbolRecords = ReadMarketSymbolFile();

            return symbolRecords;
        }
    }
}