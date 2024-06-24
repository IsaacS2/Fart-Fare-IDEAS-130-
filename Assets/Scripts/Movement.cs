using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* References: https://www.youtube.com/watch?app=desktop&v=Pt6ay_JFyyQ
 * https://gamedevbeginner.com/how-to-use-fixed-update-in-unity/
 * https://discussions.unity.com/t/how-to-stop-player-velocity-completely/231371
 */

public class Movement : MonoBehaviour
{
    private GameObject player;
    private GameObject newFart;
    private Vector3 inputVal;
    private Vector3 velocityKiller;
    private Vector3 autoSpeedVec;
    private Vector3 splitForce;
    private int life = 2;
    private float autoSpeed;
    private readonly float dirSpeed = 575f;
    private Rigidbody rb;
    public bool fartOut;
    public bool bottomFloor;
    // Start is called before the first frame update
    void Start()
    {
        fartOut = false;
        bottomFloor = false;
        player = GameObject.Find("Player");
        newFart = null;
        rb = GetComponent<Rigidbody>();
        velocityKiller = new Vector3(0,0,0);
        autoSpeed = 0.2f;
        autoSpeedVec = new Vector3(0, 0, autoSpeed);
        splitForce = new Vector3(100f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        inputVal = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * dirSpeed;
        //Debug.Log("Velo: " + rb.velocity);

        if (Input.GetKey("space"))
        {
            if (Input.GetKeyDown("space"))
            {
                if (life == 2)
                {
                    GetComponent<Renderer>().material.color = Color.yellow;
                    life--;
                    fartOut = true;
                    newFart = Instantiate(gameObject, transform.position, transform.rotation);
                    newFart.GetComponent<Movement>().setNewFart();
                    newFart.GetComponent<Renderer>().material.color = Color.yellow;
                    player.GetComponent<PlayerMovement>().changeFartCount();
                }
            }
        }
        else
        {
            newFart = null;
        }
    }

    public void setNewFart()
    {
        life--;
        fartOut = true;
    }

    public void boostHealth(int health)
    {
        life += health;
        GetComponent<Renderer>().material.color = Color.green;
    }

    private void FixedUpdate()
    {
        //rb.MovePosition(transform.position + inputVal);
        rb.AddForce(inputVal);
        rb.MovePosition(transform.position + autoSpeedVec);
        rb.velocity = velocityKiller;

        if (newFart != null)
        {
            newFart.GetComponent<Rigidbody>().AddForce(splitForce);
            rb.AddForce(-splitForce);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            life--;
            GetComponent<Renderer>().material.color = Color.yellow;
            if (life <= 0)
            {
                player.GetComponent<PlayerMovement>().changeFartCount();
                foreach (GameObject fart in GameObject.FindGameObjectsWithTag("Fart"))
                {
                    fart.GetComponent<Movement>().fartOut = false;
                }
                Destroy(gameObject);
            }
        }

        if (other.gameObject.tag == "Trash")
        {
            if (life <= 1 && !fartOut)
            {
                life++;
            }
        }

        if (other.gameObject.tag == "Human")
        {
            if (!other.gameObject.GetComponent<Human>().fartedOn)
            {
                other.gameObject.GetComponent<Human>().fartedOn = true;
            }
        }

        if (other.gameObject.tag == "End")
        {
            SceneManager.LoadScene(2);
        }

        if (other.gameObject.tag == "Butt")
        {
            SceneManager.LoadScene(4);
        }

        if (other.gameObject.tag == "Floor" && !bottomFloor)
        {
            bottomFloor = true;
            player.GetComponent<PlayerMovement>().setBottom();
        }

        if (other.gameObject.tag == "AltEnd")
        {
            SceneManager.LoadScene(3);
        }
    }
}
