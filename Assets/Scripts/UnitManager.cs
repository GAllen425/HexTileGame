using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitManager : MonoBehaviour {

    public Map map;
    public GameObject playerPrefab;
    public GameObject unit_go;

    public Text text;

    // Use this for initialization
    void Start () {
        unit_go = (GameObject)Instantiate(playerPrefab, Map.HexTileToVector3(0, 2) + new Vector3(0, 0.1f, 0), Quaternion.identity);
        unit_go.GetComponent<Unit>().hexX = 0;
        unit_go.GetComponent<Unit>().hexY = 2;
        unit_go.GetComponent<Unit>().map = map;
    }

    // Update is called once per frame
    void Update () {
        text.text = "Movement left: " + unit_go.GetComponent<Unit>().movementTurn;
	}

    public void EndTurn()
    {
        unit_go.GetComponent<Unit>().RefreshMovementSpeed();
    }
}
