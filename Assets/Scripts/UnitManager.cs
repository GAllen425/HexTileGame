using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {

    public GameObject playerPrefab;
    // Use this for initialization
    void Start () {
        GameObject unit_go = (GameObject)Instantiate(playerPrefab, new Vector3(0, 0.1f, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
