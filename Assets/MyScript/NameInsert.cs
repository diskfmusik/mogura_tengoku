using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class NameInsert : MonoBehaviour
{
    public Text name_Text;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //文字うち
        if (GameManeger.Instance.type)
        {
            //OK-----------------------------------------------
            if (GameManeger.Instance.mozis == "OK")
            {
                GameManeger.Instance.hi_name = name_Text.text;
                GameManeger.Instance.type_con = true;
            }
            //BS-----------------------------------------------
            else if (GameManeger.Instance.mozis == "BS")
            {
                if (name_Text.text.Length != 0)
                    name_Text.text = 
                        name_Text.text.Substring(0, name_Text.text.Length - 1);

            }
            //文字数オーバー------------------------------
            else if (name_Text.text.Length >= 4)
            {
            }
            else
            {
                string A = GameManeger.Instance.mozis.ToString();
                name_Text.text += A;
            }

            GameManeger.Instance.type = false;
        }
    }
}
