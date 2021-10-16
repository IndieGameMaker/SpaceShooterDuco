using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    // 충돌 콜백함수 (Callback function)
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "BULLET") // if (coll.gameObject.tag == "BULLET")
        {
            // 총알을 삭제
            Destroy(coll.gameObject);
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

*/