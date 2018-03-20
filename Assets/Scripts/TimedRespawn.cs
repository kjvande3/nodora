using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * @author Jeremy Powell
 * @date 3/19/2018
 * Class Timed Respawn
 * - Attach to any object directly
 * - Will respawn the object after specified number of seconds
 */
public class TimedRespawn : MonoBehaviour {

    public int timeInSec = 5;

    void respawn()
    {
        gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        Invoke("respawn", timeInSec);
    }
}
