using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pizza_Pick_Up_Script : MonoBehaviour
{
    public Movement_Script pizza_Bool;
    public GameObject Player;

    //find the player and movment script
    private void Start()
    {
        FindPlayer();
        pizza_Bool = Player.GetComponent<Movement_Script>();
        pizza_Bool.pizza_Pick_Up = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //on collision player picks up pizza
        if(other.gameObject.tag == "Player")
        {
            pizza_Bool.pizza_Pick_Up = true;
            //GSS.bool_Check = 0;
            Debug.Log("pizza picked up");
            Destroy(gameObject);
        } 
    }
    //find the player
    void FindPlayer()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject Found_Player in player)
            Player = Found_Player;
    }



}
