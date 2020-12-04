using System;

namespace Cotr
{
    public class Position
    {
        public int Id { get; set; }
        public MarketGroup MarketGroup { get; set; }
        public Market Market { get; set; }
        public Direction Direction { get; set; }
        public uint Value { get; set; }
        public Exchange Exchange { get; set; }

        public Position(int id, MarketGroup marketGroup, Market market,
                        Direction direction, uint value, Exchange exchange)
        {
            this.Id = id;
            this.MarketGroup = marketGroup;
            this.Market = market;
            this.Direction = direction;
            this.Value = value;
            this.Exchange = exchange;
        }
    }

    public class Market
    {
        public int Id { get; set; }
        public string Name {get; set; }
    }

    public class MarketGroup  // 
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Direction  // Long, Short, Spreading
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Exchange
    {
        public int Id {get;}
        public string Name {get;}
    }
}
