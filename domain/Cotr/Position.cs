using System;
using System.Collections.Generic;
using System.Linq;

namespace Cotr
{
    public class Position
    {
        public string MarketGroup { get; set; }
        public string Market { get; set; }
        public DateTime ReportDate { get; set; }
        public List<TraderPosition> Positions { get; set; }

        // LiteDB need a public parameterless constructor
        public Position()
        {
            MarketGroup = "";
            Market = "";
            ReportDate = DateTime.Today;
            Positions = new List<TraderPosition>();
        }

        public Position(string marketGroup, string market,
                        DateTime reportDate, List<TraderPosition> positions)
        {
            Fn.ThrowIfInvalidArgs(marketGroup, market);

            MarketGroup = marketGroup;
            Market = market;
            ReportDate = reportDate;
            Positions = positions;
        }
    }

    public class TraderPosition
    {
        public string TraderName { get; }
        public string Direction { get; }
        public uint Value { get; }

        // LiteDB need a public parameterless constructor
        public TraderPosition()
        {
            TraderName = "";
            Direction = "";
            Value = 0;
        }

        public TraderPosition(string traderName, string direction, uint value)
        {
            Fn.ThrowIfInvalidArgs(traderName, direction, value);

            TraderName = traderName;
            Direction = direction;
            Value = value;
        }
    }

    internal static class Fn
    {
        public static void ThrowIfInvalidArgs(string marketGroup, string market)
        {
            string[] marketGroups = { "commodity", "financial" };

            if (!marketGroups.Contains(marketGroup))
                throw new ArgumentOutOfRangeException("Position Market Group must be commodity or financial");

            if (String.IsNullOrWhiteSpace(market))
                throw new ArgumentException("Position Market name cannot be empty or contain only spaces");
        }

        public static void ThrowIfInvalidArgs(string traderName, string direction, uint value)
        {
            string[] directions = { "long", "short" };

            if (String.IsNullOrWhiteSpace(traderName))
                throw new ArgumentException("Position Trader Name cannot be empty or contain only spaces");

            if (!directions.Contains(direction))
                throw new ArgumentOutOfRangeException("Position Direction must be long or short");

            if (value < 0)
                throw new ArgumentOutOfRangeException("Position value cannot be negative");
        }
    }
}