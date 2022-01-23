using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag_Script1 : MonoBehaviour
{

    public Movement_Script pizza_Bool;
    public GameObject Player;

    private void Start()
    {
        FindPlayer();
        
        pizza_Bool = Player.GetComponent<Movement_Script>();
    }
    public void OnTriggerEnter(Collider other)
    {
        //allow player to deliver pizza
        if (other.gameObject.tag == "Player" && pizza_Bool.pizza_Pick_Up == true)
        {
            pizza_Bool.pizza_Pick_Up = false;
            Debug.Log("pizza delivered");
            Destroy(gameObject);

        }
    }

    void FindPlayer()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject Found_Player in player)
            Player = Found_Player;
    }

}
