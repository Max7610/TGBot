
using System.Globalization;
using TGBot.Classes.telegramBot;

Console.WriteLine("Запуск программ");
A();
TelegramBot telegramBot = new TelegramBot();


Console.ReadKey();

void A()
{
    var d = Directory.GetFiles(Directory.GetCurrentDirectory());
    foreach(var i in d)
    {
        Console.WriteLine(i);
    }
    var b = Directory.GetDirectories(Directory.GetCurrentDirectory());
    foreach(var i in b)
    {
        Console.WriteLine(i);
    }
}

