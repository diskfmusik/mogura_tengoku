using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectController : MonoBehaviour
{

    static int Max = 3;
    int num_ = 0;
    public int Num { get { return num_; } }

    string[] fname_ =
    {
        "Test",
        "BitterSweet",
        "Holiday"
    };
    public string FileName { get { return fname_[num_]; } }


    GameObject[] tuneInfo_ = new GameObject[Max];

    float prevVert = 0f;

    void Start()
    {
        tuneInfo_[0] = GameObject.Find("Tune1");
        tuneInfo_[1] = GameObject.Find("Tune2");
        tuneInfo_[2] = GameObject.Find("Tune3");


        NoteHeaderInfo[] info = new NoteHeaderInfo[Max];
        for (int i = 0; i < Max; i++)
        {
            info[i] = MyUtil.Instance.ReadNoteHeaderInfo(fname_[i]);
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

        /*
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
        /**/


        /* */
        float vert = Input.GetAxis("Vertical");

        if (vert > 0 &&
            prevVert < 0)
        {
            num_ = (++num_) % Max;
            //Debug.Log("num : " + num_);
        }
        if (Input.GetButtonDown("Left"))
        {
            num_ = (--num_ + Max) % Max;
            //Debug.Log("num : " + num_);
        }
        /**/


        for (int i = 0; i < Max; i++)
        {
            Image image = tuneInfo_[i].GetComponent<Image>();
            if (i == num_)
                image.color = red;
            else
                image.color = blue;

        }


        //Debug.Log("vert : " + vert);
        prevVert = vert;

    }

}
