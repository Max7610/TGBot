
using System.Globalization;
using TGBot.Classes.telegramBot;

Console.WriteLine("Запуск программ");

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

