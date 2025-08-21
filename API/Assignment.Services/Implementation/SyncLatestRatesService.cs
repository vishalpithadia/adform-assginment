using System.Xml.Serialization;
using Assignment.Models.CurrencyRate.Tables;
using Assignment.Models.Xml;
using Assignment.Repository.Interfaces;
using Assignment.Services.Interfaces;
namespace Assignment.Services.Implementation;

public class SyncLatestRatesService : ISyncLatestRatesService
{
    private readonly ICurrencyRateRepository _currencyRateRepository;

    public SyncLatestRatesService(ICurrencyRateRepository currencyRateRepository)
    {
        _currencyRateRepository = currencyRateRepository;
    }

    public async Task SyncLatestRatesAsync()
    {
        // Logic to sync latest rates
        var latestRates = await FetchLatestRatesFromExternalSourceAsync();
        if (latestRates.Any())
        {
            // Clear existing rates
            await _currencyRateRepository.DeleteAllRatesAsync();
        }
        await _currencyRateRepository.AddBulkRatesAsync(latestRates);
    }

    private async Task<IEnumerable<CurrencyRate>> FetchLatestRatesFromExternalSourceAsync()
    {
        HttpClient httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://www.nationalbanken.dk");
        var xml = await httpClient.GetStringAsync("/api/currencyratesxml?lang=da");

        XmlSerializer serializer = new XmlSerializer(typeof(Exchangerates));
        using (StringReader reader = new StringReader(xml))
        {
            var poco = (Exchangerates)serializer.Deserialize(reader);
            if (poco?.Dailyrates?.Currency != null)
            {
                return poco.Dailyrates.Currency.Select(c => new CurrencyRate
                {
                    CurrencyCode = c.Code,
                    Rate = c.Rate,
                    CurrencyDescription = c.Desc,
                    EffectiveDate = poco.Dailyrates.Id
                });
            }
            else
            {
                return Enumerable.Empty<CurrencyRate>();
            }
        }
    }
}


