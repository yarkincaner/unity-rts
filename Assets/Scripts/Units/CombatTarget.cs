using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTarget : MonoBehaviourPunCallbacks
{
    private bool isEnemey;

    void Start()
    {
        if (this.gameObject.tag == "Tree")
        {
            isEnemey = true;
        } else
        {
            isEnemey = !photonView.IsMine;
        }
    }

    public bool getIsEnemy() { return isEnemey; }
}
