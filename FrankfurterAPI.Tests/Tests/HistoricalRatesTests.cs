using Microsoft.Extensions.Logging;
using Serilog;
using static ExchangeRates.Tests.Utilities.GeneralConstants;
using static ExchangeRates.Tests.Utilities.HistoricalTestsConstants;
using static ExchangeRates.Tests.Utilities.CurrencyConstants;

namespace ExchangeRates.Tests.Tests
{
    [TestFixture]
    public class HistoricalRatesTests
    {
        private HttpClient client;
        private readonly ILogger<HistoricalRatesTests> _logger;

        public HistoricalRatesTests()
        {
            var loggerFactory = new LoggerFactory()
                .AddSerilog(new LoggerConfiguration()
                    .WriteTo.File(HistoricalTestsLogFilePath)
                    .CreateLogger());

            _logger = loggerFactory.CreateLogger<HistoricalRatesTests>();
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
        public void GetHistoricalRates_ValidDate_Success()
        {
            // Arrange
            string endpoint = $"{ApiBaseUrl}{ValidDate}";

            // Act
            HttpResponseMessage response = client.GetAsync(endpoint).Result;

            // Assert
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.NotNull(response.Content);

            Console.WriteLine(response.IsSuccessStatusCode);

            string logMessage = $"{Test} {GetHistoricalRatesValidDateMessage} {Result} {response.IsSuccessStatusCode}";

            _logger.LogInformation(logMessage);
        }

        [Test]
        public void GetHistoricalRates_InvalidDate_Fails()
        {
            // Arrange
            string endpoint = $"{ApiBaseUrl}{InvalidDate}";

            // Act
            HttpResponseMessage response = client.GetAsync(endpoint).Result;

            // Assert
            Assert.IsFalse(response.IsSuccessStatusCode);

            Console.WriteLine(response.IsSuccessStatusCode);

            string logMessage = $"{Test} {GetHistoricalRatesInvalidDateMessage} {Result} {response.IsSuccessStatusCode}";

            _logger.LogInformation(logMessage);
        }

        [Test]
        public void GetHistoricalRates_FutureDate_Fails()
        {
            // Arrange
            string endpoint = $"{ApiBaseUrl}docs/{FutureDate}";

            // Act
            HttpResponseMessage response = client.GetAsync(endpoint).Result;

            // Assert
            Assert.IsFalse(response.IsSuccessStatusCode);

            Console.WriteLine(response.IsSuccessStatusCode);

            string logMessage = $"{Test} {GetHistoricalRatesFutureDateMessage} {Result} {response.IsSuccessStatusCode}";

            _logger.LogInformation(logMessage);
        }

        [Test]
        public void GetHistoricalRates_MissingDate_Fails()
        {
            // Arrange
            string endpoint = $"{ApiBaseUrl}docs/{MissingDate}";

            // Act
            HttpResponseMessage response = client.GetAsync(endpoint).Result;

            // Assert
            Assert.IsFalse(response.IsSuccessStatusCode);

            Console.WriteLine(response.IsSuccessStatusCode);

            string logMessage = $"{Test} {GetHistoricalRatesMissingDateMessage} {Result} {response.IsSuccessStatusCode}";

            _logger.LogInformation(logMessage);
        }

        [Test]
        public void GetHistoricalRatesByPeriod_ValidPeriod_Success()
        {
            // Arrange
            string endpoint = $"{ApiBaseUrl}{ValidDate}..{ValidEndDate}";

            // Act
            HttpResponseMessage response = client.GetAsync(endpoint).Result;

            // Assert
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.NotNull(response.Content);

            Console.WriteLine(response.IsSuccessStatusCode);

            string logMessage = $"{Test} {GetHistoricalRatesByPeriodValidPeriodMessage} {Result} {response.IsSuccessStatusCode}";

            _logger.LogInformation(logMessage);
        }

        [Test]
        public void GetHistoricalRatesByPeriod_InvalidStartDate_Fails()
        {
            // Arrange
            string endpoint = $"{ApiBaseUrl}{InvalidDate}..{ValidDate}";

            // Act
            HttpResponseMessage response = client.GetAsync(endpoint).Result;

            // Assert
            Assert.IsFalse(response.IsSuccessStatusCode);

            Console.WriteLine(response.IsSuccessStatusCode);

            string logMessage = $"{Test} {GetHistoricalRatesByPeriodInvalidStartDateMessage} {Result} {response.IsSuccessStatusCode}";

            _logger.LogInformation(logMessage);
        }

        [Test]
        public void GetHistoricalRatesByPeriod_InvalidFinalDate_Fails()
        {
            // Arrange
            string endpoint = $"{ApiBaseUrl}{ValidDate}..{InvalidDate}";

            // Act
            HttpResponseMessage response = client.GetAsync(endpoint).Result;

            // Assert
            Assert.IsFalse(response.IsSuccessStatusCode);

            Console.WriteLine(response.IsSuccessStatusCode);

            string logMessage = $"{Test} {GetHistoricalRatesByPeriodInvalidEndDateMessage} {Result} {response.IsSuccessStatusCode}";

            _logger.LogInformation(logMessage);
        }

        [Test]
        public void GetHistoricalRatesByPeriod_FinalDateBeforeStartDate_Fails()
        {
            // Arrange
            string endpoint = $"{ApiBaseUrl}{ValidEndDate}..{ValidDate}";

            // Act
            HttpResponseMessage response = client.GetAsync(endpoint).Result;

            // Assert
            Assert.IsFalse(response.IsSuccessStatusCode);

            Console.WriteLine(response.IsSuccessStatusCode);

            string logMessage = $"{Test} {GetHistoricalRatesByPeriodFinalDateBeforeStartDateMessage} {Result} {response.IsSuccessStatusCode}";

            _logger.LogInformation(logMessage);
        }

        [Test]
        public void GetHistoricalRatesByPeriod_MissingStartDate_Fails()
        {
            // Arrange
            string endpoint = $"{ApiBaseUrl}{MissingDate}..{ValidEndDate}";

            // Act
            HttpResponseMessage response = client.GetAsync(endpoint).Result;

            // Assert
            Assert.IsFalse(response.IsSuccessStatusCode);

            Console.WriteLine(response.IsSuccessStatusCode);

            string logMessage = $"{Test} {GetHistoricalRatesByPeriodMissingStartDateMessage} {Result} {response.IsSuccessStatusCode}";

            _logger.LogInformation(logMessage);
        }

        [Test]
        public void GetHistoricalRatesByPeriod_MissingEndDate_Success()
        {
            // Arrange
            string endpoint = $"{ApiBaseUrl}{ValidDate}..{ValidEndDate}";

            // Act
            HttpResponseMessage response = client.GetAsync(endpoint).Result;

            // Assert
            Assert.IsTrue(response.IsSuccessStatusCode);

            Console.WriteLine(response.IsSuccessStatusCode);

            string logMessage = $"{Test} {GetHistoricalRatesByPeriodMissingEndDateMessage} {Result} {response.IsSuccessStatusCode}";

            _logger.LogInformation(logMessage);
        }

        [Test]
        public void GetHistoricalRatesByPeriodToCurrency_ValidCurrency_Success()
        {
            // Arrange
            string endpoint = $"{ApiBaseUrl}{ValidDate}..{ValidEndDate}?to={USDName}";

            // Act
            HttpResponseMessage response = client.GetAsync(endpoint).Result;

            // Assert
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.NotNull(response.Content);

            Console.WriteLine(response.IsSuccessStatusCode);

            string logMessage = $"{Test} {GetHistoricalRatesByPeriodToValidCurrencyMessage} {Result} {response.IsSuccessStatusCode}";

            _logger.LogInformation(logMessage);
        }

        [Test]
        public void GetHistoricalRatesByPeriodToCurrency_InvalidCurrency_Fails()
        {
            // Arrange
            string endpoint = $"{ApiBaseUrl}{ValidDate}..{ValidEndDate}?to={InvalidCurrency}";

            // Act
            HttpResponseMessage response = client.GetAsync(endpoint).Result;

            // Assert
            Assert.IsFalse(response.IsSuccessStatusCode);

            Console.WriteLine(response.IsSuccessStatusCode);

            string logMessage = $"{Test} {GetHistoricalRatesByPeriodToInvalidCurrencyMessage} {Result} {response.IsSuccessStatusCode}";

            _logger.LogInformation(logMessage);
        }
    }
}
