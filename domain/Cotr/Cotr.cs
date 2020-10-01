using System;

namespace Cotr
{
    public class Position
    {
        public int Id { get; set; }
        public MarketGroup MarketGroup { get; set; }
        public Market Market { get; set; }
        public Exchange Exchange { get; set; }
        
    }

    public class Market
    {
        public int Id { get; set; }
        public string Name {get; set; }
    }

    public class MarketGroup
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
