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

        public CotDataService()
        {
            CotRepo = CotRepo ?? new CotDataRepository();
        }

        public bool UpdateCotData()
        {
            // get last date from DB
            DateTime dbDataLastDate = PositionService.PosRepo.GetPositionsLastDate();

            // if last date is null, get reports archive from API and save to DB
            // upload report from API and get last date
            // if report date not equal to last date from DB, save new report data to DB

            Debug.WriteLine(dbDataLastDate.ToString());
            return false;
        }
    }
}