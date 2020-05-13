using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTileCalculation : TacticsBaseTileCalculation
{
    private bool isInited = false;
    // Start is called before the first frame update
    void Start()
    {
        //Init();
    }

    public delegate void mozo();
    public static event mozo ssa;

    // Update is called once per frame
    void Update()
    {
        if (!isInited)
        {
            Init();
            isInited = true;            
        }

        if (!isMoving)
        {
            //ssa();
            FindSelectableTiles();
            CheckMouse();
        }
        else
        {
            //Debug.Log("isMoving? " + isMoving);
            Move();
            //isTilesFound = false;
            
        }

        //if (!isTilesFound)
        //{
        //    FindSelectableTiles();
        //}
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
                        MoveToDesignatedTile(t);
                    }
                }
            }
        }
    }
}
