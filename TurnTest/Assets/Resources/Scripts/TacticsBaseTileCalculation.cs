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


    [SerializeField]
    private Stack<Tile> _stackPath = new Stack<Tile>();
    public bool isMoving = false;

    //new
    protected bool isTilesFound = false;

    protected void Init()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        

    }

    public void FindSelectableTiles()
    {
        GetCurrentTile();

        //BFS Algorithm
        var queueProcess = new Queue<Tile>();

        queueProcess.Enqueue(currentTile);
        currentTile.isVisited = true;

        while (queueProcess.Count > 0)
        {            
            Tile t = queueProcess.Dequeue();

            selectableTiles.Add(t);
            t.isSelectable = true;

            if (t.distance < _movePoints)
            {               
                foreach (var tile in t.listOfDetectedTiles)
                {                   
                    if (!tile.isVisited)
                    {
                        tile.parent = t;
                        tile.isVisited = true;
                        tile.distance = 1 + t.distance;
                        queueProcess.Enqueue(tile);                        
                    }
                }
            }
        }
        isTilesFound = true;
    }

    public void GetCurrentTile()
    {
        currentTile = GetOccupiedTileByTheUnit(gameObject);
        currentTile.isCurrent = true;
    }

    public Tile GetOccupiedTileByTheUnit(GameObject gameObject)
    {
        RaycastHit hit;
        Tile tilePlaceHolder = default;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            tilePlaceHolder = hit.collider.GetComponent<Tile>();
        }

        return tilePlaceHolder;
    }

    public void MoveToDesignatedTile(Tile tile)
    {
        tile.isTarget = true;
        isMoving = true;
        _stackPath.Clear();

        Tile next = tile;
        while (!next)
        {
            _stackPath.Push(next);
            next = next.parent;
        }
        
    }
}
