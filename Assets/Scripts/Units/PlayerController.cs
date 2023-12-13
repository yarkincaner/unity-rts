using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks
{
    void Update()
    {
        if (InteractWithCombat() == true)
        {
            return;
        }
        if (InteractWithMovement() == true)
        {
            return;
        }
    }

    private bool InteractWithCombat()
    {
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        foreach (RaycastHit hit in hits)
        {
            CombatTarget target = hit.transform.GetComponent<CombatTarget>();
            if (target == null)
            {
                continue;
            }

            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<Fighter>().Attack(target);
            }
            return true;
        }
        return false;
    }

    private bool InteractWithMovement()
    {
        RaycastHit hit;
        bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
        if (hasHit)
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                if (photonView.IsMine)
                {
                    GetComponent<Mover>().gameObject.GetComponent<PhotonView>().RPC("StartMoveAction", RpcTarget.All, hit.point);
                    //GetComponent<Mover>().StartMoveAction(hit.point);
                }
            }
            
            return true;
        }

        return false;
    }

    private static Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}