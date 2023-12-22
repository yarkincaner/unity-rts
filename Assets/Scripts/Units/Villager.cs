using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviourPunCallbacks, IAction
{
    [SerializeField] float timeBetweenAttacks;
    [SerializeField] float weaponRange;
    [SerializeField] float weaponDamage;

    Transform targetObject;
    float timeSinceLastAttack;
    private void Start()
    {
        weaponRange = 5.0f;
        weaponDamage = 10f;
        timeBetweenAttacks = 1f;
    }
    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (targetObject == null)
        {
            return;
        }
        bool isInRange = Vector3.Distance(transform.position, targetObject.position) < weaponRange;
        if (isInRange == false)
        {
            GetComponent<Mover>().gameObject.GetComponent<PhotonView>().RPC("MoveTo", RpcTarget.All, targetObject.position);
            //GetComponent<Mover>().MoveTo(targetObject.position);
        }
        else
        {
            this.GetComponent<PhotonView>().RPC("AttackMethod", RpcTarget.All, null);
            //AttackMethod();
            GetComponent<Mover>().gameObject.GetComponent<PhotonView>().RPC("Cancel", RpcTarget.All, null);
            //GetComponent<Mover>().Cancel();
        }
    }
    void Hit()
    {
        if (photonView.IsMine)
        {
            Health health = targetObject.GetComponent<Health>();
            object[] parameters = new object[] { weaponDamage, this.gameObject.GetComponent<PhotonView>().Owner.NickName };
            health.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, parameters);
        }
    }

    [PunRPC]
    private void AttackMethod()
    {
        if (timeSinceLastAttack > timeBetweenAttacks)
        {
            //GetComponent<Animator>().gameObject.GetComponent<PhotonView>().RPC("SetTrigger", RpcTarget.All, "attack");
            GetComponent<Animator>().SetTrigger("attack");
            timeSinceLastAttack = 0;
        }
    }

    public void Gather(CombatTarget target)
    {
        GetComponent<ActionScheduler>().StartAction(this);
        if (target.tag == "Building")
        {
            weaponRange = 10.0f;
        }
        else
        {
            weaponRange = 5.0f;
        }
        targetObject = target.transform;
        //Debug.Log("Attack is done");
    }
    public void Cancel()
    {
        targetObject = null;
    }
}
