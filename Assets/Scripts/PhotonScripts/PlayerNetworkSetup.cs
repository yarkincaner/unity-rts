using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject instantiatedObject;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            //the player is local
            instantiatedObject.SetActive(true);
        }
        else
        {
            //the player is remote
            instantiatedObject.SetActive(false);
        }
    }
}
