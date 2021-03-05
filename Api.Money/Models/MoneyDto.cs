using System.Collections.Generic;

namespace Models
{
    public class MoneyDto
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        /// <summary>
        /// Статус кошелька
        /// </summary>
        public string MoneyStatus { get; set; }

        /// <summary>
        /// Баланс кошелька в валюте
        /// </summary>
        public List<BalanceDto> Balances { get; set; }
    }
}
