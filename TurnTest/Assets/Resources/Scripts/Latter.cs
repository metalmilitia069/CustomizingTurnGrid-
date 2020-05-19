using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Latter : MonoBehaviour
{
    public Vector3 direction;
    public bool isForward;
    public bool isBackward;
    public bool isRight;
    public bool isLeft;

    public Vector3 latterHeight;

    // Start is called before the first frame update
    void Start()
    {
        if (isForward)
        {
            direction = Vector3.forward;
            return;
        }
        if (isBackward)
        {
            direction = Vector3.back;
            return;
        }
        if (isRight)
        {
            direction = Vector3.right;
            return;
        }
        if (isLeft)
        {
            direction = Vector3.left;
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
