// We implemented a chat system that works directly with UNET. The chat supports
// different channels that can be used to communicate with other players:
// 
// - **Team Chat:** by default, all messages that don't start with a **/** are
// addressed to the team.
// - **Whisper Chat:** a player can write a private message to another player by
// using the **/ name message** format.
// - **All Chat:** we implemented all chat support with the **/all message**
// command.
// - **Info Chat:** the info chat can be used by the server to notify all
// players about important news. The clients won't be able to write any info
// messages.
// 
// _Note: the channel names, colors and commands can be edited in the Inspector
// by selecting the Player prefab and taking a look at the Chat component._
// 
// A player can also click on a chat message in order to reply to it.
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ChannelInfo {
    public string command; // /w etc.
    public string identifierOut; // for sending
    public string identifierIn; // for receiving
    public Color color;

    public ChannelInfo(string command, string identifierOut, string identifierIn, Color color) {
        this.command = command;
        this.identifierOut = identifierOut;
        this.identifierIn = identifierIn;
        this.color = color;
    }
}

[System.Serializable]
public class MessageInfo {
    public string content; // the actual message
    public string replyPrefix; // copied to input when clicking the message
    public Color color;

    public MessageInfo(string sender, string identifier, string message, string replyPrefix, Color color) {
        // construct the message (we don't really need to save all the parts,
        // also this will save future computations)
        content = "<b>" + sender + identifier + ":</b> " + message;
        this.replyPrefix = replyPrefix;
        this.color = color;
    }
}

[NetworkSettings(channel=Channels.DefaultUnreliable)]
public class Chat : NetworkBehaviour {
    // channels
    [Header("Channels")]
    [SerializeField] ChannelInfo whisper = new ChannelInfo("/w", "(TO)", "(FROM)", Color.magenta);
    [SerializeField] ChannelInfo team = new ChannelInfo("", "(Team)", "(Team)", Color.cyan);
    [SerializeField] ChannelInfo all = new ChannelInfo("/all", "(All)", "(All)", Color.white);
    [SerializeField] ChannelInfo info = new ChannelInfo("", "(Info)", "(Info)", Color.red);

    [Header("Other")]
    public int maxLength = 70;

    public override void OnStartLocalPlayer() {
        // test messages
        AddMessage(new MessageInfo("", info.identifierIn, "Type here for team chat!", "", info.color));
        AddMessage(new MessageInfo("", info.identifierIn, "  Use /all for all chat", "",  info.color));
        AddMessage(new MessageInfo("", info.identifierIn, "  Use /w NAME to whisper", "",  info.color));
        AddMessage(new MessageInfo("Someone", all.identifierIn, "Hello Everyone!", "",  all.color));
    }

    // submit tries to send the string and then returns the new input text
    [Client]
    public string OnSubmit(string s) {
        // not empty and not only spaces?
        if (!Utils.IsNullOrWhiteSpace(s)) {
            // command in the commands list?
            // note: we don't do 'break' so that one message could potentially
            //       be sent to multiple channels (see mmorpg local chat)
            string lastcommand = "";
            if (s.StartsWith(whisper.command)) {
                // whisper
                var parsed = ParsePM(whisper.command, s);
                string user = parsed[0];
                string msg = parsed[1];
                if (!Utils.IsNullOrWhiteSpace(user) && !Utils.IsNullOrWhiteSpace(msg)) {
                    if (user != name) {
                        lastcommand = whisper.command + " " + user + " ";
                        CmdMsgWhisper(user, msg);
                    } else print("cant whisper to self");
                } else print("invalid whisper format: " + user + "/" + msg);
            } else if (!s.StartsWith("/")) {
                // local chat is special: it has no command
                lastcommand = "";
                CmdMsgTeam(s);
            } else if (s.StartsWith(all.command)) {
                // all
                string msg = ParseGeneral(all.command, s);
                lastcommand = all.command + " ";
                CmdMsgAll(msg);
            }

            // input text should be set to lastcommand
            return lastcommand;
        }

        // input text should be cleared
        return "";
    }

    [Client]
    void AddMessage(MessageInfo mi) {
        FindObjectOfType<UIChat>().AddMessage(mi);
    }

    // parse a message of form "/command message"
    static string ParseGeneral(string command, string msg) {
        if (msg.StartsWith(command + " "))
            // remove the "/command " prefix
            return msg.Substring(command.Length + 1); // command + space
        return "";
    }

    static string[] ParsePM(string command, string pm) {
        // parse to /w content
        string content = ParseGeneral(command, pm);

        // now split the content in "user msg"
        if (content != "") {
            // find the first space that separates the name and the message
            int i = content.IndexOf(" ");
            if (i >= 0) {
                string user = content.Substring(0, i);
                string msg = content.Substring(i+1);
                return new string[] {user, msg};
            }
        }
        return new string[] {"", ""};
    }

    // networking //////////////////////////////////////////////////////////////
    [Command(channel=Channels.DefaultUnreliable)] // unimportant => unreliable
    void CmdMsgAll(string message) {
        if (message.Length > maxLength) return;

        print("sending all msg:" + message);

        // send to each player
        foreach (var entry in NetworkServer.objects) {
            var chat = entry.Value.GetComponent<Chat>();
            if (chat != null) {
                // send message
                print("sending all msg to:" + entry.Value.name);
                chat.TargetMsgAll(chat.connectionToClient, name, message);
            }
        }
    }

    [Command(channel=Channels.DefaultUnreliable)] // unimportant => unreliable
    void CmdMsgTeam(string message) {
        if (message.Length > maxLength) return;

        print("sending team msg:" + message);

        // send to each player in the same team
        foreach(var entry in NetworkServer.objects) {
            var chat = entry.Value.GetComponent<Chat>();
            if (chat != null &&
                entry.Value.GetComponent<Player>().team == GetComponent<Player>().team) {
                // send message
                print("sending team msg to:" + entry.Value.name);
                chat.TargetMsgTeam(chat.connectionToClient, name, message);
            }
        }
    }

    [Command(channel=Channels.DefaultUnreliable)] // unimportant => unreliable
    void CmdMsgWhisper(string playerName, string message) {
        if (message.Length > maxLength) return;

        // find the player with that name (note: linq version is too ugly)
        foreach(var entry in NetworkServer.objects) {
            var chat = entry.Value.GetComponent<Chat>();
            if (entry.Value.name == playerName && chat != null) {
                // receiver gets a 'from' message, sender gets a 'to' message
                chat.TargetMsgWhisperFrom(chat.connectionToClient, name, message);
                TargetMsgWhisperTo(connectionToClient, entry.Value.name, message);
                return;
            }
        }
    }

    // message handlers ////////////////////////////////////////////////////////
    [TargetRpc(channel=Channels.DefaultUnreliable)] // only send to one client
    public void TargetMsgWhisperFrom(NetworkConnection target, string sender, string message) {        
        // add message with identifierIn
        string identifier = whisper.identifierIn;
        string reply = whisper.command + " " + sender + " "; // whisper
        AddMessage(new MessageInfo(sender, identifier, message, reply, whisper.color));
    }

    [TargetRpc(channel=Channels.DefaultUnreliable)] // only send to one client
    public void TargetMsgWhisperTo(NetworkConnection target, string receiver, string message) {
        // add message with identifierOut
        string identifier = whisper.identifierOut;
        string reply = whisper.command + " " + receiver + " "; // whisper
        AddMessage(new MessageInfo(receiver, identifier, message, reply, whisper.color));
    }
    [TargetRpc(channel=Channels.DefaultUnreliable)] // only send to one client
    public void TargetMsgInfo(NetworkConnection target, string message) {
        AddMessage(new MessageInfo("", info.identifierIn, message, "", info.color));
    }

    [TargetRpc(channel=Channels.DefaultUnreliable)] // only send to one client
    public void TargetMsgAll(NetworkConnection target, string sender, string message) {
        AddMessage(new MessageInfo(sender, all.identifierIn, message, "", all.color));
    }

    [TargetRpc(channel=Channels.DefaultUnreliable)] // only send to one client
    public void TargetMsgTeam(NetworkConnection target, string sender, string message) {
        AddMessage(new MessageInfo(sender, team.identifierIn, message, "", team.color));
    }
}
