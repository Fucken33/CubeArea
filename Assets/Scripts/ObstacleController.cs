using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour
{

    public const float initialXVel = -4;
    public static float xVelocity = -4;

    public int id;

    void Start()
    {
        xVelocity = -4;
    }

    // Update is called once per frame
    void Update()
    {
        MoveX(xVelocity * Time.deltaTime);
    }

    public void MoveX(float moveX)
    {
        Vector3 newPos = transform.position;
        newPos.x += moveX;
        transform.position = newPos;
    }

    public void nextId()
    {
        id = id + 2;
    }
}