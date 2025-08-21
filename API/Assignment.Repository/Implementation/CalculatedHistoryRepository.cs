using Assignment.Models.CurrencyRate;
using Assignment.Models.CurrencyRate.Tables;
using Assignment.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Repository.Implementation;

public class CalculatedHistoryRepository : ICalculatedHistoryRepository
{
    private readonly CurrencyRateDbContext _context;

    public CalculatedHistoryRepository(CurrencyRateDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CalculatedHistory>> GetAllHistoryAsync()
    {
        return await _context.CalculatedHistories.ToListAsync();
    }

    public async Task<IEnumerable<CalculatedHistory>> GetHistoryByInputCodeAndDateAsync(string inputCode, DateTime? startDate, DateTime? endDate)
    {

        var result = _context.CalculatedHistories.AsQueryable();

        if(!string.IsNullOrEmpty(inputCode))
        {
            result = result.Where(x => x.InputCode == inputCode);
        }

        if(startDate.HasValue)
        {
            result = result.Where(x => x.CalculatedAt >= startDate.Value);
        }

        if(endDate.HasValue)
        {
            result = result.Where(x => x.CalculatedAt <= endDate.Value);
        }

        return (await result.ToListAsync()).AsEnumerable();
    }

    public async Task AddHistoryAsync(CalculatedHistory history)
    {
        _context.CalculatedHistories.Add(history);
        await _context.SaveChangesAsync();
    }
}