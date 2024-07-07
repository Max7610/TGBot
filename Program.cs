
using System.Globalization;
using TGBot.Classes.telegramBot;


Console.WriteLine("Запуск программ"+ DateTime.Now);

try
{
    TelegramBot telegramBot = new TelegramBot();
}catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}

while (true)
{
    Thread.Sleep(5000000);
    Console.WriteLine(1);
}
Console.WriteLine("END");

