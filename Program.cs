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

            botClient = new TelegramBotClient(items.BotToken);

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
                  $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
                );


            botClient.OnUpdate += Bot_OnUpdate;
            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}. Text: {e.Message.Text}");
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
