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
    Vector3 oldPos = new Vector3(0, 0, 0);
    int pointNum = 0;

    void Start()
    {
        lr = GetComponent<LineRenderer>();

        Vector3 startPos = start.transform.position;
        Vector3 endPos = end.transform.position;

        lr.startWidth = width;
        lr.endWidth = width;
        lr.materials[0] = Black;
        lr.SetPosition(0, oldPos);
        Vector3[] positions = new Vector3[6];
        positions[0] = startPos;
        positions[1] = endPos;
        positions[2] = new Vector3(Random.Range(-5,5), Random.Range(-5, 5), Random.Range(-5, 5));
        positions[3] = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));
        positions[4] = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));
        positions[5] = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));
        lr.positionCount = positions.Length+1;
        for(int i =0;i!= positions.Length;i++)
            addLineSegment(positions[i]);
        lr.Simplify(0.1f);
    }

    void addLineSegment(Vector3 end)
    {
        print(pointNum);
        GameObject coll = new GameObject("colliderBoi");
        coll.transform.parent = this.gameObject.transform;
        BoxCollider boxCollider = coll.AddComponent<BoxCollider>();
        coll.transform.position = (oldPos + end) / 2; // + new Vector3(0, width, width)/2;
        float dist = Vector3.Distance(oldPos, end);
        float hieght = (end - oldPos).y;
        float depth = (end - oldPos).z;
        float widthh = (end - oldPos).x;
        int mult = 1;
        int multz = -1;
        
        if (((hieght > 0) == true && (depth > 0) == true && (widthh > 0) == true))
        {
            print("SAME");
            mult = -1;
            multz = 1;
        }
        else if (((hieght > 0) == false && (depth > 0) == true && (widthh > 0) == true))
        {
            print("height neg");
            mult = -1;
            multz = 1;
        }
        else if (((hieght > 0) == true && (depth > 0) == false && (widthh > 0) == true) )
        {
            print("depth neg");
            mult = -1;
            multz = 1;
        }
        else if (((hieght > 0) == false && (depth > 0) == false && (widthh > 0) == true) )
        {
            print("width neg");
            mult = -1;
            multz = 1;
        }




        float xRot = Mathf.Rad2Deg * Mathf.Asin(widthh / dist);
        float yRot = Mathf.Rad2Deg * Mathf.Asin(depth / Mathf.Sqrt(Mathf.Pow(widthh, 2) + Mathf.Pow(depth, 2)));
        float zRot = Mathf.Rad2Deg * Mathf.Asin(hieght / dist);
        print(xRot +" "+ yRot  + " " + zRot +" - "+ multz);
        print(widthh + " " + depth + " " + hieght);
        coll.transform.localRotation = Quaternion.Euler(0, mult*yRot, multz*zRot); //
        boxCollider.size = new Vector3(dist, width, width);
        oldPos = end;
        pointNum++;
        
        lr.SetPosition(pointNum, end);
    }

    void Update()
    {

    }
}
