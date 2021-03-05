#nullable disable

namespace Data.Models
{
    public partial class User
    {
        public long Id { get; set; }
        public string Fio { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
    }
}
