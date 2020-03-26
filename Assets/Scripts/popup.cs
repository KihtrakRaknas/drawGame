using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popup : MonoBehaviour
{
    bool active = true;
    public GameObject thePopup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            active = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            active = false;
        }
        thePopup.SetActive(active);
    }
}
