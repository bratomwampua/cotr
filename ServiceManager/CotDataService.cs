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

        static CotDataService()
        {
            CotRepo = new CotDataRepository();
        }

        public CotDataService(ServiceManager sm)
        {
            SM = SM ?? sm;
        }

        private void UpdateMarketSymbols()
        {
            List<MarketSymbol> symbolsRecords = CotRepo.GetMarketSymbols();

            SM.SymbolService.AddMarketSymbols(symbolsRecords);
        }

        private void UpdateDataFromArchive()
        {
            List<Position> positions = CotRepo.GetCotArchiveData();

            SM.PosService.AddPositions(positions);
        }

        public void UpdateCotData()
        {
            // get last position date from DB
            DateTime dbDataLastDate = SM.PosService.GetPositionsLastDate();

            // if last date is (01.01.1970 0:00:00)
            if (dbDataLastDate < Convert.ToDateTime("02/01/1970"))
            {
                // update market symbols dictionary from csv file
                UpdateMarketSymbols();
                // get reports archive from API and save to DB
                UpdateDataFromArchive();
            }
            // if last date more then 14 days old (old)
            // clear DB and update from reports archive
            else if ((DateTime.Today - dbDataLastDate).Days > 14)
            {
                SM.PosService.DeleteAllPositions();
                UpdateDataFromArchive(); // get reports archive from API and save to DB
            }
            // if last date from 7 to 13 days old
            // upload report from API and add to DB
            else if ((DateTime.Today - dbDataLastDate).Days > 1)
            {
                // addDataFromReport();
            }
        }
    }
}