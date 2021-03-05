using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Data
{
    public partial class Money
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long UserId { get; set; }
        public long StatusId { get; set; }
        public long CurrencyId { get; set; }
        public decimal Sum { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
    }
}
