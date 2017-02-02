using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class NameSelect : MonoBehaviour
{
    [SerializeField]
    public int vertical = 0;
    [SerializeField]
    public int horizontal = 0;

    public Text name_Text;
    Colorchange color_script;

    private float nextTime;
    private bool UP;
    private float interval = 1.0f; //点滅周期


    // Use this for initialization
    void Start()
    {
        nextTime = Time.time;
        UP = true;
    }


    public void name_Start(int ver, int hor, string text)
    {
        vertical = ver;
        horizontal = hor;
        interval = 0.6f;
        name_Text.text = text.ToString();
        color_script = GetComponent<Colorchange>();
    }

    void Update()
    {
        if (Time.time > nextTime)
        {
            nextTime += interval;
            UP = !UP;
        }
        //座標一致
        if (GameManeger.Instance.ver_hor(vertical, horizontal))
        {
            light_Text();
            if (GameManeger.Instance.mozis != name_Text.text)
            {
                GameManeger.Instance.mozis = name_Text.text;
            }
        }
        else
        {
            name_Text.color = new Color(1, 1, 0, 1);
        }
        
    }

    public string Name_share()
    {
        Debug.Log(name_Text.text);
        return name_Text.text.ToString();
    }

    //文字点滅
    public void light_Text()
    {
        float A = 0;
        if (UP)
            A = (nextTime - Time.time) / interval;
        else
            A = 1 - ((nextTime - Time.time) / interval);

        name_Text.color = new Color(1, A, A, 1);

    }
}
