using Assignment.Models.CurrencyRate.Tables;

namespace Assignment.Services.Interfaces;

public interface ICalculatedHistoryService
{
    Task<IEnumerable<CalculatedHistoryDTO>> GetAllHistoryAsync();
    Task<IEnumerable<CalculatedHistoryDTO>> GetHistoryByInputCodeAndDateAsync(string inputCode, DateTime? startDate, DateTime? endDate);
    Task AddHistoryAsync(CalculatedHistory history);
}
