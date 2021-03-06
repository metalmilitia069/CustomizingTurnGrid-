﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTileCalculation : TacticsBaseTileCalculation
{
    //private bool isInited = false;
    // Start is called before the first frame update
    void Start()
    {
        //Init();
        halfHeight = GetComponent<Collider>().bounds.extents.y;        
    }   

    // Update is called once per frame
    void Update()
    {
        //if (!isInited)
        //{
        //    Init();
        //    isInited = true;
        //    Debug.Log("INITIATED????????????????????");
        //}

        if (!isMoving)
        {
            //FindSelectableTiles();
            CheckMouse();
        }
        else
        {            
            Move();            
        }

        if (!isTilesFound)
        {
            //FindSelectableTiles();
            GridManager.instance.CalculateAvailablePath(this.gameObject);
        }
    }

    public void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile")
                {
                    Tile t = hit.collider.GetComponent<Tile>();

                    if (t.isSelectable)
                    {
                        PathToDesignatedTile(t);
                    }
                }
            }
        }
    }
}
