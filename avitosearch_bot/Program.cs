using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using avitosearch_bot.core;
using avitosearch_bot.core.avito;

namespace avitosearch_bot
{
    class Program
    {
        private static string token { get; set; } = "2032398066:AAF-vt0Q8SLbYoU_LOgjld-q2sIjULI3vyY";
        private static TelegramBotClient client;
        public static ParserWorker<string[]> parser;
        public static AvitoSettings set = null;
        public static string[] data;
        
        static void Main(string[] args)
        {
            client = new TelegramBotClient(token);
            client.StartReceiving();
            client.OnMessage += onMessageHandler;
            Console.ReadLine();
            client.StopReceiving();

        }

        private static async void onMessageHandler(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Text != null)
            {
                
                string message_last = "";
                switch (message.Text)
                {
                    case "Создать фильтры поиска":

                        await client.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "пришлите ссылку на поиск");
                        message_last = "Создать фильтры поиска";
                        break;

                    case "Показать обьявления":
                        if (set == null)
                        {
                            await client.SendTextMessageAsync(
                             chatId: message.Chat.Id,
                             text: "Сначала добавьте ссылку",
                             replyToMessageId: message.MessageId,
                             replyMarkup: GetButtons());
                        }
                        else
                        {
                            parser = new ParserWorker<string[]>(
                            new AvitoParser());
                            parser.OnCompleted += parser_OnCompleted;
                            parser.OnNewData += parser_OnNewData;
                            parser.Settings = set;
                            parser.Start();

                            for (int i = 0; i < 5; i++)
                            {
                                await client.SendTextMessageAsync(
                                chatId: message.Chat.Id,
                                text: data[i],
                                replyMarkup: GetButtons());

                            }

                        }
                        
                        break;
                    default:
                        if (message.Text.Contains("https://www.avito.ru/"))
                        {
                            set = new AvitoSettings(1, 1, message.Text.Replace("https://www.avito.ru/",""));
                            await client.SendTextMessageAsync(
                             chatId: message.Chat.Id,
                             text: "Выполнено!",
                             replyToMessageId: message.MessageId,
                             replyMarkup: GetButtons());
                            
                        }
                        else
                        {
                            await client.SendTextMessageAsync(
                                                         chatId: message.Chat.Id,
                                                         text: "команда неверная!",
                                                         replyToMessageId: message.MessageId,
                                                         replyMarkup: GetButtons());
                        }
                        break;
                        
                        
                }

            }
        }

        private static async void parser_OnNewData(object arg1, string[] arg2)
        {
            data = arg2;
        }

        private static void parser_OnCompleted(object obj)
        {
            Console.WriteLine("All works done!");
        }

        private static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> { new KeyboardButton { Text = "Создать фильтры поиска"},
                    new KeyboardButton { Text = "Показать обьявления"} }

                }
            };
        }

    }
}        
    

