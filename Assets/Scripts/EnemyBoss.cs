using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public GameObject player;
    public GameObject player_mesh;
    public GameManager gm;
    Vector3[] attackPositions;
    int bossHp;
    bool isWar;
    Vector3 target;
    void Start()
    {
        isWar = false;
        attackPositions = new Vector3[3];
        attackPositions[0] = transform.position + new Vector3(-0.358f, 0.141552f, 125.848f);
        attackPositions[1] = transform.position + new Vector3(-0.115f, 0.141552f, 125.848f);
        attackPositions[2] = transform.position + new Vector3(0.141f, 0.141552f, 125.848f);
        bossHp = Random.Range(5, 15);
        target = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f);
       // Debug.Log("boss hp : " + bossHp);
    }


    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);//Player ile arasındaki mesafe
        if (dist <= 2f && !isWar)
        {
            CharacterController.move = false;
            transform.position = Vector3.MoveTowards(transform.position,target , 2.0f * Time.deltaTime);
            enemyAttack();
            isWar = true;
        }


    }
    void enemyAttack()
    {
        if (GameManager.child_sayac == 1)
        {
            CharacterController.move = true;
        }
        int r;
        for (int i = 0; i < gm.activeChilds.Count; i++)
        {
            r = Random.Range(0, 3);
            gm.activeChilds[i].GetComponent<Child>().setBossTarget(attackPositions[r]);
        }
        Child.attackBoss = true;
    }
    void isBossDown()
    {
        if (bossHp <= 0)
        {
            player.transform.position = transform.position; 
           gm.kuyrukDuzenle();
            Child.attackBoss = false;
            //player.SetActive(false);
            enemyDance();
            gm.win();
            gameObject.SetActive(false);   
        }
    }
    void enemyDance()
    {
        for (int i = 0; i < gm.activeChilds.Count; i++)
        {
            gm.activeChilds[i].GetComponent<Animator>().Play("dance");
        }
        player.GetComponent<Animator>().Play("iddle");
        player_mesh.GetComponent<SkinnedMeshRenderer>().enabled = false;
        gm.kuyrukDuzenle();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "child")
        {
            GameManager.child_sayac--;
            if (GameManager.child_sayac == 1)
            {
                gm.gameOver();
            }
            other.gameObject.SetActive(false);
            bossHp--;
            isBossDown();
        }

        if (other.gameObject.name == "Player")
        {
            other.gameObject.SetActive(false);
            gm.gameOver();
        }
    }


}
