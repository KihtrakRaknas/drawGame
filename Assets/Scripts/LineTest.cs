﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LineTest : MonoBehaviour
{
    LineRenderer lr;
    public Material Black;
    float dist;
    float counter = 0;
    float drawSpeed = 2f;
    float width = 0.45f;
    Vector3 oldPos = new Vector3(0, 0, 0);
    int pointNum = 0;
    List<Vector3> positions = new List<Vector3>();
    List<GameObject> colliders = new List<GameObject>();
    static public bool drawing = false;
    static public Vector3 averagePos = new Vector3(0,0,0);
    Rigidbody body;

    void Start()
    {
        lr = GetComponent<LineRenderer>();


        lr.startWidth = width;
        lr.endWidth = width;
        lr.materials[0] = Black;
        lr.SetPosition(0, oldPos);

        
        lr.Simplify(1f);
        body = GetComponent<Rigidbody>();
    }

    void addLineSegment(Vector3 end)
    {

        float dist = Vector3.Distance(oldPos, end);
        if (dist != 0)
        {
            addLineSegmentIgnoreDist(end);
        }
        
    }

    void addLineSegmentIgnoreDist(Vector3 end)
    {

        float dist = Vector3.Distance(oldPos, end);
            positions.Add(end);
            lr.positionCount = positions.Count + 1;

            averagePos = new Vector3(0, 0, 0);
            for (int i = positions.Count - 1; i != -1; i--)
            {
                averagePos += positions[i] / positions.Count;
            }

            GameObject coll = new GameObject("colliderBoi");
            coll.transform.parent = this.gameObject.transform;
            colliders.Add(coll);
            BoxCollider boxCollider = coll.AddComponent<BoxCollider>();
            coll.transform.localPosition = (oldPos + end) / 2; // + new Vector3(0, width, width)/2; 
            float hieght = (end - oldPos).y;
            float depth = (end - oldPos).z;
            float widthh = (end - oldPos).x;
            int mult = 1;
            int multz = -1;

            if (((hieght > 0) == true && (depth > 0) == true && (widthh > 0) == true))
            {
                mult = -1;
                multz = 1;
            }
            else if (((hieght > 0) == false && (depth > 0) == true && (widthh > 0) == true))
            {
                mult = -1;
                multz = 1;
            }
            else if (((hieght > 0) == true && (depth > 0) == false && (widthh > 0) == true))
            {
                mult = -1;
                multz = 1;
            }
            else if (((hieght > 0) == false && (depth > 0) == false && (widthh > 0) == true))
            {
                mult = -1;
                multz = 1;
            }




            float xRot = Mathf.Rad2Deg * Mathf.Asin(widthh / dist);
            float yRot;
            if (depth != 0)
                yRot = Mathf.Rad2Deg * Mathf.Asin(depth / Mathf.Sqrt(Mathf.Pow(widthh, 2) + Mathf.Pow(depth, 2)));
            else
                yRot = 0;
            float zRot = Mathf.Rad2Deg * Mathf.Asin(hieght / dist);
            coll.transform.localRotation = Quaternion.Euler(0, mult * yRot, multz * zRot); //
            boxCollider.size = new Vector3(dist, width, width);
            oldPos = end;
            pointNum++;

            lr.SetPosition(pointNum, end);
    }

    Vector2 oldInput;
    bool firstClick = true;
    public void Reset()
    {
        oldPos = new Vector3(0, 0, 0);
        positions = new List<Vector3>();
        
        lr.positionCount = 0;
        for(int i = colliders.Count-1; i!= -1; i--)
        {
            Destroy( colliders[i] );
        }
        colliders.Clear();
        pointNum = 0;
        addLineSegmentIgnoreDist(new Vector3(0, 0, 0));
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        body.angularVelocity = new Vector3(0, 0, 0);
        body.velocity = new Vector3(0, 0, 0);
        Reset();
        body.useGravity = false;
        this.transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                oldInput = touch.position;
                Reset();
                //List<Vector3> positions = new List<Vector3>();
                //GetComponent<Rigidbody>().useGravity = false;
                drawing = true;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 relitivePos = touch.position - oldInput;

                addLineSegment(new Vector3(relitivePos.x, relitivePos.y, 0));
            }
            if (touch.phase == TouchPhase.Ended)
            {
                body.useGravity = true;
                drawing = false;
            }
        }

        if (Input.GetButton("Fire1"))
        {
            if (firstClick == true) {
                firstClick = false;
                oldInput = Input.mousePosition;
                Reset();
                drawing = true;
            }
            else
            {
                Vector2 relitivePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - oldInput;
                relitivePos /= Screen.width/10;
                print(transform.localEulerAngles.z);
                relitivePos = Quaternion.AngleAxis(-transform.localEulerAngles.z, Vector3.forward) * relitivePos;
                addLineSegment(new Vector3(relitivePos.x, relitivePos.y, 0));
            }
        }
        else
        {
            if (!firstClick)
            {
                firstClick = true;
                if (positions.Count>0)
                {
                    drawing = false;
                    body.useGravity = true;
                }
            }
            
        }
    }
}
