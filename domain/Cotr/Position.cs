using System;

namespace Cotr
{
    public class Position
    {
        public int Id { get; }
        public PositionAttribute MarketGroup { get; }
        public PositionAttribute Market { get; }
        public PositionAttribute Direction { get; }
        public uint Value { get; }
        public DateTime PositionDate { get; }

        public Position(int id, PositionAttribute marketGroup, PositionAttribute market,
                        PositionAttribute direction, uint value, DateTime positionDate)
        {
            this.Id = id;
            this.MarketGroup = marketGroup;
            this.Market = market;
            this.Direction = direction;
            this.Value = value;
            this.PositionDate = positionDate;
        }
    }

    public class PositionAttribute
    {
        public int Id { get; }
        public string Name { get; }

        public PositionAttribute(int id, string name)
        {
            Fn.ThrowIfInvalidArgs(id, name);

            this.Id = id;
            this.Name = name;
        }
    }

    internal static class Fn
    {
        public static void ThrowIfInvalidArgs(int id, string name)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException("PositionAttribute Id cannot be negative");

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("PositionAttribute Name cannot be empty or contain only spaces");
        }
    }
}