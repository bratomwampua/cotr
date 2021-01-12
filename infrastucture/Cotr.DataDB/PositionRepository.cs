using LiteDB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Configuration;

namespace Cotr.DataDB
{
    public class PositionRepository : IPositionRepository
    {
        private static LiteDatabase db;

        private static readonly string collectionName = "position";

        private List<Position> positions = new List<Position>();

        public PositionRepository(LiteDatabase DB)
        {
            db = DB;
        }

        public void AddPosition(Position newPosition)
        {
            var col = db.GetCollection<Position>(collectionName);

            col.Insert(newPosition);
        }

        public List<Position> GetAllPositionsByMarketId(int marketId)
        {
            var col = db.GetCollection<Position>(collectionName);

            positions = col.Query()
            .Where(x => x.Market.Id == marketId)
            .OrderBy(x => x.PositionDate)
            .ToList();

            return positions;
        }

        public DateTime GetPositionsLastDate()
        {
            var col = db.GetCollection<Position>(collectionName);

            var lastDate = col.FindOne(Query.All("PositionDate", Query.Ascending));
            Debug.WriteLine(lastDate);

            return Convert.ToDateTime(lastDate);
        }
    }
}