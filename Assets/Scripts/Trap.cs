using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    GameManager gm;
    bool isTrap = false;//çarpışma işle
    
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        isTrap = false;
    }


    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "child" && isTrap==false)
        {
           GameManager.child_sayac--;
            other.gameObject.SetActive(false);
            Invoke("kuyrukDuz", 0.3f);
            isTrap = true;
        }
        else if (other.gameObject.tag == "child")
        {
            GameManager.child_sayac--;
            other.gameObject.SetActive(false);

        }
        if ( other.gameObject.name == "Player") {

            if(GameManager.child_sayac<=1)
            gm.gameOver();

        }
    }
    void kuyrukDuz() {
        gm.kuyrukDuzenle();
    }
}
