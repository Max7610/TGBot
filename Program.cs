
using System.Globalization;
using TGBot.Classes.telegramBot;


Console.WriteLine("Запуск программ " + DateTime.Now);
Start();
while (true)
{
    try
    {
    Console.In.Read();
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
