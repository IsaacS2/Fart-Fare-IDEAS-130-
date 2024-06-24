using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private bool switchSet;
    // Start is called before the first frame update
    void Start()
    {
        switchSet = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!switchSet) {
            GameObject.Find("SwitchSystem").GetComponent<SwitchSystem>().switchSet();
            gameObject.transform.Rotate(new Vector3(180, 0, 0));
            switchSet = true;
        }
    }
}
