using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyResources : MonoBehaviourPunCallbacks
{
    private int woodsAmount;
    private int stonesAmount;
    [SerializeField] private TextMeshProUGUI woodsText;
    [SerializeField] private TextMeshProUGUI stonesText;

    void Start()
    {
        if (photonView.IsMine)
        {
            woodsAmount = 10;
            stonesAmount = 10;

            UpdateTexts();
        }
    }

    public int getWoods() { return woodsAmount; }

    [PunRPC]
    public void setWoods(int amount)
    {
        if (photonView.IsMine)
        {
            woodsAmount += amount;
            UpdateTexts();
        }
    }

    public int getStones() { return stonesAmount; }

    [PunRPC]
    public void setStone(int amount)
    {
        if (photonView.IsMine)
        {
            woodsAmount += amount;
            UpdateTexts();
        }
    }

    void UpdateTexts()
    {
        woodsText.text = woodsAmount.ToString();
        stonesText.text = stonesAmount.ToString();
    }
}
