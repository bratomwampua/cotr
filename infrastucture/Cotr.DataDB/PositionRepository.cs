using LiteDB;
using System;
using System.Collections.Generic;

namespace Cotr.DataDB
{
    public class PositionRepository : ICotrRepository
    {
        static string _strConnection = "Filename=cotr.litedb4; Mode=Exclusive;";

        public void AddPosition(Position newPosition)
        {
            // Open database (or create if doesn't exist)
            using (var db = new LiteDatabase(_strConnection))
            {
                var col = db.GetCollection<Position>("positions");

                col.Insert(newPosition);  // insert new position
            }
        }

        public List<Position> GetAllPositionsByMarketId(int marketId)
        {
            using (var db = new LiteDatabase(_strConnection))
            {
                var col = db.GetCollection<Position>("positions");

                var results = col.Query()
                .Where(x => x.Market.Id == marketId)
                .OrderBy(x => x.Market.Id)
                .ToList();

                return results;
            }
        }
    }
}
