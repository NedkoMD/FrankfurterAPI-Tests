namespace ExchangeRates.Tests.Utilities
{
    public static class HistoricalTestsConstants
    {
        public const string HistoricalTestsLogFilePath = "TestsLog/HistoricalRates-tests-log.txt";

        public const string ValidDate = "1999-01-04";
        public const string ValidEndDate = "2020-01-04";
        public const string InvalidDate = "1999-111-04";
        public const string FutureDate = "2328-01-04";
        public const string MissingDate = "";

        public const string GetHistoricalRatesValidDateMessage = "Get historical rates with valid date";
        public const string GetHistoricalRatesInvalidDateMessage = "Get historical rates with invalid date";
        public const string GetHistoricalRatesFutureDateMessage = "Get historical rates with future date";
        public const string GetHistoricalRatesMissingDateMessage = "Get historical rates with missing date";
        public const string GetHistoricalRatesByPeriodValidPeriodMessage = "Get historical rates by period with valid start and end date";
        public const string GetHistoricalRatesByPeriodInvalidStartDateMessage = "Get historical rates by period with invalid start date";
        public const string GetHistoricalRatesByPeriodMissingStartDateMessage = "Get historical rates by period with missing start date";
        public const string GetHistoricalRatesByPeriodInvalidEndDateMessage = "Get historical rates by period with invalid end date";
        public const string GetHistoricalRatesByPeriodMissingEndDateMessage = "Get historical rates by period with missing end date";
        public const string GetHistoricalRatesByPeriodFinalDateBeforeStartDateMessage = "Get historical rates by period where end date is before start date";
        public const string GetHistoricalRatesByPeriodToValidCurrencyMessage = "Get historical rates by period to valid currency";
        public const string GetHistoricalRatesByPeriodToInvalidCurrencyMessage = "Get historical rates by period to invalid currency";
    }
}
