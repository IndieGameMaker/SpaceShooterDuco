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
    private Animator anim;

    // 추적사정거리 , 공격사정거리 변수
    public float traceDist = 10.0f;
    public float attackDist = 2.0f;

    public bool isDie = false;

    private int hashAttack = Animator.StringToHash("IsAttack");
    private int hashHit = Animator.StringToHash("Hit");
    private int hashDie = Animator.StringToHash("Die");
    private int hashPlayerDie = Animator.StringToHash("PlayerDie");

    private float hp = 100.0f;

    void Start()
    {
        monsterTr = GetComponent<Transform>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("PLAYER");
        if (playerObj != null)
        {
            playerTr = playerObj.GetComponent<Transform>(); //playerObj.transform;
        }

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction()); // StartCoroutine("MonsterAction");
    }

    // 몬스터의 상태를 체크하는 코루틴
    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            if (state == State.DIE) yield break;

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
                    anim.SetBool("IsTrace", false);
                    break;

                case State.TRACE:
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;
                    anim.SetBool("IsTrace", true);
                    anim.SetBool(hashAttack, false);
                    break;

                case State.ATTACK:
                    anim.SetBool(hashAttack, true);
                    break;

                case State.DIE:
                    GetComponent<CapsuleCollider>().enabled = false;
                    anim.SetTrigger(hashDie);
                    agent.isStopped = true;
                    isDie = true;
                    break;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            Destroy(coll.gameObject);
            anim.SetTrigger(hashHit);

            hp -= 20.0f;
            if (hp <= 0.0f)
            {
                state = State.DIE;
            }
        }
    }

    void YouWin()
    {
        // 모든 코루틴 정지
        StopAllCoroutines();

        agent.isStopped = true;
        anim.SetTrigger(hashPlayerDie);
    }

}

/*
    A* PathFinding
    Navigation -> Data(NavMesh) -> NavMeshAgent(A*P)
*/
