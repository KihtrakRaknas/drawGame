using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTest : MonoBehaviour
{
    LineRenderer lr;
    public GameObject start;
    public GameObject end;
    public Material Black;
    float dist;
    float counter = 0;
    float drawSpeed = 2f;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        lr.SetPosition(0, start.transform.position);
        lr.startWidth = 0.45f;
        lr.endWidth = 0.45f;
        dist = Vector3.Distance(start.transform.position, end.transform.position);
        lr.materials[0] = Black;
        if (counter < dist)
        {
            counter += 0.1f / drawSpeed;
            float x = Mathf.Lerp(0, dist, counter);
            Vector3 pointAlongLine = x * Vector3.Normalize(end.transform.position - start.transform.position) + start.transform.position;
            lr.SetPosition(1, pointAlongLine);
        }
    }
}
