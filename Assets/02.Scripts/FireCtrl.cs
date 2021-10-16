#pragma warning disable CS0108, IDE0052, IDE0051

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    public GameObject bulletPrefab; // Bullet 프리팹을 저장할 변수
    public Transform firePos;       // 총알을 생성할 위치 정보
    public AudioClip fireSfx;       // 총소리 음원

    private AudioSource audio;
    private MeshRenderer muzzleFlash;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();
        muzzleFlash.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        // 총알을 생성 
        // 객체 생성 Instantiate(생성할객체, 위치, 각도)
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        // 총소리 발생
        audio.PlayOneShot(fireSfx, 0.8f);
        // 총구 화염 효과
        ShowMuzzleFlash();
    }

    void ShowMuzzleFlash()
    {
        // MeshRenderer 컴포넌트를 활성화
        muzzleFlash.enabled = true;

        // 대기하는 로직

        // MeshRenderer 비활성
        muzzleFlash.enabled = false;
    }
}
