﻿using UnityEngine;
using System.Collections;

public class JudgeTextController : MonoBehaviour
{

    int frame_ = 0;

    int DeadCount = 15;

    void Start()
    {
        //transform.Rotate(-90, 0, 180);

    }

    void Update()
    {
        /*
        var pos = transform.position;
        pos.y += 0.01f;

        transform.position = pos;
        */

        if (++frame_ > DeadCount)
        {
            Destroy(gameObject);
        }

    }

}
