using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject localCameraRig;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            //the player is local
            localCameraRig.SetActive(true);
        }
        else
        {
            //the player is remote 
            //If it is not local player, we will disable the XR origin gameobject under the generic VR player.
            localCameraRig.SetActive(false);
        }
    }
}
