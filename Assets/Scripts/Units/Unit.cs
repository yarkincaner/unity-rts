using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private int numOfWoods;
    [SerializeField] private int numOfStones;

    public int getNumOfStones() {  return numOfStones; }
    public int getNumOfWoods() {  return numOfWoods; }
}
