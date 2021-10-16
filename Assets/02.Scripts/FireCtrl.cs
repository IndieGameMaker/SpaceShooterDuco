using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    public GameObject bulletPrefab; // Bullet 프리팹을 저장할 변수
    public Transform firePos;       // 총알을 생성할 위치 정보

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 총알을 생성 
            // 객체 생성 Instantiate(생성할객체, 위치, 각도)
            Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        }

    }
}
