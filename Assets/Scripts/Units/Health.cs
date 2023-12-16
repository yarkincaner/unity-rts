using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviourPunCallbacks
{
    [SerializeField] float health = 100f;

    [PunRPC]
    public void TakeDamage(float damage, string attackerName)
    {
        health = Mathf.Max(health - damage, 0);
        Debug.Log(this.name + " health: " + health);
        if (health == 0)
        {
            if (gameObject.tag == "Tree" && attackerName.Equals(PhotonNetwork.NickName))
            {
                 int numOfWoods = GetComponent<MyTree>().getNumOfWoods();
                //player.GetComponent<MyResources>().setWoods(numOfWoods);
                GameObject.FindWithTag("Player").GetComponent<MyResources>().setWoods(numOfWoods);
            }
            PhotonNetwork.Destroy(PhotonView.Find(this.photonView.ViewID));
        }
    }
}
