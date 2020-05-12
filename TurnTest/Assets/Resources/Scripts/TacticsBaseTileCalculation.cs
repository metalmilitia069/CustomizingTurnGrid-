using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsBaseTileCalculation : MonoBehaviour
{
    [SerializeField]
    private List<Tile> selectableTiles = new List<Tile>();
    [SerializeField]
    private GameObject[] tiles;
    [SerializeField]
    private Tile currentTile;

    [SerializeField]
    private int _movePoints = 5;
    protected void Init()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        

    }

    public void FindSelectableTiles()
    {
        GetCurrentTile();

        //BFS Algorithm
        var process = new Queue<Tile>();

        process.Enqueue(currentTile);
        currentTile.isVisited = true;

        while (process.Count > 0)
        {            
            Tile t = process.Dequeue();

            selectableTiles.Add(t);
            t.isSelectable = true;

            if (t.distance < _movePoints)
            {               
                foreach (var tile in t.listOfDetectedTiles)
                {
                   
                    if (!tile.isVisited)
                    {
                        tile.isVisited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                        
                    }
                }
            }

        }
    }

    public void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.isCurrent = true;
    }

    public Tile GetTargetTile(GameObject gameObject)
    {
        RaycastHit hit;
        Tile tilePlaceHolder = default;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            tilePlaceHolder = hit.collider.GetComponent<Tile>();
        }

        return tilePlaceHolder;
    }
}
