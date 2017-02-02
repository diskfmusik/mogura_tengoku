using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Name_delete : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }
    
    //ランキング作ったら 自爆プログラム　
    void Update()
    {
        if (GameManeger.Instance.rank_type)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
