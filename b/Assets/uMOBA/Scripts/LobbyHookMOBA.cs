// This component transfers data from the lobby player to the gameobject player.
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;
using System;
using System.Linq;

public class LobbyHookMOBA : LobbyHook {
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer) {
        // find all players (ignore the one that we are creating right now)
        var players = FindObjectsOfType<Player>().Where(p => p != gamePlayer.GetComponent<Player>()).ToList();

        // even amount: then first team
        // uneven amount: then second team
        var player = gamePlayer.GetComponent<Player>();
        player.team = (players.Count % 2 == 0 ? Team.Good : Team.Evil);

        // set name to the name that was entered in the lobby screen
        player.name = lobbyPlayer.GetComponent<LobbyPlayer>().playerName;

        // start position
        var start = FindObjectsOfType<PlayerSpawn>().Where(g => g.team == player.team).First();
        player.agent.Warp(start.transform.position); // recommended over transform.position

        print("spawned " + player.name + " (" + player.team + ") at " + start.name);
    }
}
