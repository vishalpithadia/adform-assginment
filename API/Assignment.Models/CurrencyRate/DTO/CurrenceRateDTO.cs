namespace Assignment.Models.CurrencyRate.DTO;



public class CurrencyRateDTO
{
    public string InputCode { get; set; }

    public string Description { get; set; }

    public int InputCurrencyUnit { get; set; }

    public decimal DKKRate { get; set; }
    public DateTime EffectiveDate { get; set; }
    public DateTime CalculatedAt { get; set; }

}