using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Data
{
    public partial class User
    {
        public User()
        {
            Money = new HashSet<Money>();
        }
    
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        public string Fio { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }

        public virtual ICollection<Money> Money { get; set; }
    }
}
