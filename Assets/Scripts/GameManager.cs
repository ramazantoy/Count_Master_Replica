using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    bool readyforExit;//kazandıktan sonra çıkış yapabilmek için
   public GameObject win_txt;//kazandın yazıları
    public GameObject run_txt;
    public GameObject GameOver;
    bool isGameover;
    bool isStart;
    public GameObject child;//havuz yapmak amacıyla child oyun objeleri
    public GameObject FalseChilds;//set active'i false olan child'ların parent'i
    GameObject player;
    List<GameObject> childs;// child listesi
     public List<GameObject> activeChilds;//aktif olan child'ların listesi
   public  static int child_sayac = 1;//toplam çocuk sayısı
    float satir_no = 0;//player'in yanına dizilecek olan child nesneleri için referans değerler
    float satir_fark =0.181f;
    float satir_right = 0.174f;
    float satir_left = -0.174f;
    int right_sinir;//sağ çocuk sınırı
    int left_sinir;//sol çocuk sınırı
    int sayac2 = 0;


    void Start()
    {
        readyforExit = false;
        child_sayac = 1;
        isGameover = false;
        isStart = false;
        right_sinir = 5;//sağ sınır
        left_sinir = 10;//sol  sınır
        player = GameObject.Find("Player");
        childs = new List<GameObject>();//tüm child'ların listersi
        activeChilds = new List<GameObject>();// oyunda aktif olan child'ların listesi
        for (int i = 0; i <200; i++)//child havuzu
        {
            GameObject child_tmp = Instantiate(child);
            child_tmp.transform.SetParent(FalseChilds.transform);
            childs.Add(child_tmp);
            child_tmp.SetActive(false);
        }
    }
    public void kuyrugaEkle(int adet)
    {
  
        int sayac = 0;


        satir_fark = satir_no * -0.181f;
        if (child_sayac <= 100)
        {
            while (sayac < adet)
            {

                foreach (GameObject tmp in childs)
                {
                    if (tmp.activeSelf == false)
                    {
                        tmp.SetActive(true);

                        if (sayac2 <= right_sinir)//sağa right sınır adet child
                        {

                            tmp.transform.position = new Vector3(player.transform.position.x + satir_right, player.transform.position.y, player.transform.position.z + satir_fark);
                            satir_right += 0.174f;
                            //Debug.Log("sağ");
                            sayac++;
                            sayac2++;
                            tmp.transform.SetParent(player.transform);
                            activeChilds.Add(tmp);
                            break;

                        }
                        else if (sayac2 <= left_sinir)//sol'a  right sınır adet child
                        {
                            //Debug.Log("sol");
                            tmp.transform.position = new Vector3(player.transform.position.x + satir_left, player.transform.position.y, player.transform.position.z + satir_fark);
                            satir_left -= 0.174f;
                            sayac++;
                            sayac2++;
                            tmp.transform.SetParent(player.transform);
                            activeChilds.Add(tmp);
                            break;
                        }
                        else// sağ ve sol dolduysa sağ ve solu restleyip satır atlamak
                        {
                            //Debug.Log("else");

                            satir_no++;
                            satir_fark = satir_no * -0.181f;
                            satir_right = 0.0f;
                            satir_left = -0.174f;
                            sayac2 = 1;
                            right_sinir = 6;
                            left_sinir = 11;
                            sayac++;
                            tmp.transform.position = new Vector3(player.transform.position.x + satir_right, player.transform.position.y, player.transform.position.z + satir_fark);
                            tmp.transform.SetParent(player.transform);
                            activeChilds.Add(tmp);

                        }
                  

                    }
                }


            }
            child_sayac += sayac;
        }
    }
    public void kuyrukDuzenle()//child nesnelerin yok olması durumunda dizilişlerin düzenlenmesi amacıyla
    {
       // Debug.Log("kuyruk duzenlemeye başlandı : "+ activeChilds.Count);

        List<GameObject> activeChilds2 = new List<GameObject>();
        int sayac3 = 1;
            left_sinir = 10;
            right_sinir = 5;
  
        float satir_right2 = 0.174f;
        float satir_left2 = -0.174f;
        float sutun_fark2 = 0;
        int sutun_no2 = 0;
        for(int i = 0; i <activeChilds.Count; i++)
        {
            if (activeChilds[i].activeSelf == true)
            {
                if (sayac3 <= right_sinir)
                {
                    activeChilds[i].transform.position = new Vector3(player.transform.position.x + satir_right2, player.transform.position.y, player.transform.position.z + sutun_fark2);
                    satir_right2+= 0.174f;
                 //   Debug.Log("sağ local"+i);
                    sayac3++;
                    activeChilds[i].transform.SetParent(player.transform);
                }
                else if (sayac3 <= left_sinir)
                {
        


                  //  Debug.Log("sol local"+i);
                    activeChilds[i].transform.position = new Vector3(player.transform.position.x + satir_left2, player.transform.position.y, player.transform.position.z + sutun_fark2);
                    satir_left2 -= 0.174f;
                    sayac3++;
                    activeChilds[i].transform.SetParent(player.transform);

                }
                else
                {
      
                  //  Debug.Log("else local"+i);

                    sutun_no2++;
                    sutun_fark2 = sutun_no2 * -0.181f;
                    satir_right2 = 0.0f;
                    satir_left2 = -0.174f;
                    sayac3 = 1;
                    right_sinir = 6;
                    left_sinir = 11;
                    activeChilds[i].transform.position = new Vector3(player.transform.position.x + satir_right2, player.transform.position.y, player.transform.position.z + sutun_fark2);
                    //Debug.Log(sutun_fark);
    
                }
                activeChilds2.Add(activeChilds[i]);

            }     
        }
        satir_left = satir_left2;
        satir_right = satir_right2;
        satir_no = sutun_no2;
        satir_fark = sutun_fark2;
        activeChilds = new List<GameObject>();
        activeChilds = activeChilds2;
        //Debug.Log("kuyruk duzenlendi "+ activeChilds.Count);
        child_sayac = activeChilds.Count + 1;
      //  Debug.Log(child_sayac);
    }
    public void kuyrukSil(int adet)
    {
        int sayac = 1;
        foreach(GameObject tmp in activeChilds)
        {
            if (tmp.activeSelf == true)
            {
                tmp.SetActive(false);
            }
            if (sayac == adet)
            {
                break;
            }
            sayac++;
        }
        //Debug.Log("silme işleminden sonra " + activeChilds.Count);
        Invoke("kuyrukDuzenle", 0.2f);
       // kuyrukDuzenle();
    }
    public void gameOver()
    {
        GameOver.SetActive(true);
        player.SetActive(false);
        //Debug.Log("Game over");
        Time.timeScale = 0f;
        isGameover = true;
    }

 
    void Update()
    {
        if (Input.touchCount > 0 && !isStart)//ekrana en az bir dokunma var ise
        {
            Touch finger = Input.GetTouch(0);//parmağın bir kez dokunduğu yerin bilgisi
            if (finger.deltaPosition.x > 50.0f || finger.deltaPosition.x < -50.0f || finger.deltaPosition.y > 50 || finger.deltaPosition.y < -50)//pozitif 100 piksellik hareket var ise parmak sağa doğru kaydırılmış ise
            {
                Time.timeScale = 1.0f;
                run_txt.SetActive(false);
                CharacterController.move = true;
                player.GetComponent<Animator>().Play("Running");
                isStart = true;

            }
        
        }
        if(isGameover && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(0);

        }
        if (isGameover && Input.GetKeyDown(KeyCode.Escape))
        {

            Application.Quit();

        }
        if (readyforExit  && Input.GetKeyDown(KeyCode.Escape))
        {
         
            Application.Quit();
        }
        if (readyforExit && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(0);
        }


    }
    public void win()
    {
        win_txt.SetActive(true);
        readyforExit = true;
    }
}
