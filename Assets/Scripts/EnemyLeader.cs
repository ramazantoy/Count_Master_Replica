using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyLeader : MonoBehaviour
{//Düşman prefablarının yönetilmesi
    public Transform player;
    GameObject[] ChildEnemy;//Düşman child'ların listesi
    public static int childNumber = 0;// düşman child sayısı
   public  GameManager gm;
    public GameObject Enemytext;
     TextMeshPro Enemy_textpro;
    public GameObject Enemy;
    bool win;

    bool isWar;
    void Start()
    {
        
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        Enemy_textpro = Enemytext.GetComponent<TextMeshPro>();  
        int rand = Random.Range(10,20);
        ChildEnemy = new GameObject[rand];//Düşman child'ların dizisi
        for(int i = 0; i < ChildEnemy.Length;i++)//Düşman child'ların oluşturulması
        {
            GameObject tmp = Instantiate(Enemy);
            tmp.SetActive(false);
            ChildEnemy[i] = tmp;
        }
        addEnemys();//Düşman child'ların yan yana dizilmesi
        childNumber = ChildEnemy.Length;
        //Debug.Log(childNumber);
        Enemy_textpro.text = "" + ChildEnemy.Length;
        isWar = false;
    }
    void addEnemys()//Düşman child'ların yan yana dizilmesi
    {
        int sayac2 = 1;
        float sutun_no = 0;
        float sutun_fark = 0.181f;
        float satir_right = 0.174f;
        float satir_left = -0.174f;
        int right_sinir = 5;
        int left_sinir = 10;
        for (int i = 0; i < ChildEnemy.Length; i++)
        {
          
            if (ChildEnemy[i].activeSelf == false)
            {
                ChildEnemy[i].SetActive(true);

                if (sayac2 <= right_sinir)
                {

                    ChildEnemy[i].transform.position = new Vector3(transform.position.x + satir_right,transform.position.y,transform.position.z -sutun_fark);
                    satir_right += 0.174f;
                   
                    sayac2++;
                    ChildEnemy[i].transform.SetParent(transform);
                }
                else if (sayac2 <= left_sinir)
                {
                    ChildEnemy[i].transform.position = new Vector3(transform.position.x + satir_left,transform.position.y,transform.position.z - sutun_fark);
                    satir_left -= 0.174f;
               
                    sayac2++;
                    ChildEnemy[i].transform.SetParent(transform);
                }
                else
                {
                    sutun_no++;
                    sutun_fark = sutun_no * 0.181f;
                    satir_right = 0.0f;
                    satir_left = -0.174f;
                    sayac2 = 1;
                    right_sinir = 6;
                    left_sinir = 11;
                    ChildEnemy[i].transform.position = new Vector3(transform.position.x + satir_right,transform.position.y,transform.position.z - sutun_fark);
                    ChildEnemy[i].transform.SetParent(transform);

                }


            }
        }

    }
    void attack()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);//Player ile arasındaki mesafe
        if (dist <= 2f && !isWar)
        {
            CharacterController.move = false;
           isWar = true;//sadece bir kere çalışması amacıyla
            if (ChildEnemy.Length > gm.activeChilds.Count)
            {
                for (int i = 0; i < ChildEnemy.Length; i++)
                {
                    if (i >= gm.activeChilds.Count - 1)
                    {
                        ChildEnemy[i].GetComponent<Enemy>().SetTarget(player.transform);
                        //Debug.Log("target player");
                        ChildEnemy[i].GetComponent<Enemy>().playerAt(true);
                        win = true;
                        break;
                    }
                    else
                    {
                        ChildEnemy[i].GetComponent<Enemy>().SetTarget(gm.activeChilds[i].transform);
                    }
                }
            }
            else
            {
               // Debug.Log(gm.activeChilds.Count);
                for (int i = 0; i < ChildEnemy.Length; i++)
                {


                    ChildEnemy[i].GetComponent<Enemy>().SetTarget(gm.activeChilds[i].transform);
                    
                }
            }
            Invoke("checkKid", 1f);
        }
      
    }
    void checkKid()
    {
        int oldL = gm.activeChilds.Count;
      // Invoke("kuyrukDuz", 0.2f);
         gm.kuyrukDuzenle();
       // Debug.Log("Check Kid  child_sayac : " + GameManager.child_sayac); 
     if (win)
        {
            foreach(GameObject tmp in ChildEnemy)
            {
                if (tmp.activeSelf == true)
                {
                    tmp.GetComponent<Animator>().Play("dance");
                }
            }
            Invoke("gameover", 2.0f);
        }
        else
        {
            int l = activeChild();
      
            if (l != 0)
            {
                for(int i = 0; i < ChildEnemy.Length; i++)
                {
                    ChildEnemy[i].SetActive(false);
                }
            }
            childNumber = 0;
            gm.kuyrukDuzenle();
            CharacterController.move = true;
        }
    }
    void gameover()
    {
        gm.gameOver();
    }
    void kuyrukDuz()
    {

    }
     int activeChild()
    {
        int sayac = 0;
        for(int i = 0; i < ChildEnemy.Length; i++)
        {
            if (ChildEnemy[i].activeSelf == true)
            {
                sayac++;
            }
        }
        return sayac;
    }

 
    

    void Update()
    {
        attack();
        Enemy_textpro.text = "" + childNumber;
    }
}
