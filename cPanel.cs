using Discord;
using System.Net;
using System.Net.NetworkInformation;



onButtonKickKlick(object sender, eventArg e)
{
	var user;
	
	user = textBoxToBan.Text;
	
	user = e.Server.FindUsers(e.GetArg("User")).First();
	
	user.Kick();
	e.Channel.SendMessage($"{user.Name} has been kicked from the server!");
	
}

onButtonBanClick()
{
	var user;
	
	var User = textBoxToKick.Text;
	
	user = e.Server.FindUsers(e.GetArg("User")).First();
	
	e.Server.Ban(user);
	e.Channel.SendMessage($"{user.Name} was banned from the server!");
}

onButtinMuteClick()
{
	var user;
	
	var User = textBoxToMute.Text;
	
	user = e.Server.FindUsers(e.GetArg("User")).First();
	
	var role = e.Server.Roles.FirstOrDefault(x => x.Name.ToString() == "Muted");

	user.AddRoles(role);

    e.Channel.SendMessage($"{user.Name} was muted on the server!");
}

onButtonUnMuteClick()
{
	var user;
	
	var User = textBoxToUnMute.Text;
	
	user = e.Server.FindUsers(e.GetArg("User")).First();
	
	var role = e.Server.Roles.FirstOrDefault(x => x.Name.ToString() == "Muted");

	user.RemoveRoles(role);

    e.Channel.SendMessage($"{user.Name} was muted on the server!");
}


onButtonPingCLick()
{
	string pingValue;

    using (Ping p = new Ping())

		{

   			pingValue = p.Send("eu-central290.discord.gg").RoundtripTime.ToString();

   			Console.WriteLine($"Ping: {pingValue}ms");

    	}
	textBoxPing.Text = pingValue;
			
}
