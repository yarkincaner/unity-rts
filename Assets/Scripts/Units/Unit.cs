using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    private bool isSelected;

    Transform target;
    PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                isSelected = false;
            }

            Move();
        }
    }

    public void Move()
    {
        if (isSelected && Input.GetMouseButton(0)) 
        { 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);

            if (hasHit)
            {
                GetComponent<NavMeshAgent>().destination = hit.point;
            }
        }
    }

    private void OnMouseDown()
    {
        isSelected = true;
    }
}
