using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Script : MonoBehaviour
{

    public Transform target;

    [Header("Attributes")]
    public float range = 15f;
    public Transform part_To_Rotate;


    [Header("Setup Feilds")]
    public string enemy_Tag = "Flag";
    public float turn_Speed = 2f;

    public Transform fire_Point;
    
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    //find objext with the tag find the shortesst distance and point towards that one
    void UpdateTarget()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag(enemy_Tag);
        float short_Dis = Mathf.Infinity;
        GameObject nearest_Enemy = null;

        foreach (GameObject enemy in Enemies)
        {
            float distance_To_Enemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance_To_Enemy < short_Dis)
            {
                short_Dis = distance_To_Enemy;
                nearest_Enemy = enemy;
            }
        }

        if (nearest_Enemy != null && short_Dis <= range)
        {
            target = nearest_Enemy.transform;
        }

    }
    //continue pointing towards the closest object with tag
    void Update()
    {
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion look_Rotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(part_To_Rotate.rotation, look_Rotation, Time.deltaTime * turn_Speed).eulerAngles;
        part_To_Rotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}

