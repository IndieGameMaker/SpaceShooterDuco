using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // 열거형 변수
    public enum State
    {
        IDLE, TRACE, ATTACK, DIE
    }

    public State state = State.IDLE;

    private Transform monsterTr;
    private Transform playerTr;

    // 추적사정거리 , 공격사정거리 변수
    public float traceDist = 10.0f;
    public float attackDist = 2.0f;

    public bool isDie = false;

    // Start is called before the first frame update
    void Start()
    {
        monsterTr = GetComponent<Transform>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("PLAYER");
        if (playerObj != null)
        {
            playerTr = playerObj.GetComponent<Transform>(); //playerObj.transform;
        }

        StartCoroutine(CheckMonsterState());
    }

    // 몬스터의 상태를 체크하는 코루틴
    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            // 몬스터의 상태를 체크하는 로직
            // 주인공의 위치와 몬스터의 위치값으로 거리 계산
            float distance = Vector3.Distance(playerTr.position, monsterTr.position);

            if (distance <= attackDist)
            {
                // 상태를 공격으로 변경
                state = State.ATTACK;
            }
            else if (distance <= traceDist)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.IDLE;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }
}
