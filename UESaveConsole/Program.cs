using Newtonsoft.Json;
using System.Text.Json;
using UESave;

namespace UESaveConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UESave.UESave save = new();
            save.Load("C:\\Users\\XXXXXXXXXX\\AppData\\Local\\Pal\\Saved\\SaveGames\\76561198963400494\\E6AD16E0483C7C636EC70A9342ACB957\\Level.sav");

            Console.Write(JsonConvert.SerializeObject(save));
        }
    }
}