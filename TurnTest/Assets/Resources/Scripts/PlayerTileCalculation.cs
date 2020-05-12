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

    // Update is called once per frame
    void Update()
    {
        if (!isInited)
        {
            Init();
            isInited = true;            
        }

        if (!isTilesFound)
        {
            FindSelectableTiles();
        }
    }
}
