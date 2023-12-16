using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviourPunCallbacks, IAction
{
    private bool isSelected;

    private void Start()
    {
        isSelected = false;
    }

    void Update()
    {
        UpdateAnimator();

        if (Input.GetKey(KeyCode.Escape))
        {
            isSelected = false;
        }
    }

    [PunRPC]
    public void StartMoveAction(Vector3 hit)
    {
        if (isSelected)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            GetComponent<Fighter>().Cancel();
            GetComponent<NavMeshAgent>().destination = hit;
            GetComponent<NavMeshAgent>().isStopped = false;
        }
    }

    [PunRPC]
    public void MoveTo(Vector3 hit)
    {
        if (isSelected)
        {
            GetComponent<NavMeshAgent>().destination = hit;
            GetComponent<NavMeshAgent>().isStopped = false;
        }
    }

    [PunRPC]
    public void Cancel()
    {
        GetComponent<NavMeshAgent>().isStopped = true;
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
    
    private void OnMouseDown()
    {
        isSelected = true;
    }
}
