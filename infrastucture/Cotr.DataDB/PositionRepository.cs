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

        public void DeleteAllPositions()
        {
            var col = db.GetCollection<Position>(collectionName);

            col.DeleteAll();
        }

        public DateTime GetPositionsLastDate()
        {
            var col = db.GetCollection<Position>(collectionName);

            var lastDatePosition = col.FindOne(Query.All("ReportDate", Query.Descending));

            return lastDatePosition is null
                ? Convert.ToDateTime("01/01/1970")
                : Convert.ToDateTime(lastDatePosition.ReportDate);
        }
    }
}