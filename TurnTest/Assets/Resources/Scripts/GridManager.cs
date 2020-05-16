using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Tile Data")]
    public List<Tile> ListOfTiles;

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


}
