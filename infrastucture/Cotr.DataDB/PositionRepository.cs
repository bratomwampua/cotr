using LiteDB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Configuration;

namespace Cotr.DataDB
{
    public class PositionRepository
    {
        private static LiteDatabase db;

        private static readonly string collectionName = "position";

        // private List<Position> positions = new List<Position>();

        public PositionRepository(LiteDatabase DB)
        {
            db = DB;
        }

        public void AddPositions(List<Position> positions)
        {
            var col = db.GetCollection<Position>(collectionName);

            positions.ForEach(el => col.Insert(el));
        }

        public DateTime GetPositionsLastDate()
        {
            var col = db.GetCollection<Position>(collectionName);

            var lastDate = col.FindOne(Query.All("PositionDate", Query.Ascending));

            return Convert.ToDateTime(lastDate);
        }
    }
}