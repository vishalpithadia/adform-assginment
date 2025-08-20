using System.Xml.Serialization;
using Assignment.Models.Xml;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExchangeRateController : ControllerBase
    {
        // GET: api/ExchangeRate
        [HttpGet]
        public async Task<IActionResult> GetAllRates()
        {

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://www.nationalbanken.dk");
            var xml = await httpClient.GetStringAsync("/api/currencyratesxml?lang=da");

            XmlSerializer serializer = new XmlSerializer(typeof(Exchangerates));
            using (StringReader reader = new StringReader(xml))
            {
                var poco = (Exchangerates)serializer.Deserialize(reader);
                if(poco != null)
                {
                    return Ok(poco);
                }
            }


            // Placeholder for fetching all exchange rates
            return Ok(new { Message = "List of exchange rates." });
        }
    }
}