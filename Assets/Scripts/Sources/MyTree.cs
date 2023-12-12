using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTree : MonoBehaviour
{
    private int health;
    [SerializeField] private int numOfWoods;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getHealth() { return health; }
    public void setHealth(int health) { this.health = health; }
    public int getNumOfWoods() {  return numOfWoods; }
}
