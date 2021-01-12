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

        public (List<CommodityPosition>, List<FinancialPosition>) GetCotArchiveData()
        {
            Download(archiveUrl, commodityArcFileName, commodityArcFileCsv);
            Download(archiveUrl, financialArcFileName, financialArcFileCsv);

            List<CommodityPosition> commodityRecords = ReadCommodityFile();
            List<FinancialPosition> financialRecords = ReadFinacialFile();

            // TODO: format data to position format

            return (commodityRecords, financialRecords);
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