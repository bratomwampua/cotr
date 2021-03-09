using System;
using System.Collections.Generic;
using System.Text;

namespace Cotr
{
    public interface IPositionRepository
    {
        void AddPosition(Position newPosition);

        DateTime GetPositionsLastDate();

        List<Position> GetAllPositionsByMarketId(int marketId);
    }
}