using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    private int hitCount;
    public GameObject expEffect;

    void OnCollisionEnter(Collision coll)
    {
        //if (coll.collider.tag == "문자열") // X
        if (coll.collider.CompareTag("BULLET"))
        {
            if (++hitCount == 3)
            {
                // 드럼통 폭발하는 로직
                ExpBarrel();
            }
        }
    }

    void ExpBarrel()
    {
        // Rigidbody 컴포넌트 추가
        Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 1500.0f);
        Destroy(this.gameObject, 3.0f);

        // 폭발효과 발생
        GameObject obj = Instantiate(expEffect, this.transform.position, Quaternion.identity);
        Destroy(obj, 5.0f);
    }
}

/*
    C# - Managed Language : 메모리 활당 / 해제 자동으로 처리한 언어 - Garbage Collection
    C,C++ - Unmanaged Language 
*/
