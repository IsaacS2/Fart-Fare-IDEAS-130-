using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public bool fartedOn;
    private bool dead;
    // Start is called before the first frame update
    void Start()
    {
        fartedOn = false;
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fartedOn && !dead)
        {
            GetComponent<Renderer>().material.color = Color.green;
            dead = true;
        }
    }
}
