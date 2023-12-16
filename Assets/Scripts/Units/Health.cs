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
        if (health == 0 && photonView.IsMine)
        {
            if (gameObject.tag == "Tree")
            {
                 int numOfWoods = GetComponent<MyTree>().getNumOfWoods();
                    GameObject player = GameObject.Find(PhotonNetwork.NickName);
                    //player.GetComponent<MyResources>().setWoods(numOfWoods);
                    player.GetComponent<PhotonView>().RPC("setWoods", RpcTarget.All, numOfWoods);
            }
            PhotonNetwork.Destroy(PhotonView.Find(this.photonView.ViewID));
        }
    }
}
