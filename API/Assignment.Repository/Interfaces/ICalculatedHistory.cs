using Assignment.Models.CurrencyRate.Tables;

namespace Assignment.Repository.Interfaces;


public interface ICalculatedHistoryRepository
{
    Task<IEnumerable<CalculatedHistory>> GetAllHistoryAsync();
    Task<IEnumerable<CalculatedHistory>> GetHistoryByInputCodeAndDateAsync(string inputCode, DateTime? startDate, DateTime? endDate);
    Task AddHistoryAsync(CalculatedHistory history);
}


