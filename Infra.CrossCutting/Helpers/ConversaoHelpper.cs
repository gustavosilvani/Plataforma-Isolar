namespace Infra.CrossCutting.Helpers
{
    public static class ConversaoHelpper
    {
        public static double ParaDouble(string input)
        {
            if (double.TryParse(input, out double result))
                return result;
            return 0;
        }
    }
}
