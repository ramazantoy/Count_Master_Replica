using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
     
    public Vector3 target;
    void Start()
    {
        Invoke("gg", 1.0f);
      //Debug.Log("bullet start");
    }
     
   
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, target, 3f * Time.deltaTime);
    }

    void gg()
    {
        Destroy(gameObject);
    }
}
