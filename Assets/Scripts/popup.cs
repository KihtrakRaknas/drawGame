using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popup : MonoBehaviour
{
    bool active = true;
    public GameObject thePopup;
    public GameObject cameraObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            thePopup.transform.position = new Vector3(cameraObj.transform.position.x, cameraObj.transform.position.y + 1.2f, thePopup.transform.position.z) ;
            active = true;
            Debug.Log("here");
        }
        if (Input.GetMouseButtonUp(0))
        {
            active = false;
        }
        if (active)
        {
            thePopup.SetActive(true);
        }
        else
        {
            thePopup.SetActive(false);
        }
    }
}
