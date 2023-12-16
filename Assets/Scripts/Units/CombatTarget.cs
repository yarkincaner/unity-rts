using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTarget : MonoBehaviourPunCallbacks
{
    private bool isEnemey;

    void Start()
    {
        isEnemey = !photonView.IsMine;
    }

    public bool getIsEnemy() { return isEnemey; }
}
