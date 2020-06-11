namespace Validator
{
    public static class AmountValidator
    {
        public static bool IsValidAmount(string amountString, out int amount)
        {
            return int.TryParse(amountString, out amount) && amount >= 1;
        }
    }
}
