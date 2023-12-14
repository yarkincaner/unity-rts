using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlacementMode
{
    Fixed,
    Valid,
    Invalid
}

public class BuildingManager : MonoBehaviourPunCallbacks
{
    public Material validPlacementMaterial;
    public Material invalidPlacementMaterial;

    public MeshRenderer[] meshComponents;
    private Dictionary<MeshRenderer, List<Material>> initialMaterials;

    [HideInInspector] public bool hasValidPlacement;
    [HideInInspector] public bool isFixed;

    private int nObstacles;

    private GameObject slot1_button;
    private GameObject slot2_button;

    private void Awake()
    {
        hasValidPlacement = true;
        isFixed = true;
        nObstacles = 0;
        slot1_button = GameObject.Find("Slot1_Button");
        slot2_button = GameObject.Find("Slot2_Button");
        
        slot1_button.GetComponent<Image>().enabled = false;
        slot1_button.GetComponent<Button>().enabled = false;
        slot2_button.GetComponent<Image>().enabled = false;
        slot2_button.GetComponent<Button>().enabled = false;
        //unitsMenuImage = unitsMenu.GetComponent<Image>();
        //unitsMenuImage.enabled = false;
        //unitsMenu.SetActive(false);

        InitializeMaterials();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isFixed) return;

        // ignore ground objects
        if (IsGround(other.gameObject)) return;

        nObstacles++;
        SetPlacementMode(PlacementMode.Invalid);
    }

    private void OnTriggerExit(Collider other)
    {
        if (isFixed) return;

        // ignore ground objects
        if (IsGround(other.gameObject)) return;

        nObstacles--;
        if (nObstacles == 0)
            SetPlacementMode(PlacementMode.Valid);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        InitializeMaterials();
    }
#endif

    [PunRPC]
    public void SetPlacementMode(PlacementMode mode)
    {
        if (mode == PlacementMode.Fixed)
        {
            isFixed = true;
            hasValidPlacement = true;
        }
        else if (mode == PlacementMode.Valid)
        {
            hasValidPlacement = true;
        }
        else
        {
            hasValidPlacement = false;
        }
        SetMaterial(mode);
    }

    public void SetMaterial(PlacementMode mode)
    {
        if (mode == PlacementMode.Fixed)
        {
            foreach (MeshRenderer r in meshComponents)
                r.sharedMaterials = initialMaterials[r].ToArray();
        }
        else
        {
            Material matToApply = mode == PlacementMode.Valid
                ? validPlacementMaterial : invalidPlacementMaterial;

            Material[] m; int nMaterials;
            foreach (MeshRenderer r in meshComponents)
            {
                nMaterials = initialMaterials[r].Count;
                m = new Material[nMaterials];
                for (int i = 0; i < nMaterials; i++)
                    m[i] = matToApply;
                r.sharedMaterials = m;
            }
        }
    }

    private void InitializeMaterials()
    {
        if (initialMaterials == null)
            initialMaterials = new Dictionary<MeshRenderer, List<Material>>();
        if (initialMaterials.Count > 0)
        {
            foreach (var l in initialMaterials) l.Value.Clear();
            initialMaterials.Clear();
        }

        foreach (MeshRenderer r in meshComponents)
        {
            initialMaterials[r] = new List<Material>(r.sharedMaterials);
        }
    }

    private bool IsGround(GameObject o)
    {
        return ((1 << o.layer) & BuildingPlacer.instance.terrainLayerMask.value) != 0;
    }

    private void OnMouseDown()
    {
        if (photonView.IsMine)
        {
            if (slot1_button.GetComponent<Image>().enabled)
            {
                slot1_button.GetComponent<Image>().enabled = false;
                slot1_button.GetComponent<Button>().enabled = false;
                slot2_button.GetComponent<Image>().enabled = false;
                slot2_button.GetComponent<Button>().enabled = false;
            } else
            {
                slot1_button.GetComponent<Image>().enabled = true;
                slot1_button.GetComponent<Button>().enabled = true;
                slot2_button.GetComponent<Image>().enabled = true;
                slot2_button.GetComponent<Button>().enabled = true;
            }
            //if (unitsMenu.activeInHierarchy)
            //{
            //    unitsMenuImage.enabled = false;
            //    //unitsMenu.SetActive(false);
            //}
            //else
            //{
            //    unitsMenuImage.enabled = false;
            //    //unitsMenu.SetActive(true);
            //}
        }
    }

    [PunRPC]
    public void setIsFixed(bool isFixed)
    {
        this.isFixed = isFixed;
    }

    void HideUI()
    {

    }
}