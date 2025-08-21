using System;

namespace Assignment.Models.CurrencyRate.Tables
{
    public class CalculatedHistoryDTO
    {
        public int Id { get; set; }

        public string InputCode { get; set; }
        
        public string CurrencyDescription { get; set; }

        public int InputCurrencyUnit { get; set; }

        public decimal DKKRate { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime CalculatedAt { get; set; }
    }
}