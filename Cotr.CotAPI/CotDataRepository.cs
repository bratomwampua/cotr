using System;
using System.Net;

namespace Cotr.CotAPI
{
    public class CotDataRepository
    {
        private static readonly string commodityArchiveUrl = @"https://www.cftc.gov/files/dea/history/";
        private static readonly string commodityArcFileName = "fut_disagg_txt_2020.zip";

        private static readonly string financialArchiveUrl = @"https://www.cftc.gov/files/dea/history/";
        private static readonly string financialArcFileName = "fut_fin_txt_2020.zip";

        private static readonly string tmpDir = @"D:\tmp\";

        public bool GetCotData()
        {
            throw new NotImplementedException();
        }

        private void Download(string url, string fileName)
        {
            WebClient client = new WebClient();
            client.DownloadFile(url + fileName, tmpDir + fileName);
        }

        public void GetCotArchiveData()
        {
            this.Download(commodityArchiveUrl, commodityArcFileName);
            this.Download(financialArchiveUrl, financialArcFileName);
        }
    }
}