namespace Models
{
    public class BalanceDto
    {
        /// <summary>
        /// Сумма на счету
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Валюта
        /// </summary>
        public string Currency { get; set; }
    }
}
