using Assignment.Models.CurrencyRate.DTO;
using Assignment.Models.CurrencyRate.Tables;

namespace Assignment.Services.Interfaces
{
    public interface ICurrencyRatesService
    {
        Task<IEnumerable<CurrencyRate>> GetAllRatesAsync();
        Task<CurrencyRateDTO> GetRateByCurrencyCodeAsync(string code,int unit = 100);
    }
}