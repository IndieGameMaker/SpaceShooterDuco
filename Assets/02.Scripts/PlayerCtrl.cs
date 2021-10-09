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
        h = Input.GetAxis("Horizontal"); // -1.0f ~ 0.0f ~ +1.0f
        v = Input.GetAxis("Vertical");   // -1.0f ~ 0.0f ~ +1.0f

        Debug.Log("h=" + h); // 콘솔 뷰에 h 메시지를 출력
        Debug.Log("v=" + v); // 콘솔 뷰에 v 메시지를 출력

        // 벡터의 +연산을 통해 이동방향 벡터를 계산
        //Vector3 moveDir = (전/후진 벡터) + (좌/우 벡터);
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        transform.Translate(moveDir * 0.1f);

        //transform.Translate(Vector3.forward * 0.1f * v);    //전/후 이동
        //transform.Translate(Vector3.right * 0.1f * h);      //좌/우 이동
    }
}
