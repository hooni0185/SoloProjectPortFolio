using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMoving : MonoBehaviour
{
    public float MvSpd;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += new Vector3(MvSpd * Time.deltaTime, 0, 0);
            //transform.Translate(new Vector3(1*Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition += new Vector3(-MvSpd * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.localPosition += new Vector3(0, 0, MvSpd*Time.deltaTime);
            
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition += new Vector3(0, 0, -MvSpd * Time.deltaTime);
        }
        if (transform.localPosition.x <= -1)
        {
            transform.localPosition = new Vector3(-1f, transform.localPosition.y, transform.localPosition.z);
        }
        if (transform.localPosition.x >= 6f)
        {
            transform.localPosition = new Vector3(6f, transform.localPosition.y, transform.localPosition.z);
        }
        if (transform.localPosition.z >= 3)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 3f);
        }
        if (transform.localPosition.z <= -8)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -8f);
        }
    }
}


//0,10,3 // 6,10,3 // 6,10,-6 // 0, 10, -6