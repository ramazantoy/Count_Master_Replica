using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tower : MonoBehaviour
{
    Transform player;
  public  GameObject TowerAmmo;
    public GameObject TowerHpp;
    TextMeshPro TowerAmmo_txt;
    TextMeshPro TowerHp_txt;
    GameManager gm;
    EnemyArc e;//Kule okçusu
    int i;//child  nesnelerinin kuleye doğru yürümesi amacıyla
    int j;//kulenin mermi sayısı kadar mermi ateşlemesi için
    int towerHp;//kulenin canı
    bool up;// sürekli aynı fonksiyonları çağırmayı önlemek amacıyla
    int towerCapasity;// ammo kapasitesi
    Vector3[] attackPositions;//child nesnelerin ilerliye bileceği pozisyonlar
    

    void Start()
    {
        attackPositions = new Vector3[5];
        attackPositions[0] = transform.position + new Vector3(0, -0.5427f, -0.3f);
        attackPositions[1]= transform.position + new Vector3(0.2f, -0.5427f, -0.3f);
        attackPositions[2] = transform.position + new Vector3(0.357f, -0.5427f, -0.3f);
        attackPositions[3] = transform.position + new Vector3(0.4f, -0.5427f, -0.3f);
        j = 0;
        TowerAmmo_txt = TowerAmmo.GetComponent<TextMeshPro>();
        TowerHp_txt = TowerHpp.GetComponent<TextMeshPro>();
        towerCapasity = Random.Range(4, 10);
       // Debug.Log("tower Capasity : " + towerCapasity);
        up = true;
        e=GetComponentInChildren<EnemyArc>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
         i = 0;
        towerHp = Random.Range(10, 15);
        //Debug.Log("tower hp : " + towerHp);
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);//Player ile arasındaki mesafe
        if (dist <= 2f && up)
        {
            CharacterController.move = false;
            Invoke("enemyAttack", 0.3f);
            InvokeRepeating("Fire",0.0f, 0.3f);
            up = false;
        }
        TowerAmmo_txt.text = "" + towerCapasity+" ammo";
        TowerHp_txt.text = "Tower Hp : " + towerHp;
        istowerDown();
    }
  
     void Fire()//Mermi Ateşleme
    {
        if (j <= towerCapasity)
        {
          Vector3 target = gm.activeChilds[i].transform.position;
          e.attack(target);
          j++;
        }
        else
        {
            CancelInvoke("Fire");
           // Debug.Log("j : " + j);
            //Debug.Log("Tower Capasity down");
        }
     
    }
    void istowerDown()// kule düştümü
    {
        if (towerHp <= 0)
        {
            CharacterController.move = true;
            towerHp = 0;
            enemyStop();
            gameObject.SetActive(false);
            
        }
    }
    void enemyAttack()//child'ların kuleye doğru yürümesi
    {
        int r;
        if (GameManager.child_sayac <= towerHp)
        {
            for (int i = 0; i <gm.activeChilds.Count;  i++)
            {
                r = Random.Range(0, 4);
                gm.activeChilds[i].GetComponent<Child>().SetTarget(attackPositions[r]);
            }
        }
        else
        {
            for (int i = 0; i < towerHp+(gm.activeChilds.Count-towerHp); i++)
            {
                r = Random.Range(0, 4);
                gm.activeChilds[i].GetComponent<Child>().SetTarget(attackPositions[r]);
            }
        }
        
  
    }
    void enemyStop()// child'ların saldırıyı kesmesi
    {
        for (int i = 0; i < gm.activeChilds.Count; i++)
        {
            gm.activeChilds[i].GetComponent<Child>().towerAt(false);
        }
        gm.kuyrukDuzenle();
        Child.towerDone = true;
    }
    private void OnTriggerEnter(Collider other)// child'lar ile kule arasında temas olursa
    {
        if (other.gameObject.tag == "child")
        {
            GameManager.child_sayac--;
            other.gameObject.SetActive(false);
            towerHp--;
        }
        if (GameManager.child_sayac == 1)
        {
            gm.gameOver();
        }
    }

}
