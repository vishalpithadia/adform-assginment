using Assignment.Models.CurrencyRate.Tables;
using Assignment.Repository.Interfaces;
using Assignment.Services.Interfaces;

namespace Assignment.Services.Implementation;


public class CalculatedHistoryService : ICalculatedHistoryService
{
    private readonly ICalculatedHistoryRepository _calculatedHistoryRepository;

    public CalculatedHistoryService(ICalculatedHistoryRepository calculatedHistoryRepository)
    {
        _calculatedHistoryRepository = calculatedHistoryRepository;
    }

    public async Task<IEnumerable<CalculatedHistoryDTO>> GetAllHistoryAsync()
    {
        return (await _calculatedHistoryRepository.GetAllHistoryAsync()).Select(ToDTO());
    }

    public async Task<IEnumerable<CalculatedHistoryDTO>> GetHistoryByInputCodeAndDateAsync(string inputCode, DateTime? startDate, DateTime? endDate)
    {
        return (await _calculatedHistoryRepository.GetHistoryByInputCodeAndDateAsync(inputCode, startDate, endDate))
        .Select(ToDTO());
    }

    private static Func<CalculatedHistory, CalculatedHistoryDTO> ToDTO()
    {
        return x => new CalculatedHistoryDTO
        {
            Id = x.Id,
            InputCode = x.InputCode,
            CurrencyDescription = x.CurrencyDescription,
            InputCurrencyUnit = x.InputCurrencyUnit,
            DKKRate = x.Rate,
            EffectiveDate = x.EffectiveDate,
            CalculatedAt = x.CalculatedAt
        };
    }


    public async Task AddHistoryAsync(CalculatedHistory history)
    {
        await _calculatedHistoryRepository.AddHistoryAsync(history);
    }
}