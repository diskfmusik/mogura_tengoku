using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectController : MonoBehaviour
{

    static int Max = 3;
    int num_ = 0;

    GameObject[] tuneInfo_ = new GameObject[Max];

    void Start()
    {
        Debug.Log("num : " + num_);

        tuneInfo_[0] = GameObject.Find("Tune1");
        tuneInfo_[1] = GameObject.Find("Tune2");
        tuneInfo_[2] = GameObject.Find("Tune3");

        string[] fname =
        {
            "Test",
            "Kari",
            "Holiday"
        };

        NoteHeaderInfo[] info = new NoteHeaderInfo[Max];
        for (int i = 0; i < Max; i++)
        {
            info[i] = MyUtil.Instance.ReadNoteHeaderInfo(fname[i]);
        }

        for (int i = 0; i < Max; i++)
        {
            var t = tuneInfo_[i].GetComponentsInChildren<Text>();
            t[0].text = "「 " + info[i].Title + " 」";
            t[1].text = "BPM : " + info[i].Bpm.ToString();
            t[2].text = "Level : " + info[i].Level.ToString();
        }

        /*
        NoteHeaderInfo info = MyUtil.Instance.ReadNoteHeaderInfo("Test");
        Debug.Log("info--------------------");
        Debug.Log("title : " + info.Title);
        Debug.Log("bpm : " + info.Bpm);
        Debug.Log("level : " + info.Level);
        Debug.Log("------------------------");
        /**/

        /*
        var text = tuneInfo_[0].GetComponentsInChildren<Text>();
        text[0].text = "「 " + info.Title + " 」";
        text[1].text = "BPM : " + info.Bpm.ToString();
        text[2].text = "Level : " + info.Level.ToString();
        /**/

    }

    void Update()
    {
        // 0 ~ 1
        Color blue = new Color(10f / 255f, 42f / 255f, 195f / 255f, 100f / 255f);
        Color red = new Color(231f / 255f, 29f / 255f, 29f / 255f, 100f / 255f);

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            num_ = (++num_) % Max;
            //Debug.Log("num : " + num_);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            num_ = (--num_ + Max) % Max;
            //Debug.Log("num : " + num_);
        }


        for (int i = 0; i < Max; i++)
        {
            Image image = tuneInfo_[i].GetComponent<Image>();
            if (i == num_)
                image.color = red;
            else
                image.color = blue;

        }


    }

}
