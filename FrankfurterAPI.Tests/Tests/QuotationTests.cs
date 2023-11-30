using Microsoft.Extensions.Logging;
using Serilog;
using System.Text.Json;
using static ExchangeRates.Tests.Utilities.GeneralConstants;
using static ExchangeRates.Tests.Utilities.CurrencyConstants;
using static ExchangeRates.Tests.Utilities.QuotationTestsConstants;

namespace ExchangeRates.Tests.Tests
{
    [TestFixture]
    public class QuotationTests
    {
        private HttpClient client;
        private readonly ILogger<QuotationTests> _logger;

        public QuotationTests()
        {
            var loggerFactory = new LoggerFactory()
                .AddSerilog(new LoggerConfiguration()
                    .WriteTo.File(QuotationTestsLogFilePath)
                    .CreateLogger());

            _logger = loggerFactory.CreateLogger<QuotationTests>();
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
        public void GetCurrentRates()
        {
            // Arrange
            string currentDate = DateTime.Today.ToString(yyyyMMddDateFormat);
            string endpoint = $"{ApiBaseUrl}{currentDate}";

            // Act
            HttpResponseMessage response = client.GetAsync(endpoint).Result;

            // Assert: Verify that the response is successful
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.NotNull(response.Content);

            // Read the response content as a string
            string responseContent = response.Content.ReadAsStringAsync().Result;

            // Deserialize the JSON string
            JsonDocument jsonDocument = JsonDocument.Parse(responseContent);
            JsonElement jsonResponse = jsonDocument.RootElement;

            // Test: Check if the default currency is EUR
            JsonElement baseCurrencyElement = jsonResponse.GetProperty(BasePropertyName);
            string baseCurrency = baseCurrencyElement.GetString();

            Assert.AreEqual(EURName, baseCurrency);

            string defaultCurrencyTestLogMessage = $"{Test} {DefaultCurrencyEURMessage} {Result} {response.IsSuccessStatusCode}";

            Console.WriteLine(defaultCurrencyTestLogMessage);

            _logger.LogInformation(defaultCurrencyTestLogMessage);

            // Test: Check if JPY exists in rates
            JsonElement ratesElement = jsonResponse.GetProperty(RatesPropertyName);
            Assert.IsTrue(ratesElement.TryGetProperty(JPYName, out _));

            string jpyExistsInRatesTestLogMessage = $"{Test} {JPYExistsInRatesMessage} {Result} {response.IsSuccessStatusCode}";

            Console.WriteLine(jpyExistsInRatesTestLogMessage);

            _logger.LogInformation(jpyExistsInRatesTestLogMessage);

            // Test: Check if MLT does not exist in rates
            Assert.IsFalse(ratesElement.TryGetProperty(MLTName, out _));

            string mltDoesNotExistInRatesTestLogMessage = $"{Test} {MLTDoesNotExistInRatesMessage} {Result} {response.IsSuccessStatusCode}";

            Console.WriteLine(mltDoesNotExistInRatesTestLogMessage);

            _logger.LogInformation(mltDoesNotExistInRatesTestLogMessage);

            // Test: Check if amount is 1
            JsonElement amountElement = jsonResponse.GetProperty(AmountPropertyName);
            double amount = amountElement.GetDouble();
            Assert.AreEqual(1, amount);

            string amountIsOneTestLogMessage = $"{Test} {AmountMessage} {Result} {response.IsSuccessStatusCode}";

            Console.WriteLine(amountIsOneTestLogMessage);

            _logger.LogInformation(amountIsOneTestLogMessage);

            // Test: Check if date is the current date
            Assert.AreEqual(DateTime.Today.ToString(yyyyMMddDateFormat), currentDate);

            string dateIsCurrentDateTestLogMessage = $"{Test} {DateIsCurrentDateMessage} {Result} {response.IsSuccessStatusCode}";

            Console.WriteLine(dateIsCurrentDateTestLogMessage);

            _logger.LogInformation(dateIsCurrentDateTestLogMessage);

            // Test: Check if rates count is 30
            int ratesCount = ratesElement.EnumerateObject().Count();
            Assert.AreEqual(30, ratesCount);

            string rateCountTestLogMessage = $"{Test} {RatesCountMessage} {Result} {response.IsSuccessStatusCode}";

            Console.WriteLine(rateCountTestLogMessage);

            _logger.LogInformation(rateCountTestLogMessage);
        }

        [Test]
        public void CalculateAverageQuotationForUSDToBGN()
        {
            // Arrange
            DateTime startDate = new DateTime(2023, 01, 10);
            DateTime endDate = new DateTime(2023, 01, 15);

            // Act
            Dictionary<DateTime, double> usdToBgnQuotations = GetQuotationsForCurrencyPair(USDName, BGNName, startDate, endDate);

            // Assert: Calculate and verify the average quotation
            Assert.IsTrue(usdToBgnQuotations.Any(), NoDataFoundForSpecifiedPeriodMessage);

            double averageQuotation = usdToBgnQuotations.Values.Average();

            string logMessage = $"{Test} {AverageQuotationUSDToBGNMessage} {From} {startDate.ToShortDateString()} {To} {endDate.ToShortDateString()} {Result} {averageQuotation}";

            Console.WriteLine(logMessage);

            _logger.LogInformation(logMessage);
        }

        private Dictionary<DateTime, double> GetQuotationsForCurrencyPair(string baseCurrency, string targetCurrency, DateTime startDate, DateTime endDate)
        {
            Dictionary<DateTime, double> quotations = new Dictionary<DateTime, double>();

            while (startDate <= endDate)
            {
                string date = startDate.ToString(yyyyMMddDateFormat);
                string endpoint = $"{ApiBaseUrl}{date}";

                HttpResponseMessage response = client.GetAsync(endpoint).Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = response.Content.ReadAsStringAsync().Result;
                    JsonDocument jsonDocument = JsonDocument.Parse(responseContent);

                    if (jsonDocument.RootElement.TryGetProperty(RatesPropertyName, out JsonElement ratesElement) &&
                        ratesElement.TryGetProperty(targetCurrency, out JsonElement targetCurrencyQuotation))
                    {
                        double quotation = targetCurrencyQuotation.GetDouble();
                        quotations.Add(startDate, quotation);
                    }
                }

                startDate = startDate.AddDays(1);
            }

            return quotations;
        }

        [Test]
        public void ValidateQuotationsStartDateForBGN()
        {
            // Arrange
            DateTime startDate = new DateTime(2007, 07, 19);
            string date = startDate.ToString(yyyyMMddDateFormat);
            string endpoint = $"{ApiBaseUrl}{date}";

            // Act
            HttpResponseMessage response = client.GetAsync(endpoint).Result;

            // Assert: Verify that the response is successful and contains data for BGN
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.NotNull(response.Content);

            string responseContent = response.Content.ReadAsStringAsync().Result;
            JsonDocument jsonDocument = JsonDocument.Parse(responseContent);

            // Check if there are rates available for BGN on the specified date
            Assert.IsTrue(jsonDocument.RootElement.TryGetProperty(RatesPropertyName, out JsonElement ratesElement) &&
                          ratesElement.TryGetProperty(BGNName, out JsonElement bgnQuotation));

            string logMessage = $"{Test} {BGNQuotationsStartDateMessage} {startDate.ToShortDateString()} {Result} {response.IsSuccessStatusCode}";

            Console.WriteLine(logMessage);

            _logger.LogInformation(logMessage);
        }

        [Test]
        public void ValidateCurrencyExchangeOnSpecificDate()
        {
            // Arrange
            DateTime specificDate = new DateTime(2023, 11, 7);
            string baseCurrency = BGNName;
            string targetCurrency = HUFName;
            decimal amountToExchange = AmountToExchangeValue;
            decimal expectedTargetAmount = ExpectedTargetAmountValue;

            // Act
            decimal actualTargetAmount = GetTargetAmountOnSpecificDate(baseCurrency, targetCurrency, amountToExchange, specificDate);

            // Assert
            Assert.AreNotEqual(expectedTargetAmount, actualTargetAmount, $"{CurrencyExchangeValidationFailedMessage}");

            string logMessage = $"{Test} {On} {specificDate.ToShortDateString()} {amountToExchange} {baseCurrency} {CouldBuy} {expectedTargetAmount} {targetCurrency} {Result} {actualTargetAmount} {targetCurrency}";

            Console.WriteLine(logMessage);

            _logger.LogInformation(logMessage);
        }

        private decimal GetTargetAmountOnSpecificDate(string baseCurrency, string targetCurrency, decimal amountToExchange, DateTime specificDate)
        {
            string endpoint = $"{ApiBaseUrl}{specificDate.ToString(yyyyMMddDateFormat)}?from={baseCurrency}&to={targetCurrency}";

            HttpResponseMessage response = client.GetAsync(endpoint).Result;

            if (response.IsSuccessStatusCode)
            {
                string responseContent = response.Content.ReadAsStringAsync().Result;
                JsonDocument jsonDocument = JsonDocument.Parse(responseContent);

                if (jsonDocument.RootElement.TryGetProperty(RatesPropertyName, out JsonElement ratesElement) &&
                    ratesElement.TryGetProperty(targetCurrency, out JsonElement targetCurrencyQuotation))
                {
                    decimal quotation = (decimal)targetCurrencyQuotation.GetDouble();
                    return amountToExchange * quotation;
                }
            }

            return 0;
        }
    }
}
