using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isWalkable = false;
    public bool isCurrent = false;
    public bool isTarget = false;
    public bool isSelectable = false;

    // Start is called before the first frame update
    void Start()
    {
        MyCollisions();
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
            GetComponent<Renderer>().material.color = Color.magenta;
        }
        else if (isTarget)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (isSelectable)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        Gizmos.DrawWireCube(transform.position + Vector3.forward, new Vector3(transform.localScale.x/2, transform.localScale.y, transform.localScale.z/2));
        Gizmos.DrawWireCube(transform.position - Vector3.forward, new Vector3(transform.localScale.x / 2, transform.localScale.y, transform.localScale.z / 2));
        Gizmos.DrawWireCube(transform.position + Vector3.right, new Vector3(transform.localScale.x / 2, transform.localScale.y, transform.localScale.z / 2));
        Gizmos.DrawWireCube(transform.position - Vector3.right, new Vector3(transform.localScale.x / 2, transform.localScale.y, transform.localScale.z / 2));
    }

    void MyCollisions()
    {
        //Use the OverlapBox to detect if there are any other colliders within this box area.
        //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale/2);//, Quaternion.identity, m_LayerMask);
        int i = 0;
        //Check when there is a new collider coming into contact with the box
        while (i < hitColliders.Length)
        {
            //Output all of the collider names
            Debug.Log("Hit : " + hitColliders[i].name + i);
            //Increase the number of Colliders in the array
            i++;
        }
    }
}
