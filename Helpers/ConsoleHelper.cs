namespace Cards.Helpers
{
    public static class ConsoleHelper
    {
        public static string PromptUser(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine() ?? "";
        }
    }
}
