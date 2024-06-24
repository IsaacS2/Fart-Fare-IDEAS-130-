using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSystem : MonoBehaviour
{
    // Start is called before the first frame update
    private int switchesLeft;
    void Start()
    {
        switchesLeft = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchSet()
    {
        switchesLeft--;
        if (switchesLeft <= 0)
        {
            Destroy(gameObject);
        }
    }
}
