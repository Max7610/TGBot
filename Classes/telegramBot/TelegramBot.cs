using System;

using TGBot.Classes.neuron;
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Args;
using System.IO;
using Telegram.Bot.Types.InputFiles;

namespace TGBot.Classes.telegramBot
{
    internal class TelegramBot
    {
        bool status = false;
        bool auto = false;
        decimal step;

        NeuronMeneger neuron;
        FileMeneger file;
        string help = "/a - новое обучение ( указать количество повторений и шаг\n" +
            "/auto - режим авто обучения задается только шаг\n" +
            "/m создать новую нейросеть (указать количество элементов на каждом из слоев)\n" +
            "/s сохранить текущую сеть (указать имя)\n" +
            "/o загрузить нейросеть \n" +
            "/l список доступных сохранений \n" +
            "/p структура нейросети \n" +
            "/img отправить картинку" +
            "/t тестовый запуск";
        public TelegramBot()
        {
            Console.WriteLine("Запуск");
            try
            {
                file = new FileMeneger();
            }
            catch
            {
                Console.WriteLine($"Ошибка создания FileMeneger {DateTime.Now}");
            }
            var client = new TelegramBotClient("5688737254:AAHRad9LW7Bd_joHoIgll4uylSzKS95P-9U");
            client.StartReceiving(Update, Error);
            Console.WriteLine("Запущен");
           
        }
        async Task Error(ITelegramBotClient botClient, Exception update, CancellationToken token)
        {
            return;
        }

        async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var message = update.Message;

            if (message != null)
            {
                if (message.Text != null)
                {
                    var MessText = update.Message.Text.Split('_');
                    Console.WriteLine(update.Message.Text);
                    if (MessText[0] == "/a")
                    {
                        if (status)
                        {
                            try
                            {
                                A(botClient, update, Convert.ToInt32(MessText[1]), Convert.ToDecimal(MessText[2]));
                            }catch
                            {
                                Console.WriteLine("ошибка ввода обучения");
                            }
                            //string mess = LearnNeuron(Convert.ToInt32(MessText[1]), Convert.ToDecimal(MessText[2]));
                            //botClient.SendTextMessageAsync(update.Message.Chat.Id, mess);
                        }
                        else
                        {
                            botClient.SendTextMessageAsync(update.Message.Chat.Id, "перед обучением создайте нейронку");
                        }
                    }
                    if (MessText[0] == "/auto")
                    {
                        if (status)
                        {
                            step = Convert.ToDecimal(MessText[1]);
                            try
                            {
                                AutoA(botClient, update);
                            }catch
                            {
                                Console.WriteLine("Ошибка запуска автообучения");
                            }
                        }
                        else
                        {
                            botClient.SendTextMessageAsync(update.Message.Chat.Id, "перед обучением создайте нейронку");
                        }
                    }
                    if (MessText[0] == "/help")
                    {
                        botClient.SendTextMessageAsync(update.Message.Chat.Id, help);
                    }
                    if (MessText[0] == "/s")
                    {
                        if (status)
                        {
                            try
                            {
                                neuron.FileSave();
                            }catch
                            {
                                Console.WriteLine("Ошибка при сохранении файла");
                            }
                        }
                        else
                        {
                            botClient.SendTextMessageAsync(update.Message.Chat.Id, "перед сохранением создайте нейронку");
                        }
                    }
                    if (MessText[0] == "/m")
                    {
                        try
                        {
                            int[] st = statusRead(MessText);
                            botClient.SendTextMessageAsync(update.Message.Chat.Id, statusToString(st));
                            neuron = new NeuronMeneger(st);
                            status = true;
                        }catch
                        {
                            Console.WriteLine("Ошибка создания нейросети");
                            botClient.SendTextMessageAsync(update.Message.Chat.Id, "Ошибка создания нейросети");
                        }
                    }
                    if (MessText[0] == "/t")
                    {
                        if (status)
                        {
                            try
                            {
                                var s = neuron.TestForFile();
                                botClient.SendTextMessageAsync(update.Message.Chat.Id, s);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Тест провален. {ex.Message}");
                            }
                            try
                            {
                                Tbmp(botClient, update);
                            }
                            catch (Exception ex)
                            {
                                botClient.SendTextMessageAsync(update.Message.Chat.Id, ex.Message);
                            }
                        }
                        else
                        {
                            botClient.SendTextMessageAsync(update.Message.Chat.Id, "перед запуском создайте нейронку");
                        }
                    }
                    if (MessText[0] == "/l")
                    {
                        string mass = "";
                        int n = 0;
                        try { 
                        foreach (var i in file.SaveList())
                        {
                            mass += $"{n}) {i.Split('/')[i.Split('/').Length-1]}\n";
                            Console.WriteLine($"{n}) {i.Split('/')[i.Split('/').Length - 1]}\n");
                            n++;
                        }
                        }
                        catch(Exception ex) { Console.WriteLine("Ошибка создания списка сохранений"+ex.Message); }
                        try
                        {
                            botClient.SendTextMessageAsync(update.Message.Chat.Id, mass);
                        }catch
                        {
                            Console.WriteLine("Ошибка отправки списка сохранений");
                        }
                    }
                    if (MessText[0] == "/o")
                    {
                        try
                        {
                            neuron = new NeuronMeneger(file.SaveList()[Convert.ToInt32(MessText[1])]);
                            status = true;
                        }
                        catch (Exception ex)
                        {
                            botClient.SendTextMessageAsync(update.Message.Chat.Id, ex.Message);
                            Console.WriteLine(ex.Message);
                        }
                    }
                    if (MessText[0] == "/img")
                    {
                        Tbmp(botClient, update);
                    }
                    if (MessText[0] == "/p")
                    {
                        if (status)
                        {
                            string mes = $"{neuron.NeyronStruct()} \nauto - {auto} step -{step}";
                            botClient.SendTextMessageAsync(update.Message.Chat.Id, neuron.NeyronStruct());
                        }
                        else
                        {
                            botClient.SendTextMessageAsync(update.Message.Chat.Id, "Создайте нейросеть");
                        }

                    }
                    if (MessText[0] == "/stop")
                    {
                        auto = false;
                        botClient.SendTextMessageAsync(update.Message.Chat.Id, "auto stop");
                    }
                    if (MessText[0] == "/txt")
                    {
                        try
                        {
                            Atxt(botClient, update, file.SaveList()[Convert.ToInt32(MessText[1])]);

                        }
                        catch (Exception ex)
                        {
                            botClient.SendTextMessageAsync(update.Message.Chat.Id, ex.Message);
                        }
                    }
                    
                }
                return;
            }

        }
        async void A(ITelegramBotClient botClient, Update update, int n, decimal k)
        {
            try
            {
                await Task.Run(() =>
                {
                    string mess = LearnNeuron(n, k);
                    botClient.SendTextMessageAsync(update.Message.Chat.Id, mess);
                });
            }
            catch (Exception ex)
            {
                botClient.SendTextMessageAsync(update.Message.Chat.Id, ex.Message);
            }
        }
        async void Tbmp(ITelegramBotClient botClient, Update update)
        {
            string path = Path() + "/1.bmp";
            try
            {
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    await botClient.SendPhotoAsync(
                        chatId: update.Message.Chat.Id,
                        photo: new InputOnlineFile(fileStream)
                    );
                }
            }
            catch (Exception ex)
            {
                botClient.SendTextMessageAsync(update.Message.Chat.Id, ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
        async void Atxt(ITelegramBotClient botClient, Update update, string p)
        {
            string path = Path() + "/1.bmp";
            try
            {
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    await botClient.SendDocumentAsync(
                        chatId: update.Message.Chat.Id,
                        document: new InputOnlineFile(fileStream)
                    );
                }
            }
            catch (Exception ex)
            {
                botClient.SendTextMessageAsync(update.Message.Chat.Id, ex.Message);
            }
        }
        string Path()
        {
            string path = Directory.GetCurrentDirectory();

            return path;
        }

        int[] statusRead(string[] text)
        {
            int[] res = new int[text.Length - 1];
            for (int i = 1; i < text.Length; i++)
            {
                res[i - 1] = Convert.ToInt32(text[i]);
            }
            return res;
        }
        string statusToString(int[] st)
        {
            string res = "Структура\n";
            for (int i = 0; i < st.Length; i++)
            {
                res += st[i] + " ";
            }
            return res;
        }
        string LearnNeuron(int count, decimal step)
        {
            string res = "";
            NeuronMeneger n = (NeuronMeneger)neuron.Clone();
            res += ($" Обучаем \n");
            n.LearningForFile(count, step);
            neuron = n;
            res += (neuron.TestForFile());
            return res;
        }

        async void AutoA(ITelegramBotClient botClient, Update update)
        {
            auto = true;
            await Task.Run(() =>
            {
                while (auto)
                {
                    string res = "";
                    NeuronMeneger n = (NeuronMeneger)neuron.Clone();
                    res += ($" Обучаем \n");
                    n.LearningForFile(10000, step);
                    neuron = n;
                    res += (neuron.TestForFile());
                    botClient.SendTextMessageAsync(update.Message.Chat.Id, res);
                }
            });
            botClient.SendTextMessageAsync(update.Message.Chat.Id, "Авто обучение завершено");
        }

    }
}
