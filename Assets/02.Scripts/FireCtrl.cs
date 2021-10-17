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

    private RaycastHit hit;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();
        muzzleFlash.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(firePos.position, firePos.forward * 10.0f, Color.green);

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
            if (Physics.Raycast(firePos.position, firePos.forward, out hit, 10.0f, 1 << 8))
            {
                Debug.Log(hit.collider.name);
            }
        }
    }

    void Fire()
    {
        // 총알을 생성 
        // 객체 생성 Instantiate(생성할객체, 위치, 각도)
        //Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        // 총소리 발생
        audio.PlayOneShot(fireSfx, 0.8f);
        // 총구 화염 효과
        StartCoroutine(ShowMuzzleFlash());
    }

    // 코루틴 (Coroutine)
    IEnumerator ShowMuzzleFlash()
    {
        // 텍스처 오프셋 값을 변경 (0,0) (0.5, 0) (0.5, 0.5) (0, 0.5) x,y =( 0, 0.5 )
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;
        muzzleFlash.material.mainTextureOffset = offset;

        // 스케일 변경
        muzzleFlash.transform.localScale = Vector3.one * Random.Range(1.0f, 3.0f);
        // = new Vector3(1,1,1) * Random.Range(1,0f, 3.0f)

        // 회전 처리
        muzzleFlash.transform.localRotation = Quaternion.Euler(Vector3.forward * Random.Range(0, 360));
        // = Quaternion.Euler(0, 0, Random.Range(0, 360));

        // MeshRenderer 컴포넌트를 활성화
        muzzleFlash.enabled = true;

        // 대기하는 로직
        yield return new WaitForSeconds(0.3f);

        // MeshRenderer 비활성
        muzzleFlash.enabled = false;
    }
}


/*
    하늘 표현 방식

    1. Procedural Sky
    2. Skybox (6-Sided Sky)
    3. Cubemap
    4. Panoramic Sky

*/

/*
    LOD (Level of Detail)

    카메라로 부터 가까울 수록 하이 폴리곤으로 교체
    LOD0 - High
    LOD1 - Middle
    LOD2 - Low
    Culling
*/

/*
    유한상태머신 (Finite State Machine:FSM)
        - NPC(NonPlayable Character) 인공지능

*/