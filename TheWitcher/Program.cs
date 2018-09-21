namespace TheWitcher
{
    class Program
    {
        private delegate void Checking();
        static void Main(string[] args)
        {
            Builder.ConfigureContainer();
        }
    }

}
