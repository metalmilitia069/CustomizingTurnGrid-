using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Tile Data")]
    public List<Tile> listOfTiles;
    public List<Tile> listOfSelectableTiles;

    public Tile tilePlaceholder;

    public delegate void OnScanTiles();
    public static event OnScanTiles EventScanTilesUpdate;

    #region Singleton

    public static GridManager instance = null;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Theres More than One GridManager in the Scene!!!");
            Destroy(this.gameObject);
        }

        instance = this;

        //DontDestroyOnLoad(this.gameObject);        
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScannedTiles()
    {
        EventScanTilesUpdate();
    }

    public void GetCurrentTile(GameObject characterStandingOnTile)
    {
        RaycastHit hit;
        //Tile tilePlaceholder;

        LayerMask mask = LayerMask.GetMask("TileLayer");

        if (Physics.Raycast(characterStandingOnTile.transform.position, Vector3.down, out hit, 1))
        {            
            tilePlaceholder = hit.collider.transform.GetComponent<Tile>();
            if (tilePlaceholder)
            {
                tilePlaceholder.isCurrent = true;
                Debug.Log("mozovos, carai");
            }
        }
    }

    public void CalculateAvailablePath(GameObject character)
    {
        UpdateScannedTiles();
        GetCurrentTile(character);
        TacticsBaseTileCalculation baseCharacter = character.GetComponent<TacticsBaseTileCalculation>();
        //GetCurrentTile();

        //BFS Algorithm
        var queueProcess = new Queue<Tile>();

        queueProcess.Enqueue(tilePlaceholder);
        tilePlaceholder.isVisited = true;

        while (queueProcess.Count > 0)
        {
            Tile t = queueProcess.Dequeue();

            listOfSelectableTiles.Add(t);
            t.isSelectable = true;

            if (t.distance < baseCharacter._movePoints) 
            {
                foreach (var tile in t.listOfNearbyValidTiles)
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
        baseCharacter.currentTile = tilePlaceholder;
        baseCharacter.isTilesFound = true;
    }


}
