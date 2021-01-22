using System;
using System.Diagnostics;
using LiteDB;
using System.Configuration;

namespace Cotr.Service
{
    public class ServiceManager
    {
        private static readonly LiteDatabase db;
        public CotDataService CotService { get; private set; }
        public PositionService PosService { get; set; }
        public MarketSymbolService SymbolService { get; set; }

        static ServiceManager()
        {
            string strConnection = ConfigurationManager.ConnectionStrings["LiteDb"].ConnectionString;
            db = new LiteDatabase(strConnection);
        }

        public ServiceManager()
        {
            CotService = new CotDataService(this);
            PosService = new PositionService(db);
            SymbolService = new MarketSymbolService(db);
        }
    }
}