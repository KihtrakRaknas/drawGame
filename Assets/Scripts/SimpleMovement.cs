using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    float speedx = 0f;
    float speedz = 0f;
    public static bool collide = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            speedz = 3f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            speedz = -3f;
        }
        else
        {
            speedz = 0f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            speedx = -2f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            speedx = 2f;
        }
        else
        {
            speedx = 0f;
        }
        transform.Translate(new Vector3(speedx * Time.deltaTime, 0f, speedz * Time.deltaTime));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("obstacle"))
        {
            collide = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("obstacle"))
        {
            collide = false;
        }
    }
}

