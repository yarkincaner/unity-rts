using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacer : MonoBehaviour
{
    public static BuildingPlacer instance; // Singleton Pattern instance
    public LayerMask terrainLayerMask;

    private GameObject buildingPrefab;
    private GameObject toBuild;

    private Camera mainCamera;

    private Ray ray;
    private RaycastHit hit;

    private void Awake()
    {
        instance = this;

        mainCamera = Camera.main;
        buildingPrefab = null;
    }

    private void Update()
    {
        // If in build mode
        if (buildingPrefab != null)
        {
            // right-click: cancel build mode
            if (Input.GetMouseButtonDown(1)) {
                Destroy(toBuild);
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

            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000f, terrainLayerMask))
            {
                if (!toBuild.activeSelf) toBuild.SetActive(true);
                toBuild.transform.position = hit.point;

                // left-click
                if (Input.GetMouseButtonDown(0))
                {
                    BuildingManager manager = toBuild.GetComponent<BuildingManager>();
                    if (manager.hasValidPlacement)
                    {
                        manager.SetPlacementMode(PlacementMode.Fixed);

                        // shift-key: chain builds
                        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
                            toBuild = null; // to avoid destruction
                            PrepareBuilding();
                        } else
                        {
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
        buildingPrefab = prefab;
        PrepareBuilding();
    }

    private void PrepareBuilding()
    {
        if (toBuild) Destroy(toBuild);

        toBuild = Instantiate(buildingPrefab);
        toBuild.SetActive(false);

        BuildingManager manager = toBuild.GetComponent<BuildingManager>();
        manager.isFixed = false;
        manager.SetPlacementMode(PlacementMode.Valid);
    }
}
