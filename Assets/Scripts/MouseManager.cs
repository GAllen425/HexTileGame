using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

    public Unit selectedUnit;

    public Map map;
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
                // get the hex the player stands on before moving
                selectedUnit.GetCurrentHex();
                Hex ourCurrentObject = selectedUnit.currentHex;
                Debug.Log(ourCurrentObject.name);

                // set destination - unit handles movement
                map.GeneratePathTo(ourHitObject.GetComponent<Hex>().x, ourHitObject.GetComponent<Hex>().y, selectedUnit);

                // update the object to say we arent standing here anymore
                ourCurrentObject.occupied = false;
                //ourCurrentObject.GetComponentInChildren<MeshRenderer>().material.color = Color.white;

                ourHitObject.GetComponent<Hex>().occupied = true;
            }
        }

        // TODO highlighting tiles incorrectly
        /*if (selectedUnit != null)
        {
                normalColor = mr.material.color;
                ourHitObject.GetComponent<Hex>().OnMouseEnter(
                    ourHitObject, selectedUnit, mr, normalColor, highlightColor);
                highlightObject.GetComponent<Hex>().OnMouseExit(
                    ourHitObject, selectedUnit, mr, normalColor);
        }*/

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
