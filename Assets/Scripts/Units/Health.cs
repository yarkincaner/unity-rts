using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviourPunCallbacks
{
    [SerializeField] float health = 100f;

    [PunRPC]
    public void TakeDamage(float damage)
    {
        health = Mathf.Max(health - damage, 0);
        Debug.Log(this.name + " health: " + health);
        if (health == 0)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
