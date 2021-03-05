using System.ComponentModel;

namespace Models
{
    public enum Status
    {
        [Description("Активый")]
        Active = 1,

        [Description("Заблокирован")]
        Locked = 2
    }
}
