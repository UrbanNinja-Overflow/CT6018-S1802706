using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza_Spawn_Script : MonoBehaviour
{
    public int random;
    public List<GameObject> pizzas_Pick_Up_points;
    public GameObject pizza;

    public Movement_Script MS;
    public int bool_Check;

    //randomly spawn pizza

    public void get_Random_number()
    {
        DestroyAllObjects();
        random = Random.Range(0, pizzas_Pick_Up_points.Count);
        Instantiate(pizza);
        pizza.transform.position = new Vector3(pizzas_Pick_Up_points[random].transform.position.x, pizzas_Pick_Up_points[random].transform.position.y - 4, pizzas_Pick_Up_points[random].transform.position.z);
    }
    //find all pizzas and destroy them

    void DestroyAllObjects()
    {
        GameObject[] Flags = GameObject.FindGameObjectsWithTag("Pizza");
        foreach (GameObject destoy_Flag in Flags)
            GameObject.Destroy(destoy_Flag);
    }

    private void Start()
    {
        get_Random_number();
    }

    //force spawn pizza

    private void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            bool_Check = 0;
            Debug.Log("Pizza Button Presed");
            get_Random_number();
        }

        if (MS.pizza_Pick_Up == false && bool_Check == 0)
        {
            bool_Check += 1;
            get_Random_number();
        }
    }
}
