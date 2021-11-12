namespace NationStates.NET
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using static Utility;

    /// <summary>
    /// Represents a trading card.
    /// </summary>
    public struct Card
    {
        /// <summary>
        /// Gets the nation's category.
        /// </summary>
        [JsonProperty]
        public NationCategory Category { get; }

        /// <summary>
        /// Gets the URL of the nation's flag.
        /// </summary>
        [JsonProperty]
        public string Flag { get; }

        /// <summary>
        /// Gets the nation's ID.
        /// </summary>
        [JsonProperty]
        public long ID { get; }

        /// <summary>
        /// Gets a list of asks and bids for the card.
        /// </summary>
        [JsonProperty]
        public HashSet<Market> Markets { get; }

        /// <summary>
        /// Gets the card's market value.
        /// </summary>
        [JsonProperty]
        public double MarketValue { get; }

        /// <summary>
        /// Gets the card's name.
        /// </summary>
        [JsonProperty]
        public string Name { get; }

        /// <summary>
        /// Gets a list of nations that own the card.
        /// </summary>
        [JsonProperty]
        public HashSet<Owner> Owners { get; }

        /// <summary>
        /// Gets the card's rarity.
        /// </summary>
        [JsonProperty]
        public Rarity Rarity { get; }

        /// <summary>
        /// Gets the nation's region.
        /// </summary>
        [JsonProperty]
        public string Region { get; }

        /// <summary>
        /// Gets the card's season.
        /// </summary>
        [JsonProperty]
        public int Season { get; }

        /// <summary>
        /// Gets the nation's slogan.
        /// </summary>
        [JsonProperty]
        public string Slogan { get; }

        /// <summary>
        /// Gets the nation's type.
        /// </summary>
        [JsonProperty]
        public string Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> struct.
        /// </summary>
        /// <param name="id">The nation's ID.</param>
        /// <param name="season">The card's season.</param>
        public Card(long id, int season)
        {
            XmlNode node = ParseDocument($"q=card+info+markets+owners+trades;cardid={id};season={season}")
                .SelectSingleNode("/CARD");

            this.ID = id;
            this.Season = season;
            this.Category = (NationCategory)ParseEnum(typeof(NationCategory), node.SelectSingleNode("GOVT").InnerText);
            this.Flag = node.SelectSingleNode("FLAG").InnerText;

            HashSet<Market> markets = new();
            foreach (XmlNode market in node.SelectNodes("MARKETS/MARKET"))
            {
                string nation = market.SelectSingleNode("NATION").InnerText;
                double price = double.Parse(market.SelectSingleNode("PRICE").InnerText);
                DateTime timeStamp = ParseUnix(market.SelectSingleNode("TIMESTAMP").InnerText);
                MarketType type = (MarketType)ParseEnum(typeof(MarketType), market.SelectSingleNode("TYPE").InnerText);

                markets.Add(new(this.ID, this.Season, nation, price, timeStamp, type));
            }

            this.Markets = markets;

            this.MarketValue = double.Parse(node.SelectSingleNode("MARKET_VALUE").InnerText);
            this.Name = node.SelectSingleNode("NAME").InnerText;

            Dictionary<string, int> count = new();
            foreach (XmlNode owner in node.SelectNodes("OWNERS/OWNER"))
            {
                string nationName = owner.InnerText;

                if (count.ContainsKey(nationName))
                {
                    count[nationName]++;
                }
                else
                {
                    count.Add(nationName, 1);
                }
            }

            this.Owners = count.Select(i => new Owner(this.ID, i.Key, i.Value)).ToHashSet();

            this.Rarity = (Rarity)ParseEnum(typeof(Rarity), node.SelectSingleNode("CATEGORY").InnerText);
            this.Region = node.SelectSingleNode("REGION").InnerText;
            this.Slogan = node.SelectSingleNode("SLOGAN").InnerText;

            this.Type = node.SelectSingleNode("TYPE").InnerText;
        }

        /// <summary>
        /// Gets a list of recent trades for the card.
        /// </summary>
        /// <param name="limit">The maximum amount of trades to get.</param>
        /// <param name="sinceTime">Get trades that occurred after this time.</param>
        /// <param name="beforeTime">Get trades that occurred before this time.</param>
        /// <returns>A list of recent trades for the card.</returns>
        public HashSet<Trade> Trades(int limit = 50, DateTime? sinceTime = null, DateTime? beforeTime = null)
        {
            HashSet<Trade> trades = new();

            string url = $"q=card+trades;cardid={this.ID};season={this.Season};limit={limit}";

            if (sinceTime != null)
            {
                url += $";sincetime={ConvertToUnix((DateTime)sinceTime)}";
            }

            if (beforeTime != null)
            {
                url += $";beforetime={ConvertToUnix((DateTime)beforeTime)}";
            }

            foreach (XmlNode trade in ParseDocument(url).SelectNodes("/CARD/TRADES/TRADE"))
            {
                string buyer = trade.SelectSingleNode("BUYER").InnerText;
                string seller = trade.SelectSingleNode("SELLER").InnerText;

                string priceS = trade.SelectSingleNode("PRICE").InnerText;
                double price = (priceS != string.Empty) ? double.Parse(priceS) : 0;

                DateTime timeStamp = ParseUnix(trade.SelectSingleNode("TIMESTAMP").InnerText);

                trades.Add(new(this.ID, this.Season, this.Rarity, buyer, seller, price, timeStamp));
            }

            return trades;
        }

        /// <summary>
        /// Gets a JSON string representing the card.
        /// </summary>
        /// <returns>A JSON string representing the card.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}