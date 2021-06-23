namespace NationStates.NET.Nation
{
    public class WABadge
    {
        public WABadgeType Type { get; set; }

        public long ID { get; set; }

        public WABadge(WABadgeType type, long id)
        {
            this.Type = type;
            this.ID = id;
        }
    }
}
