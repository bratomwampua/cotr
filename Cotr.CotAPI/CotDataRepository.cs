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
        private static readonly string archiveUrl = ConfigurationManager.AppSettings["archiveUrl"];
        private static readonly string commodityArcFileName = ConfigurationManager.AppSettings["commodityArcFileName"];
        private static readonly string commodityArcFileCsv = ConfigurationManager.AppSettings["commodityArcFileCsv"];
        private static readonly string financialArcFileName = ConfigurationManager.AppSettings["financialArcFileName"];
        private static readonly string financialArcFileCsv = ConfigurationManager.AppSettings["financialArcFileCsv"];

        private static readonly string tmpDir = ConfigurationManager.AppSettings["tmpDir"];

        public bool GetCotData()
        {
            throw new NotImplementedException();
        }

        private void Download(string url, string fileName, string csvFileName)
        {
            string pathZip = Path.Combine(tmpDir, fileName);
            string pathCsv = Path.Combine(tmpDir, csvFileName);

            WebClient client = new WebClient();
            client.DownloadFile(url + fileName, pathZip);

            if (File.Exists(pathCsv)) File.Delete(pathCsv);

            ZipFile.ExtractToDirectory(pathZip, tmpDir);
        }

        private List<CommodityPosition> ReadCommodityFile(string arcFileCsvName)
        {
            using (var reader = new StreamReader(Path.Combine(tmpDir + arcFileCsvName)))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.RegisterClassMap<CommodityPositionMap>();
                var records = csv.GetRecords<CommodityPosition>().ToList();

                return records;
            }
        }

        private List<FinancialPosition> ReadFinacialFile(string arcFileCsvName)
        {
            using (var reader = new StreamReader(Path.Combine(tmpDir + arcFileCsvName)))
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

            List<CommodityPosition> commodityRecords = ReadCommodityFile(commodityArcFileCsv);
            List<FinancialPosition> financialRecords = ReadFinacialFile(financialArcFileCsv);

            // TODO: format data to position format

            return (commodityRecords, financialRecords);
        }
    }
}