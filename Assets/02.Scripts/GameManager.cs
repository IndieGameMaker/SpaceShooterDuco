using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글턴 변수를 정의
    public static GameManager instance = null;

    public GameObject monsterPrefab;
    public Transform[] points;

    public bool isGameOver;
    public float createTime = 3.0f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();

        //InvokeRepeating("CreateMonster", 2.0f, createTime);
        StartCoroutine(CreateMonster());
    }

    IEnumerator CreateMonster()
    {
        yield return new WaitForSeconds(2.0f);

        while (!isGameOver)
        {
            // 출현시킬 위치정보 Index 추출
            int idx = Random.Range(1, points.Length); //1~23

            // 몬스터 생성
            Instantiate(monsterPrefab, points[idx].position, Quaternion.identity);

            yield return new WaitForSeconds(createTime);
        }
    }
}

/*
    싱글턴 디자인 패턴 (Singleton Design Pattern)
        - 전역적인 접근이 편리한 장점

*/
