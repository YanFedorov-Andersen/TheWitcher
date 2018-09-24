namespace TheWitcher
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Exeption: {0}", "исключение");
            Builder.ConfigureContainer();
        }
    }

}
