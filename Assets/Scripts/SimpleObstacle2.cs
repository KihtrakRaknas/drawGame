using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObstacle2 : MonoBehaviour
{
    public float startPos;
    public float endPos;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        startPos = 8f;
        endPos = -12f;
        if (tag.Equals("group1"))
        {
            speed = -5f;
        }
        if (tag.Equals("group2"))
        {
            speed = 5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > startPos)
        {
            speed *= -1;
        }
        if (transform.position.x < endPos)
        {
            speed *= -1;
        }
        transform.Translate(new Vector3(speed * Time.deltaTime, 0f, 0f));
    }
}
