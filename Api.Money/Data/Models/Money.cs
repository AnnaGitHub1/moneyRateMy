#nullable disable

namespace Data.Models
{
    public partial class Money
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long StatusId { get; set; }
        public long CurrencyId { get; set; }
        public decimal Sum { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual Status Status { get; set; }
    }
}
