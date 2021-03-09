using System;
using System.Net;
using System.IO;
using System.IO.Compression;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

using System.Diagnostics;

namespace Cotr.CotAPI
{
    public class CotDataRepository
    {
        private static readonly uint dataDepthInYears = uint.Parse(ConfigurationManager.AppSettings["dataDepthInYears"]);

        private static readonly string wDir = ConfigurationManager.AppSettings["wDir"];

        private static readonly string archiveUrl = ConfigurationManager.AppSettings["archiveUrl"];
        private static readonly string reportUrl = ConfigurationManager.AppSettings["reportUrl"];

        private static readonly string commodityArcFileName = ConfigurationManager.AppSettings["commodityArcFileName"];
        private static readonly string commodityArcFileCsv = ConfigurationManager.AppSettings["commodityArcFileCsv"];
        private static readonly string commodityReportFileCsv = ConfigurationManager.AppSettings["commodityReportFileCsv"];

        private static readonly string financialArcFileName = ConfigurationManager.AppSettings["financialArcFileName"];
        private static readonly string financialArcFileCsv = ConfigurationManager.AppSettings["financialArcFileCsv"];
        private static readonly string financialReportFileCsv = ConfigurationManager.AppSettings["financialReportFileCsv"];

        private static readonly string dicMarketSymbolFileName = ConfigurationManager.AppSettings["dicMarketSymbolFileName"];

        private void Download(string fileName, string csvFileName)
        {
            WebClient client = new WebClient();
            int currentYear = DateTime.Now.Year;
            string pathOrigCsvFile = Path.Combine(wDir, csvFileName);

            for (int year = currentYear; currentYear - year < dataDepthInYears; year--)
            {
                string archFile = fileName.Replace("xxxx", year.ToString());
                string pathZip = Path.Combine(wDir, archFile);

                string csvFile = csvFileName.Replace(".txt", $"_{year}.txt");
                string pathCsv = Path.Combine(wDir, csvFile);

                client.DownloadFile(archiveUrl + archFile, pathZip);

                if (File.Exists(pathOrigCsvFile)) File.Delete(pathOrigCsvFile);
                ZipFile.ExtractToDirectory(pathZip, wDir);

                // rename original csv file
                if (File.Exists(pathCsv)) File.Delete(pathCsv);
                File.Move(pathOrigCsvFile, pathCsv);
            }
        }

        public void DownloadReport(string fileName)
        {
            WebClient client = new WebClient();
            string pathCsv = Path.Combine(wDir, fileName);

            client.DownloadFile(reportUrl + fileName, pathCsv);
        }

        private List<CommodityPosition> ReadCommodityArcFile()
        {
            int currentYear = DateTime.Now.Year;

            List<CommodityPosition> records = new List<CommodityPosition>();

            for (int year = currentYear; currentYear - year < dataDepthInYears; year--)
            {
                string csvFile = commodityArcFileCsv.Replace(".txt", $"_{year}.txt");
                string pathCsv = Path.Combine(wDir, csvFile);

                using (var reader = new StreamReader(pathCsv))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.RegisterClassMap<CommodityPositionMap>();
                    var recs = csv.GetRecords<CommodityPosition>().ToList();

                    records.AddRange(recs);
                }
            }

            return records;
        }

        private List<FinancialPosition> ReadFinacialArcFile()
        {
            int currentYear = DateTime.Now.Year;

            List<FinancialPosition> records = new List<FinancialPosition>();

            for (int year = currentYear; currentYear - year < dataDepthInYears; year--)
            {
                string csvFile = financialArcFileCsv.Replace(".txt", $"_{year}.txt");
                string pathCsv = Path.Combine(wDir, csvFile);

                using (var reader = new StreamReader(pathCsv))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.RegisterClassMap<FinancialPositionMap>();
                    var recs = csv.GetRecords<FinancialPosition>().ToList();

                    records.AddRange(recs);
                }
            }

            return records;
        }

        private List<CommodityPosition> ReadCommodityReportFile()
        {
            string pathCsv = Path.Combine(wDir, commodityReportFileCsv);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            using (var reader = new StreamReader(pathCsv))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Configuration.RegisterClassMap<CommodityPositionIndexMap>();
                var records = csv.GetRecords<CommodityPosition>().ToList();

                return records;
            }
        }

        private List<FinancialPosition> ReadFinancialReportFile()
        {
            string pathCsv = Path.Combine(wDir, financialReportFileCsv);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            using (var reader = new StreamReader(pathCsv))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.RegisterClassMap<FinancialPositionIndexMap>();
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
            Download(commodityArcFileName, commodityArcFileCsv);
            Download(financialArcFileName, financialArcFileCsv);

            List<CommodityPosition> commodityRecords = ReadCommodityArcFile();
            List<FinancialPosition> financialRecords = ReadFinacialArcFile();

            List<Position> data = FormatData(commodityRecords, financialRecords);

            return data;
        }

        public List<Position> GetCotReportData()
        {
            DownloadReport(commodityReportFileCsv);
            DownloadReport(financialReportFileCsv);

            List<CommodityPosition> commodityRecords = ReadCommodityReportFile();
            List<FinancialPosition> financialRecords = ReadFinancialReportFile();

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