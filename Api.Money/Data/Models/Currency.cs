using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class Currency
    {
        public Currency()
        {
            Money = new HashSet<Money>();
        }

        public long Id { get; set; }
        public string CurrencyName { get; set; }

        public virtual ICollection<Money> Money { get; set; }
    }
}
