using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    float counter=0;
    bool moved = false;
    bool collided = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (SimpleMovement.collide)
        {
            counter = 0;
            if (counter < 1)
            {
                transform.Translate(0f, 1f * Time.deltaTime, 0f);
                collided = true;
            }
        }
        if (counter > 1 && collided)
        {
            moved = true;

        }
        if(!SimpleMovement.collide && moved)
        {
            counter = 0;
            if (counter < 0.3)
            {
                transform.Translate(0f, -1f * Time.deltaTime, 0f);
            }
        }
    }
}
