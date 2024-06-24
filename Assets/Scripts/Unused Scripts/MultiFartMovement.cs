using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiFartMovement : MonoBehaviour
{
    private Vector3 fartStart;
    // Start is called before the first frame update
    void Start()
    {
        fartStart = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdjustLocation(Vector3 playerPosition)
    {
        transform.position = playerPosition + fartStart;
    }
}
