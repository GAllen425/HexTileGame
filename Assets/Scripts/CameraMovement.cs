using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float panSpeed = 5f;
    public float panBorderThickness = 10.0f;
    public Vector2 panLimit;
	// Update is called once per frame
	void Update () {

        Vector3 pos = transform.position;

        if(Input.mousePosition.y >= Screen.height- panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if(Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if(Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if(Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);
        transform.position = pos;
		
	}
}
