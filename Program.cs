
using System.Globalization;
using TGBot.Classes.telegramBot;


Console.WriteLine("Запуск программ " + DateTime.Now);
Start();
Console.In.Read();
Console.WriteLine("END");

async void Start()
{
    await Task.Run(() =>
    {
        TelegramBot telegramBot = new TelegramBot();
    });
}
