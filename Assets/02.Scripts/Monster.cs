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

    // Start is called before the first frame update
    void Start()
    {
        monsterTr = GetComponent<Transform>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("PLAYER");
        if (playerObj != null)
        {
            playerTr = playerObj.GetComponent<Transform>(); //playerObj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
