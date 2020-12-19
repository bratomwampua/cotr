using System;
using System.Collections.Generic;
using System.Text;

using Cotr.CotAPI;

namespace Cotr.Service
{
    public class CotDataService
    {
        public static CotDataRepository CotRepo { get; private set; }

        public CotDataService()
        {
            CotRepo = CotRepo == null ? new CotDataRepository() : CotRepo;
        }

        public bool UpdateCotData()
        {
            // get last date from DB
            // DateTime dbDataLastDate =

            // if last date is null, get reports archive from API and save to DB
            // upload report from API and get last date
            // if report date not equal to last date from DB, save new report data to DB

            return false;
        }
    }
}