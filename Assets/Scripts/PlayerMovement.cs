using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private GameObject[] farts;
    [SerializeField]
    private GameObject bottomFloor;
    private bool changeFarts;
    private bool bottom;
    private bool floorConfirmed;
    private bool floorSwitchComplete;
    public Camera playerCam;

    // Start is called before the first frame update
    void Start()
    {
        farts = GameObject.FindGameObjectsWithTag("Fart");
        changeFarts = false;
        floorConfirmed = false;
        bottom = false;
        floorSwitchComplete = false;
    }

    public void changeFartCount()
    {
        changeFarts = true;
    }

    public void setBottom()
    {
        bottom = true;
        floorConfirmed = true;
        changeFartCount();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (changeFarts)
        {
            farts = GameObject.FindGameObjectsWithTag("Fart");
            changeFarts = false;
            if (farts.Length <= 0)
            {
                SceneManager.LoadScene(2);
            }
        }

        if (farts.Length > 1)
        {
            transform.position = (farts[0].transform.position + farts[1].transform.position) / 2;
        }
        else if (farts.Length > 0)
        {
            transform.position = farts[0].transform.position;
        }

        if (!bottom && bottomFloor != null)
        {
            if (transform.position.z > bottomFloor.transform.position.z) {
                floorConfirmed = true;
            }
        }

        if (floorConfirmed && !floorSwitchComplete) {
            if (bottom)
            {
                farts = GameObject.FindGameObjectsWithTag("Fart");
                var startingFartCount = farts.Length;
                var endingFartCount = farts.Length;
                GameObject livingFart = null;
                Debug.Log(startingFartCount);
                foreach (GameObject fart in farts)
                {
                    if (!fart.GetComponent<Movement>().bottomFloor)
                    {
                        Destroy(fart);
                        endingFartCount--;
                    }
                    else
                    {
                        livingFart = fart;
                    }
                }
                Debug.Log(endingFartCount);
                if (endingFartCount < startingFartCount)
                {
                    livingFart.GetComponent<Renderer>().material.color = Color.green;
                    livingFart.GetComponent<Movement>().boostHealth(startingFartCount - endingFartCount);
                }
                floorConfirmed = false;
                changeFarts = true;
            }
            else
            {
                playerCam.GetComponent<CameraControl>().increaseY();
                floorConfirmed = false;
            }
            floorSwitchComplete = true;
        }
    }
}
