using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArc : MonoBehaviour
{
    Transform Player;
    GameManager gm;
   public Transform cross;
   public GameObject bullet;
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Transform>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

   public void attack(Vector3 target)
    {
      
        GameObject b = Instantiate(bullet);
        b.transform.position = cross.transform.position;
        b.GetComponent<Bullet>().target = target;
      //  Debug.Log("arc fire");
    }
    void Update()
    {
     

    }
}
