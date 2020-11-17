using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform ball;
    public Vector3 offset;

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
        //The position of the camera is the position of the ball plus offset
        transform.position = ball.position + offset;
    }
}
