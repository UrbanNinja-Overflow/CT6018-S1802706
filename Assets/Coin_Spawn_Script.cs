using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Spawn_Script : MonoBehaviour
{
    public int random;
    public List<GameObject> coin_Points;
    public GameObject coin;
 
    public Movement_Script MS;
    public int bool_Check;
 
 //randomly spawn coin
    public void get_Random_number()
    {
        DestroyAllObjects();
        random = Random.Range(0, coin_Points.Count);
        Instantiate(coin);
        coin.transform.position = coin_Points[random].transform.position;
    }
 
    //find all coins and destroy them
    void DestroyAllObjects()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject destoy_Coins in coins)
            GameObject.Destroy(destoy_Coins);
    }
 
    private void Start()
    {
        get_Random_number();
    }
 
    //force spawn coin
    private void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            bool_Check = 0;
            Debug.Log("Coin Button Presed");
            get_Random_number();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Application closing");
        }

    }
}
