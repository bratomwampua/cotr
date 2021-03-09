using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;

using Cotr.DataDB;
using Cotr.CotAPI;

namespace Cotr.Service
{
    public class MarketSymbolService
    {
        private static MarketSymbolRepository SymbolRepo;

        public MarketSymbolService(LiteDatabase db)
        {
            SymbolRepo = new MarketSymbolRepository(db);
        }

        public void AddMarketSymbols(List<MarketSymbol> symbols)
        {
            SymbolRepo.AddMarketSymbols(symbols);
        }

        public List<string> GetAllMarketSymbols()
        {
            return SymbolRepo.GetAllMarketSymbols();
        }
    }
}