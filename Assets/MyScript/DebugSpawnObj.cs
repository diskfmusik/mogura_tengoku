using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebugSpawnObj : MonoBehaviour
{

    GameObject prefabMogura_;

    int loopStartNum = 0;
    static public float bpm = 200;
    string musicTitle;
    int level;

    //struct Note
    //{
    //    public Note(float time, int lane)
    //    {
    //        time_ = time;
    //        lane_ = lane;
    //    }
    //    public float time_;
    //    public int lane_;
    //}
    //private List<Note> notes_ = new List<Note>();


    private List<float> appearTime_ = new List<float>();
    private List<int> appearLane_ = new List<int>();


    // debug

    SoundManager.BGM bgmtype_ = SoundManager.BGM.Holiday;
    string fname_ = "Holiday";


    void Start()
    {
        prefabMogura_ = Resources.Load("DebugMogura") as GameObject;

        NoteInfoSet(fname_);

        SoundManager.Instance.PlayBGM(bgmtype_);

        //for (int i = 0; i < appearTime_.Count; i++)
        //{
        //    notes_.Add(new Note(appearTime_[i], appearLane_[i]));

        //}

        //foreach (var n in notes_)
        //{
        //    Debug.Log("time:" + n.time_ + ", lane:" + n.lane_);
        //}


    }


    void Update()
    {

        float nowTime = SoundManager.Instance.GetBgmTime(bgmtype_);
        //Debug.Log("time:" + nowTime);

        for (int i = loopStartNum; i < appearTime_.Count; i++)
        {
            // トータル拍数
            // time * (BPM / 60)
            float total = nowTime * (bpm / 60.0f);

            float offset = (bpm / 60.0f / 60.0f * 30.0f); // 30 frame 分 早く出現させる

            if (total + offset >= appearTime_[i])
            {
                var diff = (total + offset) - appearTime_[i]; // 拍

                var fdiff = diff / (bpm / 60.0f / 60.0f); // diff -> frame変換

                CreateMogura(appearLane_[i], fdiff);
                loopStartNum++;
                //break;
            }
        }

        Debug.Log("time : " + nowTime);

    }

    /// <summary>
    /// mogura 作成
    /// </summary>
    /// <param name="lane">0 ~ 4</param>
    /// <param name="fdiff">実際の再生時間 と 本来のノート出現時間 との差 (単位:frame)</param>
    void CreateMogura(int lane, float fdiff)
    {
        float x = -5.0f + 2.5f * lane;

        // 0: -5.0f
        // 1: -2.5f
        // 2: -0.0f
        // 3:  2.5f
        // 4:  5.0f

        // 0.5f - 1.5f = -1.0f

        // spd: 0.05f / frame


        var pos = new Vector3(x, -1.0f + fdiff * 0.05f, 10.0f);

        GameObject obj = /*(GameObject)*/Instantiate(prefabMogura_);

        //var obj = PhotonNetwork.Instantiate(
        //    prefabMogura_.name,
        //    pos,
        //    Quaternion.identity,
        //    0);

        obj.transform.position = pos;

        obj.GetComponent<DebugMoguraController>().SetLane(lane);

    }


    public void ClearInfo()
    {
        appearTime_.Clear();
        appearLane_.Clear();
    }


    public void SetInfo(float time, int lane)
    {
        appearTime_.Add(time);
        appearLane_.Add(lane);
    }


    void NoteInfoSet(string fname)
    {
        ClearInfo();

        TextAsset t = Resources.Load("NoteInfo/" + fname) as TextAsset;
        //Debug.Log(t.text);

        string[] s = t.text.Replace("\r", "").Split(new char[] { '\n', ',' });
        //for (int i = 0; i < s.Length - 1; i++)
        //    Debug.Log(s[i]);

        int idx = 0;
        while (true)
        {
            if (s[idx] == "Title")
            {
                musicTitle = s[++idx];
            }
            else if (s[idx] == "BPM")
            {
                bpm = float.Parse(s[++idx]);
            }
            else if (s[idx] == "level")
            {
                level = int.Parse(s[++idx]);
            }
            else if (s[idx] == "NotesNum")
            {
                idx += 6;
                break;
            }

            idx++;
        }

        for (int i = idx; i < s.Length - 1; i += 6)
        {
            // NotesNum A S D F G
            for (int j = 1; j < 6; j++)
            {
                //if (s[i + j] != "")
                if (!string.IsNullOrEmpty(s[i + j]))
                {
                    float n = float.Parse(s[i]); // NotesNum
                    float offset = float.Parse(s[i + j]); // ASDFG それぞれの値
                    //Debug.Log("NotesNum:" + n + "_offset:" + offset);
                    SetInfo(n + offset, j - 1);
                }
            }
        }

        //Debug.Log("time num:" + appearTime_.Count);
        //Debug.Log("lane num:" + appearLane_.Count);

    }


}
