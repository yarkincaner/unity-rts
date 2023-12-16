using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private NavMeshSurface surface;
    private bool needUpdate;

    private void Start()
    {
        needUpdate = false;
    }

    void Update()
    {
        if (needUpdate)
        {
            BakeSurface();
            needUpdate = false;
        }
    }

    void BakeSurface()
    {
        surface.BuildNavMesh();
    }

    public bool getNeedUpdate()
    {
        return needUpdate;
    }

    public void setNeedUpdate(bool value)
    {
        needUpdate = value;
    }
}
