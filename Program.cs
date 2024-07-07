
using System.Globalization;
using TGBot.Classes.telegramBot;


Console.WriteLine("Запуск программ " + DateTime.Now);
Start();
Console.ReadLine();
Console.WriteLine("END");

async void Start()
{
    await Task.Run(() =>
    {
        TelegramBot telegramBot = new TelegramBot();
    });
}