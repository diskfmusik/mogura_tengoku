using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUNInit : MonoBehaviour
{

    void Start()
    {
        var pm = PUNManager.Instance;
        ClientManager.Instance.frame_ = 0;
    }

}
