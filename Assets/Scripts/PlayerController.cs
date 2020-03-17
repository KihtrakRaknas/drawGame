using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float z = transform.rotation.eulerAngles.z;
        if (LineTest.drawing == true)
        {
            z = 0;
        }
        transform.rotation = Quaternion.Euler(0, 0, z);

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

    }

    private void FixedUpdate()
    {
        float xVel = -Input.GetAxis("Horizontal");
        float xFinal = xVel * Time.deltaTime*6;
        if((body.angularVelocity.z>=0 && xFinal<=0) || (body.angularVelocity.z <= 0 && xFinal >= 0))
            body.angularVelocity = new Vector3(body.angularVelocity.x, body.angularVelocity.y, body.angularVelocity.z / (60 * Time.deltaTime));
        body.angularVelocity = new Vector3(body.angularVelocity.x, body.angularVelocity.y, body.angularVelocity.z + xFinal);
        if(Mathf.Abs(body.angularVelocity.z)>=10)
            body.angularVelocity = new Vector3(body.angularVelocity.x, body.angularVelocity.y, 5 * body.angularVelocity.z / Mathf.Abs(body.angularVelocity.z));
    }
}
