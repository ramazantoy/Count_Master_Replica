using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CharacterController : MonoBehaviour
{
    public float speed = 50f;
    bool right;
    bool left;
  public  float sight_speed = 20f;
    [SerializeField]
    Rigidbody r;
    GameManager gm;
     public GameObject childTxt;
  TextMeshPro child_sayac_txt;
    Transform Enemy;
   public static bool move;
    void Start()
    {
        move = false;
        GetComponent<Animator>().Play("iddle");
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        child_sayac_txt = childTxt.GetComponent<TextMeshPro>();
        childText();
        Enemy = GameObject.Find("EnemyLeader").GetComponent<Transform>();
    }

    public void childText()
    {
      
        child_sayac_txt.text = ""+GameManager.child_sayac;
    }


    void moveJobs()
    {

        if (move)
        {
            //parmağın hareket ettiği yönü anlamak amacıyla  delta.position metotdunu kullanacağım pozitif değerler 
            //yukarı ve sağı negatif değerler ise sol ve aşağı yönü kast eder.
            if (Input.touchCount > 0)//ekrana en az bir dokunma var ise
            {
                Touch finger = Input.GetTouch(0);//parmağın bir kez dokunduğu yerin bilgisi
                if (finger.deltaPosition.x > 25.0f)//pozitif 100 piksellik hareket var ise parmak sağa doğru kaydırılmış ise
                {
                    right = true;
                    left = false;

                }
                if (finger.deltaPosition.x < -25.0f)//pozitif 100 piksellik hareket var ise parmak sola doğru kaydırılmış ise
                {
                    right = false;
                    left = true;

                }
          
            }
            if (right)//sağa hareket var ise
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(1.2f, transform.position.y, transform.position.z), sight_speed * Time.deltaTime);

                right = false;
            }
            if (left)//sola hareket var ise
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(-1.2f, transform.position.y, transform.position.z), sight_speed * Time.deltaTime);

                left = false;
            }
            transform.Translate(0, 0, speed * Time.deltaTime);

        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "gate")
        {
            if (other.transform.parent.gameObject.name == "+25")
            {
              //  Debug.Log("gate +25");
                gm.kuyrugaEkle(25);
                childText();

            }
            if (other.transform.parent.gameObject.name == "+5")
            {
               // Debug.Log("gate +5");
                gm.kuyrugaEkle(5);
                childText();
            }
            if (other.transform.parent.gameObject.name == "-5")
            {
                // Debug.Log("Gate -5");
                //Debug.Log(gm.activeChilds.Count);
                if (gm.activeChilds.Count>=5)
                {
                    gm.kuyrukSil(5);
                }
                else
                {
                  
                    gm.gameOver();
                }
            }
            if (other.transform.parent.gameObject.name == "5x")
            {
               // Debug.Log("5x çarpılan değer  : " + GameManager.child_sayac);
                gm.kuyrugaEkle((GameManager.child_sayac*5)-GameManager.child_sayac);

            }
            if (other.transform.parent.gameObject.name == "2x")
            {
                // Debug.Log("5x çarpılan değer  : " + GameManager.child_sayac);
                gm.kuyrugaEkle((GameManager.child_sayac * 2) - GameManager.child_sayac);

            }

        }
     
    }
 
     void Update()
    {
        moveJobs();
        childText();

    }



}
