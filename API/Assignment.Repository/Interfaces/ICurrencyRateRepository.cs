using Assignment.Models.CurrencyRate.Tables;

namespace Assignment.Repository.Interfaces;

public interface ICurrencyRateRepository
{
    Task<IEnumerable<CurrencyRate>> GetAllRatesAsync();
    Task<CurrencyRate?> GetRateByCurrencyCodeAsync(string code);
    Task DeleteAllRatesAsync();
    Task AddBulkRatesAsync(IEnumerable<CurrencyRate> rates);
}