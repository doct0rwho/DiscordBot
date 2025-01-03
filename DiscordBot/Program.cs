using System;
using DiscordBot;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DotNetEnv;

class Program
{
    static async Task Main(string[] args)
    {

        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Env.TraversePath().Load();
        var token = Env.GetString("TOKEN");
        if (string.IsNullOrWhiteSpace(token))
        {
            Console.WriteLine("Ошибка: Токен не найден или пустой в .env файле.");
            return;
        }
        // Создаём Discord-клиент
        

        var discord = new DiscordClient(new DiscordConfiguration
        {
            Intents = DiscordIntents.All,
            Token = Env.GetString("TOKEN"),
            TokenType = TokenType.Bot,
            AutoReconnect = true
        });

        discord.MessageCreated += async (sender, e) =>
        {
            // Логирование получения сообщения
            Console.WriteLine($"Получено сообщение: {e.Message.Content}");

            // Проверка, что сообщение не от бота
            if (e.Author.IsBot)
                return;

            // Логирование канала, откуда пришло сообщение
            Console.WriteLine($"Сообщение пришло из канала: {e.Channel.Name}");

            // Если сообщение начинается с "/", проверим, что оно относится к команде
            if (e.Message.Content.StartsWith("/"))
            {
                // Логирование длины команды
                Console.WriteLine($"Длина команды: {e.Message.Content.Length}");

                // Проверка, что команда не пустая
                if (e.Message.Content.Trim().Length > 1)
                {
                    Console.WriteLine("Команда распознана: " + e.Message.Content);

                    // Здесь может идти обработка команды
                    // ...
                }
                else
                {
                    Console.WriteLine("Команда пустая или состоит только из пробелов.");
                }
            }
            else
            {
                Console.WriteLine("Это не команда");
            }
        };



        var commands = discord.UseCommandsNext(new CommandsNextConfiguration
        {
            StringPrefixes = new[] { "/" }
        });

        commands.RegisterCommands<MusicCommands>();

        // Логирование событий (например, сообщения об ошибках)
        discord.Ready += async (sender, e) =>
        {
            Console.WriteLine("Бот запущен и готов к работе!");
        };

        // Запускаем бота
        await discord.ConnectAsync();
        Console.WriteLine("Нажми Enter, чтобы остановить бота.");
        Console.ReadLine();
    }
}
