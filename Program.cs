using System;
using Telegram.Bot;
using MihaZupan;
using Newtonsoft.Json;
using System.IO;
using Telegram.Bot.Args;
using System.Threading;
using System.Net;

namespace BeerBot
{
    class Program
    {
        static ITelegramBotClient botClient;

        static void Main(string[] args)
        {
            DirtyLittleSecretsMap items;
            string projectFolder = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())));


            using (StreamReader r = new StreamReader(Path.Combine(projectFolder, "DirtyLittleSecrets.json")))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<DirtyLittleSecretsMap>(json);
            }

            HttpToSocks5Proxy proxy = new HttpToSocks5Proxy(items.Proxy2.Address, items.Proxy2.Port);
            //HttpToSocks5Proxy proxy2 = new HttpToSocks5Proxy(items.Proxy.Address, items.Proxy.Port, items.Proxy.Login, items.Proxy.Password);

            botClient = new TelegramBotClient(items.BotToken, proxy);

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
                  $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
                );


            botClient.OnUpdate += Bot_OnUpdate;
            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            //while (true)
            //{
            //    var message = Console.ReadLine();

            //}
            Thread.Sleep(int.MaxValue);
        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");
            if (e.Message.Text == "/beer")
            {
                await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text: "Го хоть по пивку как-нибудь двинем. Посидим."
                );
            }
        }

        static async void Bot_OnUpdate(object sender, UpdateEventArgs e)
        {
            Console.WriteLine($"Received an UPDATE.");
        }
    }
}
