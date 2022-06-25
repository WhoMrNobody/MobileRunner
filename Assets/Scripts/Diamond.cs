using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    void FixedUpdate()
    {

        transform.Rotate(0f, Random.Range(2, 10), 0f);
        
        
    }
}
