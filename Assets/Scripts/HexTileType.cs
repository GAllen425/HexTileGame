﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HexTileType {

    public string name;
    public GameObject tileVisualPrefab;
    public string tileTerrain;
    public bool isWalkable;

    public float tileMovementCost = 1;
}
