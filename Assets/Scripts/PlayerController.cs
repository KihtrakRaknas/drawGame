using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        finished = false;
    }
    // Update is called once per frame
    void Update()
    {
        float z = transform.rotation.eulerAngles.z;
        if (LineTest.drawing == true || finished == true)
        {
            z = 0;
            body.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
        }
        else
        {
            body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
        }
        
        if(finished)
            body.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
        else
            transform.rotation = Quaternion.Euler(0, 0, z);

    }

    private void FixedUpdate()
    {
        if (LineTest.drawing == false && finished == false) { 
            float xVel = -Input.GetAxis("Horizontal");
            float xFinal = xVel * Time.deltaTime * 6;
            if ((body.angularVelocity.z >= 0 && xFinal <= 0) || (body.angularVelocity.z <= 0 && xFinal >= 0))
                body.angularVelocity = new Vector3(body.angularVelocity.x, body.angularVelocity.y, body.angularVelocity.z / (60 * Time.deltaTime));
            body.angularVelocity = new Vector3(body.angularVelocity.x, body.angularVelocity.y, body.angularVelocity.z + xFinal);
            if (Mathf.Abs(body.angularVelocity.z) >= 10)
                body.angularVelocity = new Vector3(body.angularVelocity.x, body.angularVelocity.y, 5 * body.angularVelocity.z / Mathf.Abs(body.angularVelocity.z));
        }
        else
        {
            body.angularVelocity = new Vector3(0, 0, 0);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        print("COLL");
        GameObject otherG = other.gameObject;
        MeshRenderer mesh = otherG.GetComponent<MeshRenderer>();
        Material mat = mesh.material;
        print("\""+mat.name+ "\"");
        if(mat.name.Contains("Death"))
        {
            GetComponent<LineTest>().Restart();
        }
        else if (mat.name.Contains("Finish"))
        {
            print(this.transform.position);
            StartNextLevelAnim();
        }
    }
    public static bool finished = false;
    void StartNextLevelAnim()
    {
        if (!finished)
        {
            finished = true;
            GameObject race = Resources.Load("Eraser") as GameObject;
            if (race == null)
            {
                Debug.Log("IT'S NULL!!!");
            }
            GameObject newRace = Instantiate(race, this.transform.position+new Vector3(0,30,0), Quaternion.identity);
            newRace.transform.localScale = new Vector3(350, 350, 350);
            newRace.transform.localRotation = Quaternion.Euler(0, 90, -90);
            Rigidbody raceRig = newRace.AddComponent<Rigidbody>();
            raceRig.useGravity = true;
            Invoke("clear", 2);
            Invoke("switchLevel", 3);
            //finished = false;
        }
    }
    void switchLevel()
    {
        int currLevel = int.Parse(SceneManager.GetActiveScene().name.Substring(5));
        SceneManager.LoadScene("Level" + (currLevel + 1), LoadSceneMode.Single);
    }
    void clear()
    {
        GetComponent<LineTest>().Reset();
    }
    
}
