using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

    public Unit selectedUnit;
    Color normalColor;
    Color highlightColor = Color.red;

    GameObject ourHitObject;
    GameObject highlightObject;

    void Start()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.transform.parent.gameObject != ourHitObject)
            {
                highlightObject = ourHitObject;
                ourHitObject = hitInfo.collider.transform.parent.gameObject;
            }

            if (ourHitObject.GetComponent<Hex>() != null)
            {
                MouseOver_Hex(ourHitObject);
            }
            else if (ourHitObject.GetComponent<Unit>() != null)
            {
                MouseOver_Unit(ourHitObject);
            }
        }
    }

    void MouseOver_Hex(GameObject ourHitObject)
    {
        //Debug.Log("Mouse position" + Input.mousePosition);
        MeshRenderer mr = ourHitObject.GetComponentInChildren<MeshRenderer>();

        if (Input.GetMouseButtonDown(0))
        {
            /*if (mr.material.color == Color.red)
            {
                mr.material.color = Color.white;
            }
            else
            { 
                mr.material.color = Color.red;
            }*/

            if (selectedUnit != null && ourHitObject.GetComponent<Hex>().occupied == false)
            {
                selectedUnit.GetCurrentHex();
                Hex ourCurrentObject = selectedUnit.currentHex;
                Debug.Log(ourCurrentObject.name);

                selectedUnit.destination = ourHitObject.transform.position + new Vector3(0,0.1f,0);

                ourCurrentObject.occupied = false;
                ourCurrentObject.GetComponentInChildren<MeshRenderer>().material.color = Color.white;

                ourHitObject.GetComponent<Hex>().occupied = true;
            }
        }

        // TODO highlighting tiles incorrectly
        if (selectedUnit != null)
        {
                normalColor = mr.material.color;
                ourHitObject.GetComponent<Hex>().OnMouseEnter(
                    ourHitObject, selectedUnit, mr, normalColor, highlightColor);
                highlightObject.GetComponent<Hex>().OnMouseExit(
                    ourHitObject, selectedUnit, mr, normalColor);
        }

    }

    void MouseOver_Unit(GameObject ourHitObject)
    {
        //Debug.Log("Mouse position" + Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            selectedUnit = ourHitObject.GetComponent<Unit>();

            MeshRenderer mr = ourHitObject.GetComponentInChildren<MeshRenderer>();
            if (mr.material.color == Color.cyan)
            {
                mr.material.color = Color.green;
                selectedUnit = null;
            }
            else
            {
                mr.material.color = Color.cyan;
            }
        }
    }

}
