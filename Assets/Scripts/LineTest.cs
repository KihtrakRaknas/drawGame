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
    float width = 0.45f;
    Vector3 startPos;
    Vector3 endPos;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        
        startPos = start.transform.position;
        endPos = end.transform.position;

        lr.startWidth = width;
        lr.endWidth = width;
        lr.materials[0] = Black;
        addLineSegment(startPos, endPos);
    }

    void addLineSegment(Vector3 start, Vector3 end)
    {
        GameObject coll = new GameObject("colliderBoi");
        coll.transform.parent = this.gameObject.transform;
        BoxCollider boxCollider = coll.AddComponent<BoxCollider>();
        coll.transform.position = (start + end) / 2 + new Vector3(0, width, width)/2;
        float dist = Vector3.Distance(start, end);
        float hieght = (end - start).y;
        float depth = (end - start).z;
        float widthh = (end - start).x;
        print("asin"+ hieght +" "+ dist); 
        print(Mathf.Rad2Deg * Mathf.Asin(hieght / dist));
        print(-Mathf.Rad2Deg * Mathf.Asin(depth / dist));
        int mult = 1;

        float xRot = Mathf.Rad2Deg * Mathf.Asin(widthh / Mathf.Sqrt(Mathf.Pow(depth,2) + Mathf.Pow(hieght, 2)));
        float yRot = Mathf.Rad2Deg * Mathf.Asin(depth / Mathf.Sqrt(Mathf.Pow(widthh, 2) + Mathf.Pow(hieght, 2)));
        float zRot = -Mathf.Rad2Deg * Mathf.Asin(hieght / Mathf.Sqrt(Mathf.Pow(depth, 2) + Mathf.Pow(widthh, 2)));
        print(xRot +" "+ yRot  + " " + zRot);
        coll.transform.localRotation = Quaternion.Euler(0, yRot, zRot); //
        boxCollider.size = new Vector3(dist, width, width);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

    void Update()
    {

    }
}
