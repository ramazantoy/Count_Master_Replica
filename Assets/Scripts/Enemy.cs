using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
 public   bool attack;
    Animator a;
    Vector3 target;
    bool playerAtack = false;

    void Start()
    {
        target = Vector3.zero;
        a=GetComponent<Animator>();
        playerAtack = false;
        attack = false;
    }
    public void SetTarget(Transform target)
    {
        a.Play("Running");
        this.target = target.position;
        attack = true;
        Invoke("activeFalse", 1.0f);

    }
    public void playerAt(bool attack)
    {
        playerAtack = attack;

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name=="Player" && playerAtack)
        {
            other.gameObject.SetActive(false);
           
        }
    }



    void Update()
    {
        if (attack == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 5.0f * Time.deltaTime);

        }

      
    }
   void activeFalse()
    {
        gameObject.SetActive(false);
    }
}
