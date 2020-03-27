using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    float counter=0;
    bool moved = false;
    bool collided = false;
    bool down = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (down) { 
            transform.Translate(0f, -3f * Time.deltaTime, 0f);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        down = true;
    }
}