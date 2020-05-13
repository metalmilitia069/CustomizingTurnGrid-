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

    public List<Tile> listOfDetectedTiles;

    public int jumpHeight = 100;



    private Tile referenceTile;


    public bool isVisited = false;
    public int distance = 0;

    public Tile parent = default;

    public event EventHandler sdsa;

    // Start is called before the first frame update
    void Start()
    {
        //MyCollisions();
        //this.gameObject.SetActive(true);
        ScanTiles();
    }

    void FixedUpdate()
    {
        //MyCollisions();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCurrent)
        {
            //this.gameObject.SetActive(true);
            GetComponent<Renderer>().material.color = Color.magenta;
        }
        else if (isTarget)
        {
            //this.gameObject.SetActive(true);
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (isSelectable)
        {
            //this.gameObject.SetActive(true);
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {            
            GetComponent<Renderer>().material.color = Color.white;
            //this.gameObject.SetActive(false);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.magenta;

    //    Gizmos.DrawWireCube(transform.position + Vector3.forward, new Vector3(transform.localScale.x/2, transform.localScale.y * jumpHeight, transform.localScale.z/2));
    //    Gizmos.DrawWireCube(transform.position - Vector3.forward, new Vector3(transform.localScale.x / 2, transform.localScale.y * jumpHeight, transform.localScale.z / 2));
    //    Gizmos.DrawWireCube(transform.position + Vector3.right, new Vector3(transform.localScale.x / 2, transform.localScale.y * jumpHeight, transform.localScale.z / 2));
    //    Gizmos.DrawWireCube(transform.position - Vector3.right, new Vector3(transform.localScale.x / 2, transform.localScale.y * jumpHeight, transform.localScale.z / 2));
    //}

    public void ResetTileData()
    {
        isCurrent = false;
        isTarget = false;
        isSelectable = false;

        //listOfDetectedTiles.Clear();

        isVisited = false;

        referenceTile = default;


        distance = 0;

        parent = null;
        
    }

    public void ScanTiles()
    {
        ResetTileData();

        GatherNearbyTiles(Vector3.forward);
        GatherNearbyTiles(Vector3.back);
        GatherNearbyTiles(Vector3.left);
        GatherNearbyTiles(Vector3.right);

        //
        //this.gameObject.SetActive(false);
    }

    public int counnnt = 0;

    public void GatherNearbyTiles(Vector3 direction)
    {
        Vector3 halfExtends = new Vector3(0.25f, jumpHeight, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtends);
        
        foreach (var col in colliders)
        {            
            referenceTile = col.GetComponent<Tile>();
            if (referenceTile && referenceTile.isWalkable)
            {                
                RaycastHit hit;
                if (!Physics.Raycast(referenceTile.transform.position, Vector3.up, out hit, 1))
                {
                    listOfDetectedTiles.Add(referenceTile);
                }
            }            
        }

    }

    //void MyCollisions()
    //{
    //    //Use the OverlapBox to detect if there are any other colliders within this box area.
    //    //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
    //    Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale/2);//, Quaternion.identity, m_LayerMask);
    //    int i = 0;
    //    //Check when there is a new collider coming into contact with the box
    //    while (i < hitColliders.Length)
    //    {
    //        //Output all of the collider names
    //        Debug.Log("Hit : " + hitColliders[i].name + i);
    //        //Increase the number of Colliders in the array
    //        i++;
    //    }
    //}
}
