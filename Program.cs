
using System.Globalization;
using TGBot.Classes.telegramBot;

DateTime d = new DateTime();
Console.WriteLine("Запуск программ"+ d.Date.ToString());

try
{
    TelegramBot telegramBot = new TelegramBot();
}catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}

while (true)
{
    Thread.Sleep(500000);
}
Console.WriteLine("END");

