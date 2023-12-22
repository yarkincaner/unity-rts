using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacer : MonoBehaviourPunCallbacks
{
    public static BuildingPlacer instance; // Singleton Pattern instance
    public LayerMask terrainLayerMask;

    private GameObject buildingPrefab;
    private GameObject toBuild;
    [SerializeField] private GameObject player;

    private Camera mainCamera;

    private Ray ray;
    private RaycastHit hit;

    private void Awake()
    {
        instance = this;

        //mainCamera = Camera.main;
        buildingPrefab = null;
    }

    private void Update()
    {
        // If in build mode
        if (buildingPrefab != null && toBuild != null)
        {
            // right-click: cancel build mode
            if (Input.GetMouseButtonDown(1)) {
                //Destroy(toBuild);
                PhotonNetwork.Destroy(toBuild);
                toBuild = null;
                buildingPrefab = null;
                return;
            }

            // hide preview when hovering UI
            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (toBuild.activeSelf) toBuild.SetActive(false);
            } else if (!toBuild.activeSelf)
            {
                toBuild.SetActive(true);
            }

            //ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            ray = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000f, terrainLayerMask))
            {
                if (!toBuild.activeSelf) toBuild.SetActive(true);
                toBuild.transform.position = hit.point;
                //this.gameObject.GetComponent<PhotonView>().RPC("TransformPos", RpcTarget.All, null);

                // left-click
                if (Input.GetMouseButtonDown(0))
                {
                    BuildingManager manager = toBuild.GetComponent<BuildingManager>();

                    if (manager.hasValidPlacement)
                    {
                        //manager.SetPlacementMode(PlacementMode.Fixed);
                        toBuild.GetComponent<BuildingManager>().gameObject.GetComponent<PhotonView>().RPC("SetPlacementMode", RpcTarget.All, PlacementMode.Fixed);

                        // shift-key: chain builds
                        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
                            toBuild = null; // to avoid destruction
                            PrepareBuilding();
                        } else
                        {
                            player.GetComponent<MyResources>().setStone(-buildingPrefab.GetComponent<BuildingManager>().getStones());
                            player.GetComponent<MyResources>().setWoods(-buildingPrefab.GetComponent<BuildingManager>().getWoods());
                            buildingPrefab = null;
                            toBuild = null;
                        }
                    }
                }
            } else if (toBuild.activeSelf)
            {
                toBuild.SetActive(false);
            }
        }
    }

    public void SetBuildingPrefab(GameObject prefab)
    {
        // if player has enough resources
        buildingPrefab = prefab;
        int playerWoods = player.GetComponent<MyResources>().getWoods();
        int playerStones = player.GetComponent<MyResources>().getStones();
        if (playerWoods >= buildingPrefab.GetComponent<BuildingManager>().getWoods() && playerStones >= buildingPrefab.GetComponent<BuildingManager>().getStones())
        {
            PrepareBuilding();
        } else
        {
            Debug.Log("You don't have enough resources for this type of building");
        }
        //this.gameObject.GetComponent<PhotonView>().RPC("PrepareBuilding", RpcTarget.All, null);
    }
    private void PrepareBuilding()
    {
        if (toBuild)
        {
            //Destroy(toBuild);
            PhotonNetwork.Destroy(toBuild);
        }
        //toBuild = Instantiate(buildingPrefab);
        toBuild = PhotonNetwork.Instantiate(buildingPrefab.name, Vector3.zero, Quaternion.identity);
        toBuild.SetActive(false);

        BuildingManager manager = toBuild.GetComponent<BuildingManager>();
        manager.isFixed = false;
        manager.SetPlacementMode(PlacementMode.Valid);
        //toBuild.GetComponent<BuildingManager>().gameObject.GetComponent<PhotonView>().RPC("setIsFixed", RpcTarget.All, false);
        toBuild.GetComponent<BuildingManager>().gameObject.GetComponent<PhotonView>().RPC("SetPlacementMode", RpcTarget.All, PlacementMode.Valid);
    }

    [PunRPC]
    private void TransformPos()
    {
        toBuild.transform.position = hit.point;
    }
}
