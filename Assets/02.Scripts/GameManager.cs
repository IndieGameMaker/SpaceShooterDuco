using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글턴 변수를 정의
    public static GameManager instance = null;

    public GameObject monsterPrefab;
    public Transform[] points;

    // 오브젝트 풀링 변수
    public List<GameObject> monsterPool = new List<GameObject>();
    public int maxPool = 10;

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
        MakeMonsterPool();

        //InvokeRepeating("CreateMonster", 2.0f, createTime);
        StartCoroutine(CreateMonster());
    }

    void MakeMonsterPool()
    {
        for (int i = 0; i < maxPool; i++)
        {
            GameObject monster = Instantiate<GameObject>(monsterPrefab);
            monster.name = $"Monster_{i:00}";
            monster.SetActive(false);

            monsterPool.Add(monster);
        }
    }

    IEnumerator CreateMonster()
    {
        yield return new WaitForSeconds(2.0f);

        while (!isGameOver)
        {
            // 몬스터 생성
            //Instantiate(monsterPrefab, points[idx].position, Quaternion.identity);
            foreach (var monster in monsterPool)
            {
                // 사용가능한 몬스터 여부를 확인
                if (monster.activeSelf == false)
                {
                    // 출현시킬 위치정보 Index 추출
                    int idx = Random.Range(1, points.Length); //1~23

                    monster.transform.position = points[idx].position;
                    monster.SetActive(true);
                    break;
                }
            }

            yield return new WaitForSeconds(createTime);
        }
    }
}

/*
    싱글턴 디자인 패턴 (Singleton Design Pattern)
        - 전역적인 접근이 편리한 장점
*/

/*
    오브젝트 풀링 (Object Pooling)

    Instantiate
    Destroy
*/
