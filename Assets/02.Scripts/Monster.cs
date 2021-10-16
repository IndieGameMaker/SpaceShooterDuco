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


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
