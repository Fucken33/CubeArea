using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour
{

    public float scrollSpeed = 2f;
    public float tileWidth = 19.2f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newPos = Mathf.Repeat(Time.time * scrollSpeed, tileWidth);
        transform.position = startPosition + Vector3.left * newPos;
    }
}
