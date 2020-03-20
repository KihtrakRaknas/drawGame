using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cameraFollow : MonoBehaviour
{
    public GameObject player;
    float interpSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        float wiggle = 3; 
        float playY = player.transform.position.y + (Quaternion.AngleAxis(player.transform.localEulerAngles.z, Vector3.forward) * LineTest.averagePos).y + .5f;
        float playX = player.transform.position.x + (Quaternion.AngleAxis(player.transform.localEulerAngles.z, Vector3.forward) * LineTest.averagePos).x;
        //print((Quaternion.AngleAxis(-player.transform.localEulerAngles.z, Vector3.forward) * LineTest.averagePos).x + " "+ (Quaternion.AngleAxis(-player.transform.localEulerAngles.z, Vector3.forward) * LineTest.averagePos).y);
        Vector3 targetPos = new Vector3(playX - wiggle, playY, transform.position.z);
        if(playX < this.transform.position.x)
            targetPos = new Vector3(playX + wiggle, playY, transform.position.z);
        if (Mathf.Abs(playX - this.transform.position.x)> wiggle)
            transform.position = Vector3.Lerp(transform.position, targetPos, interpSpeed);
        else
            transform.position = new Vector3(transform.position.x , playY, transform.position.z);

    }
}