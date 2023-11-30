using Microsoft.Extensions.Logging;
using Serilog;
using System.Text.Json;
using static ExchangeRates.Tests.Utilities.GeneralConstants;
using static ExchangeRates.Tests.Utilities.CurrencyConstants;
using static ExchangeRates.Tests.Utilities.CurrencyAbbreviationTestsConstants;

namespace ExchangeRates.Tests.Tests
{
    [TestFixture]
    public class CurrencyAbbreviationTests
    {
        private HttpClient client;
        private readonly ILogger<CurrencyAbbreviationTests> _logger;

        public CurrencyAbbreviationTests()
        {
            var loggerFactory = new LoggerFactory()
                .AddSerilog(new LoggerConfiguration()
                    .WriteTo.File(CurrencyAbbreviationTestsLogFilePath)
                    .CreateLogger());

            _logger = loggerFactory.CreateLogger<CurrencyAbbreviationTests>();
        }

        [SetUp]
        public void Setup()
        {
            client = new HttpClient();
        }

        [TearDown]
        public void TearDown()
        {
            client.Dispose();
        }

        [Test]
        public void CheckCurrencyAbbreviationForNOK()
        {
            // Arrange
            string endpoint = $"{ApiBaseUrl}{CurrenciesEndpoint}";

            // Act
            HttpResponseMessage response = client.GetAsync(endpoint).Result;

            // Assert
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.NotNull(response.Content);

            // Read the response content as a string
            string responseContent = response.Content.ReadAsStringAsync().Result;

            // Deserialize the JSON string
            JsonDocument jsonDocument = JsonDocument.Parse(responseContent);
            JsonElement currenciesJson = jsonDocument.RootElement;

            // Test: Check if "NOK" is the abbreviation for "Norwegian Krone"
            JsonElement nokElement;
            bool nokExists = currenciesJson.TryGetProperty(NOKName, out nokElement);

            Assert.IsTrue(nokExists);
            Assert.AreEqual(NorwegianKroneName, nokElement.GetString());

            Console.WriteLine(response.IsSuccessStatusCode);

            _logger.LogInformation($"{Test} {CheckCurrencyAbbreviationForNOKMessage} {Result} {response.IsSuccessStatusCode}");
        }
    }
}
