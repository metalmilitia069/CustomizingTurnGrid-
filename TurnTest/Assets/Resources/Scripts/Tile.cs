using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isWalkable = true;
    public bool isCurrent = false;
    public bool isTarget = false;
    public bool isSelectable = false;

    public bool isLatter = false;

    public List<Tile> listOfNearbyValidTiles;

    public int jumpHeight = 100;



    private Tile referenceTile;


    public bool isVisited = false;
    public int distance = 0;

    public Tile parent = default;    

    // Start is called before the first frame update
    void Start()
    {
        TacticsBaseTileCalculation.ssa += UnhideTiles;
        //TacticsBaseTileCalculation.ssa += NoFlipFlop;
        GridManager.EventScanTilesUpdate += ScanTiles;

        ScanTiles();
        GridManager.instance.listOfTiles.Add(this);
    }

    public void UnhideTiles()
    {        
        this.gameObject.SetActive(true);
    }

    //public bool no = false;
    //public void NoFlipFlop()
    //{
    //    no = !no;
    //}
    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(transform.position, transform.up);

        if (isCurrent)
        {
            //this.gameObject.SetActive(true);
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (isTarget)
        {
            //this.gameObject.SetActive(true);
            GetComponent<Renderer>().material.color = Color.cyan;
        }
        else if (isSelectable)
        {
            //this.gameObject.SetActive(true);
            //if (no)
            //{
            //    return;
            //}
            GetComponent<Renderer>().material.color = Color.blue;
            if (isLatter)
            {
                GetComponent<Renderer>().material.color = Color.yellow;
            }
            
        }
        else
        {            
            GetComponent<Renderer>().material.color = Color.white;
            this.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        //Gizmos.DrawWireCube(transform.position + Vector3.forward, new Vector3(transform.localScale.x / 2, transform.localScale.y * jumpHeight, transform.localScale.z / 2));
        //Gizmos.DrawWireCube(transform.position - Vector3.forward, new Vector3(transform.localScale.x / 2, transform.localScale.y * jumpHeight, transform.localScale.z / 2));
        //Gizmos.DrawWireCube(transform.position + Vector3.right, new Vector3(transform.localScale.x / 2, transform.localScale.y * jumpHeight, transform.localScale.z / 2));
        //Gizmos.DrawWireCube(transform.position - Vector3.right, new Vector3(transform.localScale.x / 2, transform.localScale.y * jumpHeight, transform.localScale.z / 2));

        Gizmos.DrawWireCube(transform.position + Vector3.forward, new Vector3(transform.localScale.x / 2, jumpHeight, transform.localScale.z / 2));
        Gizmos.DrawWireCube(transform.position - Vector3.forward, new Vector3(transform.localScale.x / 2, jumpHeight, transform.localScale.z / 2));
        Gizmos.DrawWireCube(transform.position + Vector3.right, new Vector3(transform.localScale.x / 2, jumpHeight, transform.localScale.z / 2));
        Gizmos.DrawWireCube(transform.position - Vector3.right, new Vector3(transform.localScale.x / 2, jumpHeight, transform.localScale.z / 2));
    }

    public void ResetTileData()
    {
        isCurrent = false;
        isTarget = false;
        isSelectable = false;

        listOfNearbyValidTiles.Clear();

        isVisited = false;

        referenceTile = default;


        distance = 0;

        parent = null;

        isLatter = default;
        
    }

    public void ScanTiles()
    {
        ResetTileData();

        GatherNearbyTiles(Vector3.forward);
        GatherNearbyTiles(Vector3.back);
        GatherNearbyTiles(Vector3.left);
        GatherNearbyTiles(Vector3.right);

        //CheckTileType();
    }

    public void GatherNearbyTiles(Vector3 direction)
    {
        Vector3 halfExtends = new Vector3(0.25f, jumpHeight, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtends);
       
        foreach (var col in colliders)
        {  
            
            referenceTile = col.GetComponent<Tile>();
            if (referenceTile) //&& referenceTile.isWalkable)
            {                
                RaycastHit hit;
                if (!Physics.Raycast(referenceTile.transform.position, Vector3.up, out hit, 1))
                //if (Physics.OverlapBox(transform.position, halfExtends) == null)                
                {
                    listOfNearbyValidTiles.Add(referenceTile);
                }
                //else
                //{
                //    //Debug.Log("ai dentu");
                //    //Latter latter = 
                //    if (hit.transform.GetComponent<Latter>())
                //    {
                //        //Debug.Log("MOZO");
                //        listOfNearbyValidTiles.Add(referenceTile);
                //        //isLatter = true;
                //    }

                //}


                //else if (hit.collider.GetComponent<Latter>())
                //{

                //}
            }            
        }

    }

    public void CheckTileType()
    {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, Vector3.down, out hit, 1))
        //if (Physics.OverlapBox(transform.position, halfExtends) == null)                
        {
            Latter latter = hit.transform.GetComponent<Latter>();

            if (hit.transform.GetComponent<Latter>())
            {
                isLatter = true;
                Collider[] colliders = Physics.OverlapBox(transform.position + latter.direction, latter.latterHeight);
                foreach (var col in colliders)
                {
                    referenceTile = col.GetComponent<Tile>();
                    if (referenceTile)
                    {
                        listOfNearbyValidTiles.Add(referenceTile);
                    }
                }
            }
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    Latter latter = other.GetComponent<Latter>();
    //    if (latter)
    //    {
    //        Debug.Log("MOZO");
    //        Debug.Log(latter.direction);

    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    Latter latter = other.GetComponent<Latter>();
    //    if (latter)
    //    {
    //        Debug.Log("MOZO");
    //        Debug.Log(latter.direction);

    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("MOZO");
    //    Latter latter = collision.gameObject.GetComponent<Latter>();
    //    if (latter)
    //    {
    //        Debug.Log("MOZO");
    //        Debug.Log(latter.direction);

    //    }
    //}
}
