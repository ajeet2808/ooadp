using System;
using System.IO;
using System.Threading;

namespace NewsFeed
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pres q to exit!");
            var newsFeedManager = new NewsFeedManager();
            var commands = File.ReadAllLines("./commands.txt");
            foreach (var command in commands)
            {
                if (command == "q" || command == "Q") break;
                Thread.Sleep(1000);
                Console.WriteLine(command);
                var commandSegs = command.Split("~");
                try
                {
                    switch (commandSegs[0].ToLower())
                    {
                        case "signup":
                            if (commandSegs.Length != 2 || string.IsNullOrEmpty(commandSegs[1]))
                            {
                                Console.WriteLine("Plese enter valid command.");
                            }
                            else
                            {
                                newsFeedManager.CreateUser(commandSegs[1]);
                                Console.WriteLine($"signed up {commandSegs[1]}");
                            }
                            break;
                        case "login":
                            if (commandSegs.Length != 2 || string.IsNullOrEmpty(commandSegs[1]))
                            {
                                Console.WriteLine("Plese enter valid command.");
                            }
                            else
                            {
                                newsFeedManager.SetCurrentUser(commandSegs[1]);
                                Console.WriteLine($"logged in {commandSegs[1]}");
                                newsFeedManager.ShowNewsFeeds();
                            }
                            break;
                        case "post":
                            if (commandSegs.Length != 2 || string.IsNullOrEmpty(commandSegs[1]))
                            {
                                Console.WriteLine("Plese enter valid command.");
                            }
                            else
                            {
                                newsFeedManager.Post(commandSegs[1]);
                                Console.WriteLine($"posted {commandSegs[1]}");
                            }
                            break;
                        case "follow":
                            if (commandSegs.Length != 2 || string.IsNullOrEmpty(commandSegs[1]))
                            {
                                Console.WriteLine("Plese enter valid command.");
                            }
                            else
                            {
                                newsFeedManager.Post(commandSegs[1]);
                                Console.WriteLine($"posted {commandSegs[1]}");
                            }
                            break;
                        case "upvote":
                            if (commandSegs.Length != 2 || string.IsNullOrEmpty(commandSegs[1]))
                            {
                                Console.WriteLine("Plese enter valid command.");
                            }
                            else
                            {
                                newsFeedManager.UpVotePost(commandSegs[1]);
                                Console.WriteLine($"upvoted {commandSegs[1]}");
                            }
                            break;

                        case "downvote":
                            if (commandSegs.Length != 2 || string.IsNullOrEmpty(commandSegs[1]))
                            {
                                Console.WriteLine("Plese enter valid command.");
                            }
                            else
                            {
                                newsFeedManager.DownVotePost(commandSegs[1]);
                                Console.WriteLine($"donwvoted {commandSegs[1]}");
                            }
                            break;

                        case "reply":
                            if (commandSegs.Length != 3 || string.IsNullOrEmpty(commandSegs[1]) || string.IsNullOrEmpty(commandSegs[2]))
                            {
                                Console.WriteLine("Plese enter valid command.");
                            }
                            else
                            {
                                newsFeedManager.CommentOnPost(commandSegs[1], commandSegs[2]);
                                Console.WriteLine($"replied to {commandSegs[1]} by {commandSegs[2]}");
                            }
                            break;
                        case "shownewsfeed":
                            if (commandSegs.Length != 1)
                            {
                                Console.WriteLine("Plese enter valid command.");
                            }
                            else
                            {
                                newsFeedManager.ShowNewsFeeds();
                            }
                            break;

                        default:
                            Console.WriteLine("Please enter a valid command!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine("Press any key!");
            Console.ReadLine();
        }
    }
}
