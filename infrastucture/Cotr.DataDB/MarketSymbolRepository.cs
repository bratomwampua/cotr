using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;
using System.Configuration;
using Cotr.CotAPI;

namespace Cotr.DataDB
{
    public class MarketSymbolRepository
    {
        private static LiteDatabase db;

        private static readonly string collectionName = "marketSymbol";

        private List<MarketSymbol> symbols = new List<MarketSymbol>();

        public MarketSymbolRepository(LiteDatabase DB)
        {
            db = DB;
        }

        public void AddMarketSymbols(List<MarketSymbol> symbolRecords)
        {
            var col = db.GetCollection<MarketSymbol>(collectionName);
            col.DeleteAll();

            symbolRecords.ForEach(el => col.Insert(el));
        }

        public List<string> GetAllMarketSymbols()
        {
            var col = db.GetCollection<MarketSymbol>(collectionName);
            var symbols = col.FindAll();

            List<string> result = new List<string>();

            foreach (var rec in symbols)
            {
                if (!result.Contains(rec.Market))
                    result.Add(rec.Market);
            }
            result.Sort();

            return result;
        }
    }
}