using Microsoft.Extensions.Logging;
using Serilog;
using static ExchangeRates.Tests.Utilities.GeneralConstants;
using static ExchangeRates.Tests.Utilities.LatestRatesTestsConstants;
using static ExchangeRates.Tests.Utilities.CurrencyConstants;

namespace ExchangeRates.Tests.Tests
{
    [TestFixture]
    public class LatestRatesTests
    {
        private HttpClient client;
        private readonly ILogger<QuotationTests> _logger;

        public LatestRatesTests()
        {
            var loggerFactory = new LoggerFactory()
                .AddSerilog(new LoggerConfiguration()
                    .WriteTo.File(LatestRatesTestsLogFilePath)
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
        public void GetLatestExchangeRate_EURToOtherCurrencies_Success()
        {
            // Act
            HttpResponseMessage response = client.GetAsync($"{ApiBaseUrl}{LatestEndpoint}?from={EURName}").Result;

            // Assert
            Assert.IsTrue(response.IsSuccessStatusCode);

            Console.WriteLine(response.IsSuccessStatusCode);

            _logger.LogInformation($"{Test} {GetLatestExchangeRateEURToOtherCurrenciesMessage} {Result} {response.IsSuccessStatusCode}");
        }

        [Test]
        public void GetLatestExchangeRate_MissingParameters_Fails()
        {

            // Act
            HttpResponseMessage response = client.GetAsync($"{ApiBaseUrl}{LatestEndpoint}?from={MissingCurrency}").Result;

            // Assert
            Assert.IsFalse(response.IsSuccessStatusCode);

            Console.WriteLine(response.IsSuccessStatusCode);

            _logger.LogInformation($"{Test} {GetLatestExchangeRateMissingParametersMessage} {Result} {response.IsSuccessStatusCode}");
        }

        [Test]
        public void GetLatestExchangeRate_InvalidCurrencyToOtherCurrencies_Fails()
        {
            // Act
            HttpResponseMessage response = client.GetAsync($"{ApiBaseUrl}{LatestEndpoint}?from={InvalidCurrency}").Result;

            // Assert
            Assert.IsFalse(response.IsSuccessStatusCode);

            Console.WriteLine(response.IsSuccessStatusCode);

            _logger.LogInformation($"{Test} {GetLatestExchangeRateInvalidCurrencyToOtherCurrenciesMessage} {Result} {response.IsSuccessStatusCode}");
        }

        [Test]
        public void GetLatestExchangeRate_USDToOtherCurrencies_Success()
        {
            // Act
            HttpResponseMessage response = client.GetAsync($"{ApiBaseUrl}{LatestEndpoint}?from={USDName}").Result;

            // Assert
            Assert.IsTrue(response.IsSuccessStatusCode);

            Console.WriteLine(response.IsSuccessStatusCode);

            _logger.LogInformation($"{Test} {GetLatestExchangeRateUSDToOtherCurrenciesMessage} {Result} {response.IsSuccessStatusCode}");
        }

        [Test]
        public void GetExchangeRateWithLimits_EURToSpecificCurrencies_Success()
        {
            // Act
            HttpResponseMessage response = client.GetAsync($"{ApiBaseUrl}{LatestEndpoint}?from={EURName}&to={GDPName},{USDName}").Result;

            // Assert
            Assert.IsTrue(response.IsSuccessStatusCode);

            Console.WriteLine(response.IsSuccessStatusCode);

            _logger.LogInformation($"{Test} {GetExchangeRateWithLimitsEURToSpecificCurrenciesMessage} {Result} {response.IsSuccessStatusCode}");
        }

        [Test]
        public void GetExchangeRateWithLimits_InvalidCurrencyToSpecificCurrencies_Fails()
        {
            // Act
            HttpResponseMessage response = client.GetAsync($"{ApiBaseUrl}{LatestEndpoint}?from={InvalidCurrency}&to={GDPName},{USDName}").Result;

            // Assert
            Assert.IsFalse(response.IsSuccessStatusCode);

            Console.WriteLine(response.IsSuccessStatusCode);

            _logger.LogInformation($"{Test} {GetExchangeRateWithLimitsInvalidCurrencyToSpecificCurrenciesMessage} {Result} {response.IsSuccessStatusCode}");
        }

        [Test]
        public void GetExchangeRateWithLimits_EURToInvalidCurrencies_Fails()
        {
            // Act
            HttpResponseMessage response = client.GetAsync($"{ApiBaseUrl}{LatestEndpoint}?from={EURName}&to={InvalidCurrency}, {InvalidCurrency}").Result;

            // Assert
            Assert.IsFalse(response.IsSuccessStatusCode);

            Console.WriteLine(response.IsSuccessStatusCode);

            _logger.LogInformation($"{Test} {GetExchangeRateWithLimitsEURToInvalidCurrenciesMessage} {Result} {response.IsSuccessStatusCode}");
        }

        [Test]
        public void GetExchangeRateWithLimits_EURToMissingCurrencies_Fails()
        {
            // Act
            HttpResponseMessage response = client.GetAsync($"{ApiBaseUrl}{LatestEndpoint}?from={EURName}&to={MissingCurrency}").Result;

            // Assert
            Assert.IsFalse(response.IsSuccessStatusCode);

            Console.WriteLine(response.IsSuccessStatusCode);

            _logger.LogInformation($"{Test} {GetExchangeRateWithLimitsEURToMissingCurrenciesMessage} {Result} {response.IsSuccessStatusCode}");
        }

        [Test]
        public void GetLatestExchangeRate_ValidAmount_Success()
        {
            // Act
            HttpResponseMessage response = client.GetAsync($"{ApiBaseUrl}{LatestEndpoint}?amount={ValidAmount}").Result;

            // Assert
            Assert.IsTrue(response.IsSuccessStatusCode);

            Console.WriteLine(response.IsSuccessStatusCode);

            _logger.LogInformation($"{Test} {GetLatestExchangeRateValidAmountMessage} {Result} {response.IsSuccessStatusCode}");
        }

        [Test]
        public void GetLatestExchangeRate_InvalidAmount_Fails()
        {
            // Act
            HttpResponseMessage response = client.GetAsync($"{ApiBaseUrl}docs/{LatestEndpoint}?amount={InvalidAmount}").Result;

            // Assert
            Assert.IsFalse(response.IsSuccessStatusCode);

            Console.WriteLine(response.IsSuccessStatusCode);

            _logger.LogInformation($"{Test} {GetLatestExchangeRateInvalidAmountMessage} {Result} {response.IsSuccessStatusCode}");
        }
    }
}