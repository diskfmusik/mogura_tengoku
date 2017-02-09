using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SpawnObj : Photon.MonoBehaviour
{

    public string userId_;

    int loopStartNum_ = 0;
    NoteHeaderInfo info_ = new NoteHeaderInfo();

    private List<float> appearTime_ = new List<float>();
    private List<int> appearLane_ = new List<int>();

    int bgmType_ = 0;

    void Start()
    {
        //object userName;
        //object userId;
        //photonView.owner.customProperties.TryGetValue("userName", out userName);
        //photonView.owner.customProperties.TryGetValue("userId", out userId);

        string userName = "Name : aaaa";
        //string userId = "000" + (PhotonNetwork.room.playerCount - 1);
        string userId = "000" + (PhotonNetwork.room.playerCount);
        //PhotonNetwork.autoCleanUpPlayerObjects = false; // room入ってる間は設定できない

        // ルームプロパティ
        ExitGames.Client.Photon.Hashtable customProp = new ExitGames.Client.Photon.Hashtable();
        customProp.Add("userName", userName);
        customProp.Add("userId", userId);

        photonView.owner.SetCustomProperties(customProp);

        Debug.Log("userName(" + userName + ")  userId(" + userId + ")");

        userId_ = userId.ToString();

        /*
        //ノーツの設定 
        string fname = "Test";
        NoteInfoSet(fname);
        */

        //SoundManager.Instance.PlayBGM(SoundManager.BGM.Main);

        GameObject.DontDestroyOnLoad(this.gameObject);

    }


    //int TotalPlayer = 6;
    int TotalPlayer = 6;
    bool isPlay_ = false;
    void Update()
    {

        if (!PhotonNetwork.inRoom) return;

        // master(1) + client(5) = total (6)
        // client が揃うまでは、処理をしない
        if (PhotonNetwork.room.playerCount < TotalPlayer) return;


        if (photonView.isMine &&
           PhotonNetwork.isMasterClient)
        {

            string name = SceneManager.GetActiveScene().name;
            switch (name)
            {
                case "scene_titleTest":
                    update_title();
                    break;

                case "scene_select":
                    update_select();
                    break;

                case "scene_inGame":
                    update_inGame();
                    break;

                case "scene_result_0":
                    update_result_0();
                    break;

                case "score":
                    update_score();
                    break;
            }
        }

    }

    void update_title()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        //if (Input.GetButtonDown("Accept"))
        {
            Physics2D.gravity = new Vector2(0, -9.81f); // 戻す
            string name = "scene_select";
            //string name = "scene_inGame";
            this.photonView.RPC("ChangeScene", PhotonTargets.All, name);
        }

    }

    void update_select()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        //if (Input.GetButtonDown("Accept"))
        {
            //ノーツの設定 
            var select = GameObject.Find("SelectController").GetComponent<SelectController>();
            ReadNoteInfo(select.FileName);

            bgmType_ = select.Num;

            string name = "scene_inGame";
            this.photonView.RPC("ChangeScene", PhotonTargets.All, name);
        }

    }

    void update_inGame()
    {
        // client が揃ったら、再生する。
        if (!isPlay_ &&
            PhotonNetwork.room.playerCount == TotalPlayer)
        {
            isPlay_ = true;
            //SoundManager.Instance.PlayBGM(SoundManager.BGM.Main);
            SoundManager.Instance.PlayBGM((SoundManager.BGM)bgmType_);
        }

        //float nowTime = SoundManager.Instance.GetBgmTime(SoundManager.BGM.Main);
        float nowTime = SoundManager.Instance.GetBgmTime((SoundManager.BGM)bgmType_);
        //Debug.Log("time:" + nowTime);

        // トータル拍数 = time * (BPM / 60)
        float total = nowTime * (info_.Bpm / 60.0f);

        float offset = (info_.Bpm / 60.0f / 60.0f * 30.0f); // 30 frame 分 早く出現させる


        for (int i = loopStartNum_; i < appearTime_.Count; i++)
        {
            if (total + offset >= appearTime_[i])
            {
                // 実際の再生時間 と 本来のノート出現時間 との差 (単位:拍)
                var diff = (total + offset) - appearTime_[i];

                var fdiff = diff / (info_.Bpm / 60.0f / 60.0f); // 拍から frame へ変換

                CreateMogura(appearLane_[i], fdiff);
                ++loopStartNum_;
                //break;
            }
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //if (!SoundManager.Instance.IsPlayBgm(SoundManager.BGM.Main))
        if (!SoundManager.Instance.IsPlayBgm((SoundManager.BGM)bgmType_))
        {
            isPlay_ = false;
            loopStartNum_ = 0;

            string name = "scene_result_0";
            this.photonView.RPC("ChangeScene", PhotonTargets.All, name);
        }

        /*
        if (isPlay_)
            Debug.Log("time : " + nowTime);
        */

        //Debug.Log("isplay : " + SoundManager.Instance.IsPlayBgm(SoundManager.BGM.Main));

    }

    void update_result_0()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        //if (Input.GetButtonDown("Accept"))
        {
            //string name = "scene_titleTest";
            string name = "score";
            this.photonView.RPC("ChangeScene", PhotonTargets.All, name);
        }

    }

    void update_score()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        //if (Input.GetButtonDown("Accept"))
        {
            RecordManager.Instance.Reset();

            Physics2D.gravity = new Vector2(-9.81f, -9.81f); // タイトル画面用に変える
            string name = "scene_titleTest";
            this.photonView.RPC("ChangeScene", PhotonTargets.All, name);
        }

    }

    /// <summary>
    /// mogura 作成
    /// </summary>
    /// <param name="lane">0 ~ 4</param>
    /// <param name="fdiff">実際の再生時間 と 本来のノート出現時間 との差 (単位:frame)</param>
    void CreateMogura(int lane, float fdiff)
    {
        float x = -5.0f + 2.5f * lane;
        float spd = 0.05f; // (単位: / frame)

        var pos = new Vector3(x, -1.0f + fdiff * spd, 10.0f);

        var obj = PhotonNetwork.Instantiate(
            "mogura_prefab",
            pos,
            Quaternion.Euler(-90, 0, 180), //Quaternion.identity,
            0);

        obj.GetComponent<MoguraController>().SetLane(lane);
        obj.GetComponent<MoguraController>().diff_ = fdiff * spd;
        Debug.Log("diff : " + fdiff * spd);

    }


    void ClearInfo()
    {
        appearTime_.Clear();
        appearLane_.Clear();
    }


    void SetInfo(float time, int lane)
    {
        appearTime_.Add(time);
        appearLane_.Add(lane);
    }


    void ReadNoteInfo(string fname)
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
                info_.Title = s[++idx];
            }
            else if (s[idx] == "BPM")
            {
                info_.Bpm = float.Parse(s[++idx]);
            }
            else if (s[idx] == "Level")
            {
                info_.Level = int.Parse(s[++idx]);
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


    [PunRPC]
    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);

    }


    //void OnGUI()
    //{
    //    var pos = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);

    //    var rect = new Rect(
    //         /* x */ pos.x - 50,
    //         /* y */ -pos.y + 200 + Screen.height / 2,
    //         /* w */ 100, /* h */ 20);

    //    var str = string.Format(
    //        " userId_({0})",
    //        userId_);

    //    GUI.Label(rect, str, MyGUIUtil.Instance.getGUIStyle(new Color(0.0f, 0.0f, 0.0f, 0.5f), Color.white));

    //}


}
