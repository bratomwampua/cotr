using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using Cotr.CotAPI;

namespace Cotr.Service
{
    public class CotDataService
    {
        private static CotDataRepository CotRepo
        { get; set; }

        private static ServiceManager SM { get; set; }

        public CotDataService(ServiceManager sm)
        {
            CotRepo = CotRepo ?? new CotDataRepository();
            SM = SM ?? sm;
        }

        private void UpdateFromArchive()
        {
            CotRepo.GetCotArchiveData();
        }

        public void UpdateCotData()
        {
            // get last position date from DB
            DateTime dbDataLastDate = SM.PosService.GetPositionsLastDate();

            // if last date is empty (01.01.0001 0:00:00),
            // get reports archive from API and save to DB
            if (dbDataLastDate < Convert.ToDateTime("01/01/1970"))
                this.UpdateFromArchive();

            // upload report from API and get last date
            // if report date not equal to last date from DB, save new report data to DB

            // Debug.WriteLine(dbDataLastDate.ToString());
        }
    }
}