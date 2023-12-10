using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class ServerManagement : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        //1. connect to server
        //2. connect to lobby
        //3. connect to room 
        PhotonNetwork.ConnectUsingSettings(); // connect to server 
        /*
        PhotonNetwork.JoinRoom("room name"); // connect to room 
        PhotonNetwork.CreateRoom("room name", room_settings); // create room
        PhotonNetwork.JoinOrCreateRoom("room name", room_settings, TypedLobby.Default);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
         */
    }



    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to the server");
        PhotonNetwork.JoinLobby(); // connect to lobby
        //It checks/controls the connection of the server

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to the lobby");
        PhotonNetwork.JoinOrCreateRoom("AkdenizCSRoom", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        //connect to random room or create 
        //it checks the connection of the Lobby
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Connected to the Room");
        GameObject gameObject = PhotonNetwork.Instantiate("Camera Rig", Vector3.zero, Quaternion.identity, 0, null);
        PhotonView pv = gameObject.GetComponent<PhotonView>();
        pv.Owner.NickName = "Player" + pv.ViewID;
        gameObject.name = pv.Owner.NickName;
        GameObject otherCamera = GameObject.Find("Camera Rig(Clone)");
        if (otherCamera != null )
        {
            otherCamera.SetActive(false);
        }
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left the Room");
    }

    public override void OnLeftLobby()
    {
        Debug.Log("Left the Lobby");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Could not join any room");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Could not join any random room");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Could not create room");
    }


}
