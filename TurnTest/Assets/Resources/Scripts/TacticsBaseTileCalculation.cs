﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsBaseTileCalculation : MonoBehaviour
{
    [SerializeField]
    private List<Tile> selectableTiles = new List<Tile>();
    //[SerializeField]
    //private GameObject[] tiles;
    [SerializeField]
    public Tile currentTile;

    [SerializeField]
    public int _movePoints = 5;


    [SerializeField]
    private Stack<Tile> _stackPath = new Stack<Tile>();
    public bool isMoving = false;
    protected float halfHeight = 0;

    private Vector3 _velocity = new Vector3();
    private Vector3 _heading = new Vector3();
    public float moveSpeed = 2;



    public bool turn = false;

    //new
    public bool isTilesFound = false;

    public delegate void OnMovementEnds();
    public static event OnMovementEnds ssa;

    //protected void Init()
    //{
    //    //tiles = GameObject.FindGameObjectsWithTag("Tile");
    //}
    
    //public void ComputeAdjacencyList()
    //{
    //    //tiles = GameObject.FindGameObjectsWithTag("Tile");  //USE HERE IF YOUR TILE MAP CHANGES OFTEN

    //    foreach (var tile in tiles)
    //    {
    //        Tile t = tile.GetComponent<Tile>();
    //        t.ScanTiles();
    //    }

    //}

    //public void FindSelectableTiles()
    //{

    //    //000000000000000000000000000000000000000000000000
    //    //ComputeAdjacencyList();
    //    GridManager.instance.UpdateScannedTiles();
    //    GridManager.instance.GetCurrentTile(this.gameObject);
    //    //GetCurrentTile();
    //    currentTile = GridManager.instance.tilePlaceholder;
    //    //BFS Algorithm
    //    var queueProcess = new Queue<Tile>();

    //    queueProcess.Enqueue(currentTile);
    //    currentTile.isVisited = true;

    //    while (queueProcess.Count > 0)
    //    {            
    //        Tile t = queueProcess.Dequeue();

    //        selectableTiles.Add(t);
    //        t.isSelectable = true;

    //        if (t.distance < _movePoints)
    //        {               
    //            foreach (var tile in t.listOfNearbyValidTiles)
    //            {                   
    //                if (!tile.isVisited)
    //                {
    //                    tile.parent = t;
    //                    tile.isVisited = true;
    //                    tile.distance = 1 + t.distance;
    //                    queueProcess.Enqueue(tile);                        
    //                }
    //            }
    //        }
    //    }
    //    isTilesFound = true;

    //}

    //public void GetCurrentTile()
    //{
    //    currentTile = GetOccupiedTileByTheUnit(gameObject);
    //    currentTile.isCurrent = true;
    //}

    //public Tile GetOccupiedTileByTheUnit(GameObject gameObject)
    //{
    //    RaycastHit hit;
    //    Tile tilePlaceHolder = default;
    //    if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
    //    {
    //        tilePlaceHolder = hit.collider.GetComponent<Tile>();
    //    }

    //    return tilePlaceHolder;
    //}

    public void PathToDesignatedTile(Tile tile)
    {
        tile.isTarget = true;
        isMoving = true;
        _stackPath.Clear();

        Tile next = tile;
        while (next != null)
        {
            //next.no = true;
            //next.GetComponent<Renderer>().material.color = Color.yellow;
            _stackPath.Push(next);
            next = next.parent;
        }        
    }

    public void Move()
    {
        if (_stackPath.Count > 0)
        {            
            Tile t = _stackPath.Peek();
            Vector3 destinationCoordinates = t.transform.position;

            destinationCoordinates.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, destinationCoordinates) >= 0.05f)
            {
                bool jump = (transform.position.y != destinationCoordinates.y);
                jump = false; // DELETE WHEN IMPLEMENTING JUMP!!!!!!!!!
                if (jump)
                {
                    //TODO
                }
                else
                {
                    CalculatePathTowardsTheCharacter(destinationCoordinates);
                    SetHorizontalVelocity();
                }               
                transform.forward = _heading;
                transform.position += _velocity * Time.deltaTime;
            }
            else
            {
                transform.position = destinationCoordinates;
                _stackPath.Pop();
            }
        }
        else
        {
            RemoveSelectableTiles();
            ssa();
            isMoving = false;
            isTilesFound = false;            

            //TurnManager.EndTurn();
        }
    }

    protected void RemoveSelectableTiles()
    {
        if (currentTile != null)
        {
            currentTile.isCurrent = false;
            currentTile = null;
        }

        foreach (var tile in selectableTiles)
        {
            tile.ResetTileData();
        }

        selectableTiles.Clear();
    }

    public void CalculatePathTowardsTheCharacter(Vector3 destination)
    {
        _heading = destination - transform.position;
        _heading.Normalize();
    }

    public void SetHorizontalVelocity()
    {
        _velocity = _heading * moveSpeed;
    }

    public void Jump(Vector3 target)
    {

    }

    public void PrepareJump(Vector3 target)
    {

    }

    public void FallDownward(Vector3 target)
    {

    }

    public void JumpUpward(Vector3 target)
    {

    }

    public void MoveToEdge()
    {

    }

    public void BeginTurn()
    {
        turn = true;
    }

    public void EndTurn()
    {
        turn = false;
    }
}
