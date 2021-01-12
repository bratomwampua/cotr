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
            (List<CommodityPosition> commodityRecords,
             List<FinancialPosition> financialRecords) = CotRepo.GetCotArchiveData();

            // TODO: send data to position service
        }

        public void UpdateCotData()
        {
            // get last position date from DB
            DateTime dbDataLastDate = SM.PosService.GetPositionsLastDate();

            // if last date is empty (01.01.0001 0:00:00),
            if (dbDataLastDate < Convert.ToDateTime("01/01/1970"))
            {
                // update market symbols dictionary from csv file
                UpdateMarketSymbols();

                // get reports archive from API and save to DB
                UpdateDataFromArchive();
            }

            // upload report from API and get last date
            // if report date not equal to last date from DB, save new report data to DB
        }
    }
}