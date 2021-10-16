using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    private int hitCount;
    public GameObject expEffect;

    [HideInInspector]
    public MeshRenderer renderer;
    public Texture[] textures;

    private AudioSource audio;
    public AudioClip expSfx;

    void Start()
    {
        renderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
        audio = GetComponent<AudioSource>();

        int idx = Random.Range(0, textures.Length); // 0,1,2
        renderer.material.mainTexture = textures[idx];
    }

    /*
        Random.Range(0, 10) : 0 ~ 9
        Random.Range(0.0f, 10.0f) : 0.0f ~ 10.0f
    */

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

        // 폭발음 발생
        audio.PlayOneShot(expSfx, 0.9f);
    }
}

/*
    C# - Managed Language : 메모리 활당 / 해제 자동으로 처리한 언어 - Garbage Collection
    C,C++ - Unmanaged Language 
*/

/*
    총구화염 효과 (Muzzle Flash)

    1. 파티클 효과
    2. Mesh

*/
