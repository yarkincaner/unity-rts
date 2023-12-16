using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject spawnedPlayerPrefab;
    [SerializeField] GameObject uiPrefab;
    [SerializeField] GameObject resources;
    public Vector3 spawnPosition;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        spawnedPlayerPrefab = PhotonNetwork.Instantiate(spawnedPlayerPrefab.name, spawnPosition, Quaternion.identity, 0, null);
        spawnedPlayerPrefab.name = PhotonNetwork.NickName;
        uiPrefab = PhotonNetwork.Instantiate(uiPrefab.name, spawnPosition, Quaternion.identity, 0, null);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        resources = PhotonNetwork.Instantiate(resources.name, Vector3.zero, Quaternion.identity, 0, null);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
        PhotonNetwork.Destroy(uiPrefab);
    }
}
