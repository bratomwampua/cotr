using System;
using System.Collections.Generic;
using System.Text;

using Cotr.DataDB;

namespace Cotr.Service
{
    public class PositionService
    {
        public static PositionRepository PosRepo { get; set; }

        public PositionService()
        {
            PosRepo = PosRepo ?? new PositionRepository();
        }
    }
}