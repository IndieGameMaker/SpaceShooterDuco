using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private float h;    //좌우 화살표 입력값을 저장할 실수형 변수를 선언
    private float v;    //업다운 화살표 입력값을 저장할 실수형 변수를 선언
    private float r;    //마우스 X좌표의 변위값 저장할 실수형 변수를 선언

    public float speed = 8.0f;

    [HideInInspector]
    public Animation anim;

    public float turnSpeed = 100.0f;
    private float _turnSpeed;

    private float initHp = 100.0f;
    public float currHp = 100.0f;

    // 델리게이트 선언
    public delegate void PlayerDieHandler();

    // 이벤트를 정의
    public static event PlayerDieHandler OnPlayerDie;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        _turnSpeed = 0.0f;
        // 컴포넌트를 추출해서 변수에 할당처리
        anim = this.gameObject.GetComponent<Animation>();

        anim.Play("Idle");

        yield return new WaitForSeconds(0.3f);
        _turnSpeed = turnSpeed;
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
        r = Input.GetAxis("Mouse X");

        // Debug.Log("h=" + h); // 콘솔 뷰에 h 메시지를 출력
        // Debug.Log("v=" + v); // 콘솔 뷰에 v 메시지를 출력

        // 벡터의 +연산을 통해 이동방향 벡터를 계산
        // Vector3 moveDir = (전/후진 벡터) + (좌/우 벡터);
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        transform.Translate(moveDir.normalized * Time.deltaTime * speed);

        // 회전 처리
        transform.Rotate(Vector3.up * r * Time.deltaTime * _turnSpeed);

        // transform.Translate(Vector3.forward * 0.1f * v);    //전/후 이동
        // transform.Translate(Vector3.right * 0.1f * h);      //좌/우 이동
        PlayerAnim();
    }

    // 주인공 캐릭터의 애니메이션을 변경하는 로직
    void PlayerAnim()
    {
        if (v >= 0.1f)  // 전진
        {
            anim.CrossFade("RunF", 0.3f);
        }
        else if (v <= -0.1f) // 후진
        {
            anim.CrossFade("RunB", 0.3f);
        }
        else if (h >= 0.1f) // 오른쪽
        {
            anim.CrossFade("RunR", 0.3f);
        }
        else if (h <= -0.1f) // 왼쪽
        {
            anim.CrossFade("RunL", 0.3f);
        }
        else
        {
            anim.CrossFade("Idle", 0.3f);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (currHp > 0.0f && coll.CompareTag("PUNCH"))
        {
            currHp -= 10.0f;
            if (currHp <= 0.0f)
            {
                OnPlayerDie();
                GameManager.instance.isGameOver = true;

                //GameObject.Find("GameManager").GetComponent<GameManager>().isGameOver = true;

                //PlayerDie();
            }
        }
    }

    void PlayerDie()
    {
        // 스테이지에 있는 모든 몬스터를 배열에 저장
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");

        foreach (GameObject monster in monsters)
        {
            monster.SendMessage("YouWin", SendMessageOptions.DontRequireReceiver);
        }
    }
}
