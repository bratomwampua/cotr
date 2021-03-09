using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;

using Cotr.DataDB;

namespace Cotr.Service
{
    public class PositionService
    {
        private static PositionRepository PosRepo { get; set; }

        public PositionService(LiteDatabase db)
        {
            PosRepo = new PositionRepository(db);
        }

        public DateTime GetPositionsLastDate()
        {
            DateTime dbDataLastDate = PosRepo.GetPositionsLastDate();

            return dbDataLastDate;
        }

        public void AddPositions(List<Position> positions)
        {
            PosRepo.AddPositions(positions);
        }

        public void DeleteAllPositions()
        {
            PosRepo.DeleteAllPositions();
        }
    }
}