using Assignment.Models.CurrencyRate;
using Assignment.Models.CurrencyRate.Tables;
using Assignment.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Repository.Implementation;

public class CurrencyRateRepository : ICurrencyRateRepository
{
    private readonly CurrencyRateDbContext _context;

    public CurrencyRateRepository(CurrencyRateDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CurrencyRate>> GetAllRatesAsync()
    {
        return await _context.CurrencyRates.ToListAsync();
    }

    public async Task<CurrencyRate?> GetRateByCurrencyCodeAsync(string code)
    {
        return await _context.CurrencyRates.Where(x=> x.CurrencyCode == code).FirstOrDefaultAsync();
    }

    public async Task DeleteAllRatesAsync()
    {
        await _context.CurrencyRates.ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }

    public Task AddBulkRatesAsync(IEnumerable<CurrencyRate> rates)
    {
        _context.CurrencyRates.AddRange(rates);
        return _context.SaveChangesAsync();
    }
}