using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class ServerManagement : MonoBehaviourPunCallbacks
{
    void Start()
    {
        ConnectToServer();
    }

    void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server");
        base.OnConnectedToMaster();
        PhotonNetwork.JoinOrCreateRoom("RTSRoom", new RoomOptions
        {
            MaxPlayers = 2,
            IsOpen = true,
            IsVisible = true
        }, TypedLobby.Default);
    }

    //public override void OnJoinedLobby()
    //{
    //    Debug.Log("Connected to the lobby");
    //    PhotonNetwork.JoinOrCreateRoom("TableTennisRoom", new RoomOptions
    //    {
    //        MaxPlayers = 2,
    //        IsOpen = true,
    //        IsVisible = true
    //    }, TypedLobby.Default);
    //}

    public override void OnJoinedRoom()
    {
        Debug.Log("The local player: " + PhotonNetwork.NickName + "joined to " + PhotonNetwork.CurrentRoom.Name +
          " Player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
        base.OnJoinedRoom();
        //PhotonNetwork.Instantiate(genericVRPlayerPrefab.name, spawnPosition, Quaternion.identity);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("JoinRoom failed: " + message);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player joined the room");
        base.OnPlayerEnteredRoom(newPlayer);
    }


}
