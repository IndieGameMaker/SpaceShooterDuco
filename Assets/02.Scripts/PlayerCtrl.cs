using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // 화면을 랜더링하는 주기
    /*
        Vector3(x,y,z)
    */
    void Update()
    {
        //transform.position += new Vector3(0, 0, 0.1f);
        //transform.position = transform.position + new Vector3(0, 0, 0.1f);
        transform.Translate(Vector3.forward * 0.1f);
    }
}
