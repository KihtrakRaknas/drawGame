using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingCube : MonoBehaviour
{
    public float maxPos;
    public bool x;
    float speed = -300;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var pos = transform.position.x;
        if (!x)
            pos = transform.position.z;
        if (Mathf.Abs(pos) > maxPos)
        {
            speed = -300 * pos/ Mathf.Abs(pos);
        }
        print(speed);
        var vel = new Vector3(0f, 0f, speed * Time.deltaTime);
        if (x)
            vel =  new Vector3(speed * Time.deltaTime, 0f, 0f);
        print(vel);
        this.GetComponent<Rigidbody>().velocity = vel;
    }
}
