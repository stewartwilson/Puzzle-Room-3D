using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject cameraCenter;
	// Use this for initialization
	void Start () {
        if (cameraCenter == null)
        {
            cameraCenter = GameObject.Find("CameraCenter");
        }
        if(cameraCenter != null)
        {
            transform.position = new Vector3(cameraCenter.transform.position.x,cameraCenter.transform.position.y,transform.position.z);
        }
	}
}
