using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{

    public GameObject tilePrefab;
    public GameObject[] startingPointPrefab;



    public int[] row;
    public int[] collumn;
    //GameObject dsa = GameObject.CreatePrimitive(Plane).transform.
    // Start is called before the first frame update

    //[System.Serializable]
    //public struct GridKit
    //{
    //    public int[] row;
    //    public int[] collumn;

    //}
    //[SerializeField]
    //public GridKit gridKit;

    void Start()
    {
        //float nextCoord = tilePrefab.transform.localScale.x;
        //gridKit = new GridKit();
        //float nextCoordY = 0;
        int gridIndex = 0;
        //int collumnIndex = 0;
        foreach (var startingPoint in startingPointPrefab)
        {
            float nextCoordY = startingPoint.transform.position.z + (tilePrefab.transform.localScale.z / 2);
            for (int i = 0; i < row[gridIndex]; i++)
            {
                float nextCoordX = startingPoint.transform.position.x + (tilePrefab.transform.localScale.x / 2);

                for (int j = 0; j < collumn[gridIndex]; j++)
                {
                    //float nextCoord = tilePrefab.transform.localScale.x;                
                    Instantiate(tilePrefab, new Vector3(nextCoordX, startingPoint.transform.position.y, nextCoordY), Quaternion.identity).transform.parent = this.transform;
                    nextCoordX += tilePrefab.transform.localScale.x;
                }
                nextCoordY += tilePrefab.transform.localScale.z;
            }
            gridIndex++;
        }
        //float nextCoordY = startingPointPrefab.transform.position.z + (tilePrefab.transform.localScale.z / 2);
        //for (int i = 0; i < row; i++)
        //{
        //    float nextCoordX = startingPointPrefab.transform.position.x + (tilePrefab.transform.localScale.x / 2);
            
        //    for (int j = 0; j < collumn; j++)
        //    {
        //        //float nextCoord = tilePrefab.transform.localScale.x;                
        //        Instantiate(tilePrefab, new Vector3(nextCoordX, 0.0f, nextCoordY), Quaternion.identity).transform.parent = this.transform ;
        //        nextCoordX += tilePrefab.transform.localScale.x;
        //    }
        //    nextCoordY += tilePrefab.transform.localScale.z;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
