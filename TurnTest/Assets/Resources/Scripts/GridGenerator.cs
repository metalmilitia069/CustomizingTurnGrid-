﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{

    public GameObject tilePrefab;
    public GameObject[] startingPointPrefab;

    public int[] row;
    public int[] collumn;    

    void Start()
    {        
        int gridIndex = 0;
        
        foreach (var startingPoint in startingPointPrefab)
        {
            float nextCoordY = startingPoint.transform.position.z + (tilePrefab.transform.localScale.z / 2);
            for (int i = 0; i < row[gridIndex]; i++)
            {
                float nextCoordX = startingPoint.transform.position.x + (tilePrefab.transform.localScale.x / 2);

                for (int j = 0; j < collumn[gridIndex]; j++)
                {                    
                    Instantiate(tilePrefab, new Vector3(nextCoordX, startingPoint.transform.position.y, nextCoordY), Quaternion.identity).transform.parent = this.transform;
                    nextCoordX += tilePrefab.transform.localScale.x;
                }
                nextCoordY += tilePrefab.transform.localScale.z;
            }
            gridIndex++;
        }       
    }
}
