using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject particle;

    //Even if speed field is private, with "[SerializeField]" we can show it in Ball's components
    [SerializeField]
    private float speed;
    bool started;
    bool gameOver;

    Rigidbody rb;

    void Awake()
    {
        //GetComponent<>() can get every component in the current GameObject
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    // Use this for initialization
    void Start()
    {
        started = false;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        //HACK
        //Debug.DrawRay(transform.position, new Vector3(0, -0.5f, 1), Color.green);
        //Debug.DrawRay(transform.position, new Vector3(1, -0.5f, 0), Color.green);

        //if (!Physics.Raycast(transform.position, new Vector3(0, -0.5f, 1), 1f) && rb.velocity.z > 0)
        //{
        //    rb.velocity = new Vector3(speed, 0, 0);
        //}
        //else if (!Physics.Raycast(transform.position, new Vector3(1, -0.5f, 0), 1f) && rb.velocity.x > 0)
        //{
        //    rb.velocity = new Vector3(0, 0, speed);
        //}

        if (!started)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Set velocity to x
                rb.velocity = new Vector3(speed, 0, 0);
                started = true;

                //We are calling StartGame() method from GameManager
                GameManager.instance.StartGame();
            }
        }

        //This will draw the ray
        Debug.DrawRay(transform.position, Vector3.down, Color.red);

        //Physics.Raycast makes ray and returns false when its not colliding with any object(that is collider).
        //the position of the ray is the position of the ball, Vector3.down the ray will go down, 1f is the size of the ray
        if (!Physics.Raycast(transform.position, Vector3.down, 1f))
        {
            gameOver = true;
            //This will cause the ball to fall down
            rb.constraints = RigidbodyConstraints.None;
            rb.velocity = new Vector3(0, -25f, 0);

            //The main camera
            Camera.main.GetComponent<CameraFollow>().gameOver = true;
            SpotLightFollow.instance.GameOver();

            //We are calling GameOver() method from GameManager
            GameManager.instance.GameOver();
        }

        //Input.GetMouseButtonDown(0) gets the left mouse button click
        if (Input.GetMouseButtonDown(0) && !gameOver)
        {
            SwitchDirection();
        }
    }

    void SwitchDirection()
    {
        //If the ball is moving to z
        if (rb.velocity.z > 0)
        {
            //Change the moving from z to x
            rb.velocity = new Vector3(speed, 0, 0);
        }
        else if (rb.velocity.x > 0)
        {
            //Change the moving from x to z
            rb.velocity = new Vector3(0, 0, speed);
        }
    }

    //When this gameObject(Ball) enters a trigger, if the trigger is diamond, the diamont is destroyed.
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Diamond")
        {
            //We can get the instance of Instantiate method, but we must call "as GameObject" at the end.
            GameObject part = Instantiate(particle, col.gameObject.transform.position, Quaternion.identity);
            //Destroying the diamond
            Destroy(col.gameObject);
            //Destoying the particle
            Destroy(part, 3f);
            //Incrementing the score by 2 points
            ScoreManager.instance.IncrementScore(2);
        }
    }
}
