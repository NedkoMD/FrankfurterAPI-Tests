namespace ExchangeRates.Tests.Utilities
{
    public static class QuotationTestsConstants
    {
        public const string yyyyMMddDateFormat = "yyyy-MM-dd";
        public const string QuotationTestsLogFilePath = "TestsLog/Quotation-tests-log.txt";
        
        public const string BasePropertyName = "base";
        public const string RatesPropertyName = "rates";
        public const string AmountPropertyName = "amount";

        //GetCurrentRates constants
        public const string DefaultCurrencyEURMessage = "Default currency is EUR";
        public const string JPYExistsInRatesMessage = "JPY exists in rates";
        public const string MLTDoesNotExistInRatesMessage = "MLT does not exist in rates";
        public const string AmountMessage = "Amount is 1";
        public const string DateIsCurrentDateMessage = "Date is the current date";
        public const string RatesCountMessage = "Rates count is 30";

        //CalculateAverageQuotationForUSDToBGN constants
        public const string NoDataFoundForSpecifiedPeriodMessage = "No data found for the specified period";
        public const string AverageQuotationUSDToBGNMessage = "Average quotation for USD to BGN";
        public const string From = "from";
        public const string To = "to";

        //ValidateQuotationsStartDateForBGN constants
        public const string BGNQuotationsStartDateMessage = "Start date for quotations to BGN";
        public const string On = "On";
        public const string CouldBuy = "could by";

        //ValidateCurrencyExchangeOnSpecificDate constants
        public const string CurrencyExchangeValidationFailedMessage = "The currency exchange validation failed";
        public const decimal AmountToExchangeValue = 5;
        public const decimal ExpectedTargetAmountValue = 967.63m;
    }
}
