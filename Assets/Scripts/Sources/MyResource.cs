using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyResource : MonoBehaviour
{
    [SerializeField] private int numOfResources;
    public int getNumOfResources() {  return numOfResources; }
}
