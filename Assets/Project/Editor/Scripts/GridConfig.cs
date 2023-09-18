using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GridConfig
{
    [SerializeField]
    [Range(2, 5)] private int gridSideSize = 3;
    [SerializeField]
    [Range(11, 16)] private float cellSpacing;

    public int GridSideSize => gridSideSize;
    public float CellSpacing => cellSpacing;

    [field:SerializeField] public Transform GridTransform { get; private set; }
    [field:SerializeField] public GameObject PrefabCell { get; private set; }
}
