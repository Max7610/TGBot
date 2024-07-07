
using System.Globalization;
using TGBot.Classes.telegramBot;


Console.WriteLine("Запуск программ" + DateTime.Now);
Start();
if (Console.IsInputRedirected)
{
    Console.WriteLine("Console input is redirected. Please provide input through arguments or other means.");
}
else
{
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}
Console.WriteLine("END");

async void Start()
{
    await Task.Run(() =>
    {
        TelegramBot telegramBot = new TelegramBot();
    });
}