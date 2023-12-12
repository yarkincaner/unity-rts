using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] GameObject soldierPrefab;
    public void SpawnSoldier()
    {
        PhotonNetwork.Instantiate(soldierPrefab.name, new Vector3(43, 0, 50), Quaternion.identity, 0, null);
    }
}
