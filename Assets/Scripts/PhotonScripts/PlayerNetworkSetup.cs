using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject localCamera;
    [SerializeField] private GameObject localUI;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            //the player is local
            localCamera.SetActive(true);
            localCamera.transform.parent.tag = "Player";
            localUI.SetActive(true);
        }
        else
        {
            //the player is remote
            localCamera.SetActive(false);
            localCamera.transform.parent.tag = "Untagged";
            localUI.SetActive(false);
        }
    }
}
