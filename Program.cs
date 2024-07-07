
using System.Globalization;
using TGBot.Classes.telegramBot;

string logFilePath = "path_to_your_log_file.log";

// Очистка файла лога
File.WriteAllText(logFilePath, string.Empty);


Console.WriteLine("Запуск программ" + DateTime.Now);
Start();
Console.WriteLine("END");

async void Start()
{
    await Task.Run(() =>
    {
        TelegramBot telegramBot = new TelegramBot();
    });
}