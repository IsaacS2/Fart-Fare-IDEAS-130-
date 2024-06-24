using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private float transformDivider;
    private float cameraDist;
    private float bonusVerticality;
    // Start is called before the first frame update
    void Start()
    {
        transformDivider = 0.3f;
        cameraDist = -4.5f;
        bonusVerticality = 0;
    }

    public void increaseY()
    {
        bonusVerticality += 5;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion newRotation = Quaternion.Euler((Input.GetAxis("Vertical") * 2), Input.GetAxis("Horizontal") * 2, 0);
        transform.SetPositionAndRotation(new Vector3(player.transform.position.x * transformDivider, (player.transform.position.y * transformDivider) + bonusVerticality, player.transform.position.z + cameraDist), 
            Quaternion.Slerp(transform.rotation, newRotation, 0.005f));
    }
}
