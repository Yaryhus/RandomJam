using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionObjective : MonoBehaviour
{

    public bool reached = false;


    private void OnTriggerEnter(Collider other)
    {
        reached = true;
        Debug.Log("Player reached the waypoint!");
        //Destroy(this.gameObject);
    }

}
