
using System.Globalization;
using TGBot.Classes.telegramBot;


Console.WriteLine("Запуск программ " + DateTime.Now);
Start();
while (true)
{
    try
    {
        var c = Console.In.Read();
        Console.WriteLine("input:" + c);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
Console.WriteLine("END");

async void Start()
{
    await Task.Run(() =>
    {
        TelegramBot telegramBot = new TelegramBot();
    });
}
