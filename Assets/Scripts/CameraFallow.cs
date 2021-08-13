using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    [SerializeField]
    Transform Player;
    Vector3 distance;
    float speed = 4.0f;//kameranın takip etme hızı
    void Start()
    {

    }
    private void LateUpdate()
    {
        distance = new Vector3(Player.position.x, transform.position.y, Player.position.z - 3.66f);
        transform.position = Vector3.Lerp(transform.position, distance, speed * Time.deltaTime);



    }
}
