using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class Status
    {
        public Status()
        {
            Money = new HashSet<Money>();
        }

        public long Id { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<Money> Money { get; set; }
    }
}
