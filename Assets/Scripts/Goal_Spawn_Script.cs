using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Spawn_Script : MonoBehaviour
{
    public int random;
    public List<GameObject> delivary_Points;
    public GameObject flag;

    public Movement_Script MS;
    public int bool_Check;

    //randomly spawn flag

    public void get_Random_number()
    {
        DestroyAllObjects();
        random = Random.Range(0, delivary_Points.Count);
        Instantiate(flag);
        flag.transform.position = delivary_Points[random].transform.position;
    }
    //find all flags and destroy them

    void DestroyAllObjects()
    {
        GameObject[] Flags = GameObject.FindGameObjectsWithTag("Flag");
        foreach (GameObject destoy_Flag in Flags)
            GameObject.Destroy(destoy_Flag);
    }

    //force spawn flag

    private void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            bool_Check = 0;
            Debug.Log("Flag Button Presed");
            get_Random_number();
        }
        if (MS.pizza_Pick_Up == true && bool_Check == 0)
        {
            bool_Check += 1;
            get_Random_number();
        }
    }
}
