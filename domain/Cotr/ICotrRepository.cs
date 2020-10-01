using System;
using System.Collections.Generic;
using System.Text;

namespace Cotr
{
    public interface ICotrRepository
    {
        void AddPosition(Position newPosition);
        List<Cotr.Position> GetAllPositionsByMarketId(int marketId);
    }
}
