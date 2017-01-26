using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectController : MonoBehaviour
{

    int Max = 3;
    int num_ = 0;

    public Text[] text_ = new Text[3];

    void Start()
    {
        Debug.Log("num : " + num_);

        NoteHeaderInfo info = MyUtil.Instance.ReadNoteHeaderInfo("Test");
        Debug.Log("title : " + info.Title);
        Debug.Log("bpm : " + info.Bpm);
        Debug.Log("level : " + info.Level);

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            num_ = (++num_) % Max;
            Debug.Log("num : " + num_);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            num_ = (--num_ + Max) % Max;
            Debug.Log("num : " + num_);
        }

        for (int i = 0; i < Max; i++)
        {
            if (i == num_)
                text_[i].color = Color.red;
            else
                text_[i].color = Color.white;

        }


    }

}
