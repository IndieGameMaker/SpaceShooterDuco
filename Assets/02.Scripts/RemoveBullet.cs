using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;

    // 충돌 콜백함수 (Callback function)
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "BULLET") // if (coll.gameObject.tag == "BULLET")
        {
            // 총알을 삭제
            Destroy(coll.gameObject);

            // 충돌 지점의 정보
            ContactPoint cp = coll.GetContact(0);
            // 법선 벡터
            Vector3 _normal = -cp.normal;
            // 벡터 방향의 각도를 계산
            Quaternion rot = Quaternion.LookRotation(_normal);

            // 스파크 이펙트를 생성
            Instantiate(sparkEffect, cp.point, rot);
        }
    }
}


/*
    충돌 이벤트(충돌 콜백 함수 - Callback function) 호출 조건

    1. 양쪽 다 Collider 컴포넌트가 있어야 함.
    2. 이동하는 객체에는 Rigidbody 컴포넌트


    OnCollisionEnter
    OnCollisionStay
    OnCollisionExit


    법선벡터(Normal) : 충돌지점에서 수직을 이루는 벡터
*/