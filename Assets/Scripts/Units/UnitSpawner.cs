using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] GameObject soldierPrefab;
    [SerializeField] GameObject villagerPrefab;

    public void SpawnSoldier()
    {
        if (hasEnoughResources(soldierPrefab))
        {
            PhotonNetwork.Instantiate(soldierPrefab.name, new Vector3(43, 0, 50), Quaternion.identity, 0, null);
        }
    }

    public void SpawnVillager()
    {
        if (hasEnoughResources(villagerPrefab))
        {
            PhotonNetwork.Instantiate(villagerPrefab.name, new Vector3(43, 0, 50), Quaternion.identity, 0, null);
        }
    }

    private bool hasEnoughResources(GameObject prefab)
    {
        int neededAmountOfWoods = prefab.GetComponent<Unit>().getNumOfWoods();
        int neededAmountOfStones = prefab.GetComponent<Unit>().getNumOfStones();

        if (player.GetComponent<MyResources>().getWoods() >= neededAmountOfWoods && player.GetComponent<MyResources>().getStones() >= neededAmountOfStones)
        {
            player.GetComponent<MyResources>().setWoods(-neededAmountOfWoods);
            player.GetComponent<MyResources>().setStone(-neededAmountOfStones);
            return true;
        }

        Debug.Log("You do not meet required resources for " +  prefab.name);
        return false;
    }
}
