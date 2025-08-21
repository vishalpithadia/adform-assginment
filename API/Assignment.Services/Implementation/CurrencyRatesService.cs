using Assignment.Models.CurrencyRate.DTO;
using Assignment.Models.CurrencyRate.Tables;
using Assignment.Repository.Interfaces;
using Assignment.Services.Interfaces;

namespace Assignment.Services.Implementation;


public class CurrencyRatesService : ICurrencyRatesService
{
    private readonly ICurrencyRateRepository _currencyRateRepository;

    private readonly ICalculatedHistoryRepository _calculatedHistoryRepository;

    public CurrencyRatesService(ICurrencyRateRepository currencyRateRepository, ICalculatedHistoryRepository calculatedHistoryRepository)
    {
        _currencyRateRepository = currencyRateRepository;
        _calculatedHistoryRepository = calculatedHistoryRepository;
    }

    public async Task<IEnumerable<CurrencyRate>> GetAllRatesAsync()
    {
        return await _currencyRateRepository.GetAllRatesAsync();
    }

    public async Task<CurrencyRateDTO?> GetRateByCurrencyCodeAsync(string code, int unit = 100)
    {
        var rate = await _currencyRateRepository.GetRateByCurrencyCodeAsync(code);
        if (rate == null)
        {
            return null;
        }

        var singleDKKRate = rate.Rate / 100; // Convert to single DKK rate as API returns rates per 100 foregin currency units

        // Map CurrencyRate to CurrencyRateDTO

        CurrencyRateDTO? currencyRateDTO = new CurrencyRateDTO
        {
            Description = rate.CurrencyDescription,
            DKKRate = singleDKKRate * unit, // Adjust rate based on the requested unit
            InputCode = rate.CurrencyCode,
            EffectiveDate = rate.EffectiveDate,
            InputCurrencyUnit = unit,
            CalculatedAt = DateTime.Now,
        };
        // Add history entry
        await AddHistoryAsync(currencyRateDTO);

        return currencyRateDTO;
    }
    
    public async Task AddHistoryAsync(CurrencyRateDTO history)
    {
        await _calculatedHistoryRepository.AddHistoryAsync(new CalculatedHistory
        { 
            InputCode = history.InputCode,
            CurrencyDescription = history.Description,
            InputCurrencyUnit = history.InputCurrencyUnit,
            Rate = history.DKKRate,
            EffectiveDate = history.EffectiveDate,
            CalculatedAt = history.CalculatedAt
        });
    }
}
