namespace saucedemotests.utils
{
    public static class NumberUtils
    {
        public static int GetIntFromStringWithDefault(string str, int defaultValue)
        {
            if (int.TryParse(str, out int valueToReturn))
            {
                return valueToReturn;
            }
            else
            {
                return defaultValue;
            }

        }
    }
}