using System.Xml.Serialization;
using Assignment.Models.Xml;
using Assignment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly ICurrencyRatesService _currencyRatesService;
        private readonly ICalculatedHistoryService _calculatedHistoryService;

        public ExchangeRateController(ICurrencyRatesService currencyRatesService, ICalculatedHistoryService calculatedHistoryService)
        {
            _currencyRatesService = currencyRatesService;
            _calculatedHistoryService = calculatedHistoryService;
        }

        // GET: api/ExchangeRate
        /// <summary>
        /// Get all exchange rates.
        /// This endpoint fetches the latest exchange rates from the external source and returns them.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllRates()
        {
            // Placeholder for fetching all exchange rates
            return Ok(new { data = await _currencyRatesService.GetAllRatesAsync() });
        }

        // GET: api/ExchangeRate/{code}
        /// <summary>
        /// Get exchange rate by currency code.
        /// This endpoint retrieves the exchange rate For DKK with Input Currency and Unit. 
        /// </summary>
        /// <param name="code">Example USD or AUD</param>
        /// <param name="unit">Should be greater than 1</param>
        /// <returns></returns>
        [HttpGet("{code}/{unit:int:min(1)}")]
        public async Task<IActionResult> GetRateByCurrencyCode(string code, int unit)
        {
            var rate = await _currencyRatesService.GetRateByCurrencyCodeAsync(code, unit);
            if (rate == null)
            {
                return NotFound(new
                {
                    message = "Currency code not found.",
                    availableCodes = (await _currencyRatesService.GetAllRatesAsync()).Select(r => r.CurrencyCode)
                });
            }
            return Ok(rate);
        }

        /// <summary>
        /// Get calculated history for a specific currency code within a date range.
        /// If no dates are provided, it returns all history for the specified currency code.
        /// </summary>
        /// <param name="code">Example USD or AUD or keep it empty</param>
        /// <param name="startDate">ISO formated Date Example 2018-08-17T07:00:00</param>
        /// <param name="endDate">ISO formated Date Example 2018-08-17T07:00:00</param>
        /// <returns></returns>
        [HttpGet("history")]
        public async Task<IActionResult> GetCalulatedHistory(string code = "", DateTime? startDate = null, DateTime? endDate = null)
        {
            var history = await _calculatedHistoryService.GetHistoryByInputCodeAndDateAsync(code, startDate, endDate);
            if (history == null || !history.Any())
            {
                return NotFound(new { message = "No calculated history found for the specified currency code & startdate & enddate" });
            }
            return Ok(history);
        }
    }
}