using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObstacle1 : MonoBehaviour
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
            if (counter < 3)
            {
                transform.Translate(0f, 3f * Time.deltaTime, 0f);
                collided = true;
            }
        }
        if (counter > 3 && collided)
        {
            moved = true;

        }
        if(!SimpleMovement.collide && moved)
        {
            transform.Translate(0f, -3f * Time.deltaTime, 0f);
        }
    }
}
