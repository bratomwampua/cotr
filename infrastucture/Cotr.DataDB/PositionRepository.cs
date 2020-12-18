using LiteDB;
using System;
using System.Collections.Generic;

namespace Cotr.DataDB
{
    public class PositionRepository : IPositionRepository
    {
        private static readonly string _strConnection = "Filename=cotr.litedb4; Mode=Exclusive;";
        private static readonly string collectionName = "position";

        private List<Position> positions = new List<Position>();

        public void AddPosition(Position newPosition)
        {
            // Open database (or create if doesn't exist)
            using (var db = new LiteDatabase(_strConnection))
            {
                var col = db.GetCollection<Position>(collectionName);

                col.Insert(newPosition);  // insert new position
            }
        }

        public List<Position> GetAllPositionsByMarketId(int marketId)
        {
            using (var db = new LiteDatabase(_strConnection))
            {
                var col = db.GetCollection<Position>(collectionName);

                positions = col.Query()
                .Where(x => x.Market.Id == marketId)
                .OrderBy(x => x.PositionDate)
                .ToList();

                return positions;
            }
        }

        public DateTime GetPositionsLastDate()
        {
            using (var db = new LiteDatabase(_strConnection))
            {
                var col = db.GetCollection<Position>(collectionName);

                var lastDate = col.FindOne(Query.All("PositionDate", Query.Ascending));
                Console.WriteLine(lastDate);

                return Convert.ToDateTime("01/01/1970");  // !!! TMP !!!
            }
        }
    }
}