using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    GameManager gm;
    bool attackTower;
   public static bool attackBoss;
    public static bool towerDone;
Vector3 target;
   
    void Start()
    {
        attackTower = false;
        attackBoss = false;
        towerDone = false;
    }
    private void OnEnable()//setactive true oldugunda çalışır
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        attackTower = false;
    }
    private void OnDisable()// set active false olduğunda çalışır
    {
        //GameManager.child_sayac--;
       if (GameManager.child_sayac <1)
        {
            //Debug.Log("Child Game Over");
            gm.gameOver();
        }
    }
    public void SetTarget(Vector3 target)
    {
        this.target = target;
        attackTower = true;

    }
    public  void  setBossTarget(Vector3 target)
    {
        attackBoss = true;
        this.target = target;
    }

    public void towerAt(bool attack)
    {
       attackTower = attack;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            other.gameObject.SetActive(false);
            GameManager.child_sayac--;
            EnemyLeader.childNumber--;
            gameObject.SetActive(false);
       
        }
        if (other.gameObject.tag == "bullet")
        {
            GameManager.child_sayac--;
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
   
    }
    void Update()
    {
        if (attackTower == true && !towerDone)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 2.0f * Time.deltaTime);
        }
        if (attackBoss)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 2.0f * Time.deltaTime);
        }
    }
 
}
