using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private float h;    //좌우 화살표 입력값을 저장할 실수형 변수를 선언
    private float v;    //업다운 화살표 입력값을 저장할 실수형 변수를 선언

    // Start is called before the first frame update
    void Start()
    {

    }

    // 화면을 랜더링하는 주기
    /*
        Vector3(x,y,z)

        벡터의 크기가 1인 벡터(순수하게 방향만을 가리키는 벡터)
        정규화 벡터(Normalized Vector), 단위벡터(Unit Vector)

        Vector3.forward = Vector3(0, 0, 1)
        Vector3.up      = Vector3(0, 1, 0)
        Vector3.right   = Vector3(1, 0, 0)

        Vector3.one  = Vector3(1, 1, 1)
        Vector3.zero = Vector3(0, 0, 0)
    */
    void Update()
    {
        //transform.position += new Vector3(0, 0, 0.1f);
        //transform.position = transform.position + new Vector3(0, 0, 0.1f);
        transform.Translate(Vector3.forward * 0.1f);

        h = Input.GetAxis("Horizontal"); // -1.0f ~ 0.0f ~ +1.0f

        Debug.Log("h=" + h);
    }
}
