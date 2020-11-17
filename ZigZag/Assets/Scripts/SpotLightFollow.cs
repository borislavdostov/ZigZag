using UnityEngine;

public class SpotLightFollow : MonoBehaviour
{
    public static SpotLightFollow instance;

    public Transform ball;
    public Vector3 offset;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If the ball is on the ground
        if (ball.position.y > 1)
        {
            Follow();
        }
    }

    void Follow()
    {
        transform.position = ball.position + offset;
    }
}
