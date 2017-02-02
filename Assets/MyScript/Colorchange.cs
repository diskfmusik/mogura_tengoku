using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Colorchange : MonoBehaviour
{
    [SerializeField]
    Text textObject; //点滅させたい文字

    private float nextTime;
    private bool UP;
    private float interval = 1.0f; //点滅周期


    // Use this for initialization
    void Start()
    {
        nextTime = Time.time;
        UP = true;
    }

    // Update is called once per frame
    void Update()
    {
        light_Text(textObject);
    }

    public void light_Text(Text textObj)
    {
        float A = 0;
        if (UP)
            A = (nextTime - Time.time) / interval;
        else
            A = 1 - ((nextTime - Time.time) / interval);

        textObj.color = new Color(1, A, A, 1);

        if (Time.time > nextTime)
        {
            nextTime += interval;
            UP = !UP;
        }
    }

}




