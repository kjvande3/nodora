using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestObjectController : MonoBehaviour {

    public GameObject chestObject;
    public bool show = false;

    private chestController chest;

    // Use this for initialization
    void Start()
    {
        chestObject.SetActive(show);
    }

    // Update is called once per frame
    void Update()
    {
        if (chest.show == true) { chestObject.SetActive(show = chest.show); }
    }
}
