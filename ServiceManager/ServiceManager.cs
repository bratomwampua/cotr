using System;
using System.Diagnostics;

namespace Cotr.Service
{
    public class ServiceManager
    {
        public CotDataService CotService { get; private set; }
        public PositionService PosService { get; set; }

        public ServiceManager()
        {
            this.CotService = new CotDataService();
            this.PosService = new PositionService();
        }
    }
}