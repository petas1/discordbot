using Discord;
using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.NetworkInformation;


namespace PeTaS_Bot_App
{
    class MyBot
    {
        DiscordClient discord;

        public MyBot()
        {
            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '$';
            });

            var commands = discord.GetService<CommandService>();
            
            commands.CreateCommand("ping")
                .Do(async (e) =>
                {

                    string pingValue;
                    using (Ping p = new Ping())
                    {
                        pingValue = p.Send("eu-central290.discord.gg").RoundtripTime.ToString();
                        Console.WriteLine($"Ping: {pingValue}ms");
                    }

                    if (pingValue >= 1000)
					{
						await e.Channel.SendMessage("Server is down...");
					}
					
					if (pingValue >= 500 || pingVAue <= 1000)
					{
						await e.Channel.SendMessage("Poonngg! We are having sme network problems :-(");
					}
					
					if (pingValue <= 500)
					{
						await e.Channel.SendMessage($"Pong! Current ping is: {pingValue}ms")
					}
                });

            commands.CreateCommand("ban")
                .Parameter("User")
                .Do(async (e) =>
                {
                    if (e.User.ServerPermissions.BanMembers)
                    {
                        User user = null;
                        try
                        {
                            user = e.Server.FindUsers(e.GetArg("User")).First();
                        }

                        catch (InvalidOperationException)
                        {
                            await e.Channel.SendMessage($"Couldn't ban user {e.GetArg("User")} (not found).");
                            return;
                        }
                        if (user.HasRole(e.Server.FindRoles("Owner").FirstOrDefault()))
                        {
                            await e.Channel.SendMessage("Permission denied.");
                        }
                        else
                        {
                            await e.Server.Ban(user);
                            await e.Channel.SendMessage($"{user.Name} was banned from the server!");
                        }
                    }
                    else await e.Channel.SendMessage("Permission denied.");
                });

            commands.CreateCommand("mute")
                .Alias("shut up")
                .Parameter("User")
                .Do(async (e) =>
                {
                    if (e.User.ServerPermissions.BanMembers)
                    {
                        User user = null;
                        try
                        {
                            user = e.Server.FindUsers(e.GetArg("User")).First();
                        }

                        catch (InvalidOperationException)
                        {
                            await e.Channel.SendMessage($"Couldn't mute user {e.GetArg("User")} (not found).");
                            return;
                        }
                        if (user.HasRole(e.Server.FindRoles("Owner").FirstOrDefault()))
                        {
                            await e.Channel.SendMessage("Permission denied.");
                        }
                        else
                        {
                            var role = e.Server.Roles.FirstOrDefault(x => x.Name.ToString() == "Muted");
                            await user.AddRoles(role);
                            await e.Channel.SendMessage($"{user.Name} was muted on the server!");
                        }
                    }
                    else await e.Channel.SendMessage("Permission denied.");
                });

            commands.CreateCommand("unmute")
                .Parameter("User")
                .Do(async (e) =>
                {
                    if (e.User.ServerPermissions.BanMembers)
                    {
                        User user = null;
                        try
                        {
                            user = e.Server.FindUsers(e.GetArg("User")).First();
                        }

                        catch (InvalidOperationException)
                        {
                            await e.Channel.SendMessage($"Couldn't unmute user {e.GetArg("User")} (not found).");
                            return;
                        }
                        if (user.HasRole(e.Server.FindRoles("Owner").FirstOrDefault()))
                        {
                            await e.Channel.SendMessage("Permission denied.");
                        }
                        else
                        {
                            var role = e.Server.Roles.FirstOrDefault(x => x.Name.ToString() == "Muted");
                            await user.RemoveRoles(role);
                            await e.Channel.SendMessage($"{user.Name} was unmuted on the server!");
                        }
                    }
                    else await e.Channel.SendMessage("Permission denied.");
                });

            commands.CreateCommand("kick")
            .Parameter("User")
            .Do(async (e) =>
            {
                if (e.User.ServerPermissions.KickMembers)
                {
                    User user = null;
                    try
                    {
                        user = e.Server.FindUsers(e.GetArg("User")).First();
                    }
                    catch (InvalidOperationException)
                    {
                        await e.Channel.SendMessage($"Couldn't ban user {e.GetArg("User")} (not found).");
                        return;
                    }
                    if (user.HasRole(e.Server.FindRoles("Owner").FirstOrDefault()))
                    {
                        await e.Channel.SendMessage("Permission denied.");
                    }
                    else
                    {
                        await user.Kick();
                        await e.Channel.SendMessage($"{user.Name} was kicked from the server!");
                    }
                }
                else
                {
                    await e.Channel.SendMessage("Permission denied.");
                }
            });
			
			comande.CreateCommand("shutdown")
				.Alias("turnoff")
				.Prameter ("now", pameterType: variable)
				.Do(async (e) =>
				{
					if (User.HasRole(e.Server.FindRoles("Owner").FirstOrDefault())
					{
						Application.Close();
					}
					else
					{
						await e.Channel.Sendmessge("You can't shut down the bot, beacuse yout aren't owner.")
					}
					if (getArg("now") != null)
					{
						Enviroment.ShutDown();
					}
				});
			

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzY4ODc3NDU5NzUzNDAyMzY5.DMfeTA.ltltAyuPqmqHel6RADVqUjBPEW4", TokenType.Bot);
                discord.SetGame("I like pineapples!");
            });

        }
        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
