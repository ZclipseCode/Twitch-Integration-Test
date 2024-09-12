using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;

public class TwitchConnection : MonoBehaviour
{
    TcpClient twitch;
    StreamReader reader;
    StreamWriter writer;

    const string URL = "irc.chat.twitch.tv";
    const int PORT = 6667;

    [SerializeField] string user = "Liveztream";
    // get OAuth from https://twitchapps.com/tmi/
    string oAuth = Environment.GetEnvironmentVariable("TWITCH_OAUTH", EnvironmentVariableTarget.User);
    [SerializeField] string channel = "Liveztream";

    void ConnectToTwitch()
    {
        twitch = new TcpClient(URL, PORT);
        reader = new StreamReader(twitch.GetStream());
        writer = new StreamWriter(twitch.GetStream());

        writer.WriteLine("PASS " + oAuth);
        writer.WriteLine("NICK " + user.ToLower());
        writer.WriteLine("JOIN #" + channel.ToLower());
        writer.Flush();
    }

    private void Awake()
    {
        ConnectToTwitch();
    }

    private void Update()
    {
        if (twitch.Available > 0)
        {
            string message = reader.ReadLine();

            print(message);
        }
    }
}
