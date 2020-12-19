using System;
using System.Diagnostics;

namespace Cotr.Service
{
    public class ServiceManager
    {
        private CotDataService CotService { get; set; }
        private PositionService PosService { get; set; }

        public ServiceManager()
        {
            this.CotService = new CotDataService();
            this.PosService = new PositionService();
        }
    }
}