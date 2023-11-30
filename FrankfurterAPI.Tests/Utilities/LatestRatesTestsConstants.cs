namespace ExchangeRates.Tests.Utilities
{
    public static class LatestRatesTestsConstants
    {
        public const string LatestEndpoint = "latest";
        public const string LatestRatesTestsLogFilePath = "TestsLog/LatestRates-tests-log.txt";

        public const int ValidAmount = 1;
        public const int InvalidAmount = 1;

        public const string GetLatestExchangeRateEURToOtherCurrenciesMessage = "Get latest exchange rate EUR to other currencies";
        public const string GetLatestExchangeRateMissingParametersMessage = "Get latest exchange rate with missing parameters";
        public const string GetLatestExchangeRateInvalidCurrencyToOtherCurrenciesMessage = "Get latest exchange rate with invalid currency";
        public const string GetLatestExchangeRateUSDToOtherCurrenciesMessage = "Get latest exchange rate USD to other currencies";
        public const string GetExchangeRateWithLimitsEURToSpecificCurrenciesMessage = "Get latest exchange rate EUR with limit to specific currencies";
        public const string GetExchangeRateWithLimitsInvalidCurrencyToSpecificCurrenciesMessage = "Get latest exchange rate invalid curency with limit to specific currencies";
        public const string GetExchangeRateWithLimitsEURToInvalidCurrenciesMessage = "Get latest exchange rate EUR with limit to invalid currencies";
        public const string GetExchangeRateWithLimitsEURToMissingCurrenciesMessage = "Get latest exchange rate EUR with limit to missing currencies";
        public const string GetLatestExchangeRateValidAmountMessage = "Get latest exchange rate with valid amount";
        public const string GetLatestExchangeRateInvalidAmountMessage = "Get latest exchange rate with invalid amount";
    }
}
