using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{

    public GameObject tilePrefab;
    public GameObject startingPointPrefab;

    public int row;
    public int collumn;
    //GameObject dsa = GameObject.CreatePrimitive(Plane).transform.
    // Start is called before the first frame update
    void Start()
    {
        //float nextCoord = tilePrefab.transform.localScale.x;

        //float nextCoordY = 0;
        float nextCoordY = startingPointPrefab.transform.position.z + (tilePrefab.transform.localScale.z / 2);
        for (int i = 0; i < row; i++)
        {
            float nextCoordX = startingPointPrefab.transform.position.x + (tilePrefab.transform.localScale.x / 2);
            
            for (int j = 0; j < collumn; j++)
            {
                //float nextCoord = tilePrefab.transform.localScale.x;                
                Instantiate(tilePrefab, new Vector3(nextCoordX, 0.0f, nextCoordY), Quaternion.identity).transform.parent = this.transform ;
                nextCoordX += tilePrefab.transform.localScale.x;
            }
            nextCoordY += tilePrefab.transform.localScale.z;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
