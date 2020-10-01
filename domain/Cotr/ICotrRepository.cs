using System;
using System.Collections.Generic;
using System.Text;

namespace Cotr
{
    interface ICotrRepository
    {
        Position[] GetAllPositionsByMarketId(int marketId);
    }
}
