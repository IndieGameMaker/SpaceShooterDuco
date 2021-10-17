using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  //네비게이션 컴포넌트를 사용하기 위해 선언하는 네임스페이스

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
    private NavMeshAgent agent;

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

        agent = GetComponent<NavMeshAgent>();

        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction()); // StartCoroutine("MonsterAction");
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

    // 몬스터의 상태에 따라서 행동처리하는 코루틴
    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State.IDLE:
                    agent.isStopped = true;
                    break;

                case State.TRACE:
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;
                    break;

                case State.ATTACK:
                    //
                    Debug.Log("공격모드");
                    break;
                case State.DIE:
                    //
                    break;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }
}

/*
    A* PathFinding
    Navigation -> Data(NavMesh) -> NavMeshAgent(A*P)
*/
