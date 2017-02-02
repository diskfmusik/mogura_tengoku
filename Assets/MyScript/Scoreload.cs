using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Scoreload : MonoBehaviour
{
    //scoreX Xにすうじ　０～９
    //nameX 　Xに数字　 

    void Start()
    {

        if (PlayerPrefs.GetInt("score0") <= 0)
        {
            for (int i = 0; i < 10; i++)
            {
                string a = "name" + i;
                string b = "score" + i;
                PlayerPrefs.SetString(a, "AAA");
                PlayerPrefs.SetInt(b, (1000 - (10 * i)));
          
            }
        }


    }

    public bool hi_score()
    {

        for (int i = 0; i < 10; i++)
        {
            string b = "score" + i;

            if (PlayerPrefs.GetInt(b) <= PlayerPrefs.GetInt("newscore"))
                return true;
        }
        return false;
    }
    public int score_get()
    {
        return PlayerPrefs.GetInt("newscore");
    }


    //数値を入れる
    public void listload(string[] NI)
    {
        for (int i = 0; i < 10; i++)
        {
            string a = "name" + i;
            string b = "score" + i;
            NI[i * 2] = PlayerPrefs.GetString(a);
            NI[i * 2 + 1] = PlayerPrefs.GetInt(b).ToString();
        }
    }

    public void NewScoreSave()
    {
        PlayerPrefs.SetInt("newscore", 0);
    }

    public void listsave(string[] NI)
    {
        for (int i = 0; i < 10; i++)
        {
            string a = "name" + i;
            string b = "score" + i;
       
            PlayerPrefs.SetString(a, NI[i * 2]);
            PlayerPrefs.SetInt(b, int.Parse(NI[i * 2 + 1]));
        }


    }

    public void NewScoreSave(int score)
    {
        PlayerPrefs.SetInt("newscore", score);

    }


    void createObj(GameObject obj, Vector2 pos)
    {
        GameObject go = Instantiate(obj,
                                    new Vector3(pos.x, pos.y, 0),
                                    obj.transform.rotation) as GameObject;
    }

}
