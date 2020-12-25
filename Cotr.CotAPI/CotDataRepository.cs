using System;
using System.Net;
using System.IO;
using System.IO.Compression;
using CsvHelper;
using System.Globalization;
using System.Collections.Generic;

namespace Cotr.CotAPI
{
    public class CotDataRepository
    {
        private static readonly string commodityArchiveUrl = @"https://www.cftc.gov/files/dea/history/";
        private static readonly string commodityArcFileName = "fut_disagg_txt_2020.zip";
        private static readonly string commodityArcFileCsv = "f_year.txt";

        private static readonly string financialArchiveUrl = @"https://www.cftc.gov/files/dea/history/";
        private static readonly string financialArcFileName = "fut_fin_txt_2020.zip";
        private static readonly string financialArcFileCsv = "FinFutYY.txt";

        private static readonly string tmpDir = @"D:\tmp\";

        public bool GetCotData()
        {
            throw new NotImplementedException();
        }

        private void Download(string url, string fileName)
        {
            string path = tmpDir + fileName;

            WebClient client = new WebClient();
            client.DownloadFile(url + fileName, path);
            ZipFile.ExtractToDirectory(path, tmpDir);
        }

        public void GetCotArchiveData()
        {
            IEnumerable<dynamic> commodityRecords;
            IEnumerable<dynamic> financialRecords;

            this.Download(commodityArchiveUrl, commodityArcFileName);
            this.Download(financialArchiveUrl, financialArcFileName);

            using (var reader = new StreamReader(tmpDir + commodityArcFileCsv))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                commodityRecords = csv.GetRecords<dynamic>();
            }

            using (var reader = new StreamReader(tmpDir + financialArcFileCsv))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                financialRecords = csv.GetRecords<dynamic>();
            }
        }
    }
}