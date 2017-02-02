using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoguraController : Photon.MonoBehaviour
{

    static KeyCode[] keyAssign = {
        KeyCode.A,
        KeyCode.S,
        KeyCode.D,
        KeyCode.F,
        KeyCode.G,
    };

    // 1, 8, 7, 6, 5
    static string[] KeyAssign = {
        "Btn1",
        "Btn2",
        "Btn3",
        "Btn4",
        "Btn5"
    };


    enum STATUS
    {
        UP,
        DOWN
    }

    // var

    private int laneNum_ = 0; // モグラ番号
    private STATUS status_ = STATUS.UP;

    public float diff_ { get; set; }

    public bool DestroyByRpc;

    // func

    void Start()
    {
        //transform.Rotate(-90, 0, 180);

    }


    public void SetLane(int lane)
    {
        //photonView.TransferOwnership(1); // master 所有     いらない。
        laneNum_ = lane;
    }


    void Update()
    {

        if (photonView.isMine)
        {
            Move();

            // 対応するボタンが押された瞬間
            //if (Input.GetKeyDown(keyAssign[laneNum_]))
            if (Input.GetButton(KeyAssign[laneNum_]))
            {
                Judge();
            }

        }
        else
        {

        }

    }


    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //データの送信 
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            //データの受信 
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }


    void Move()
    {
        Vector3 spd = new Vector3();

        //上に出てくる処理。上限まで来たら下がる処理へ移行
        if (status_ == STATUS.UP)
        {
            //if (transform.position.y < 0.5f)
            if (transform.position.y < 0.5f + diff_)
            {
                spd.y = 0.05f;
                transform.position += spd;
            }
            else
            {
                status_ = STATUS.DOWN;

            }
        }
        //下に降りる処理。下限まで来たら待ち状態へ移行
        else if (status_ == STATUS.DOWN)
        {
            if (transform.position.y >= -1.0f)
            {
                spd.y = -0.05f;
                transform.position += spd;
            }
            else
            {
                RecordManager.Instance.Combo = 0;
                RecordManager.Instance.Miss++;
                ShouldDestroy();
                //CreateDeadMogura();
            }
        }

        // わからん
        //GetComponent<PhotonTransformView>().SetSynchronizedValues(speed: spd, turnSpeed: 0);

    }


    void Judge()
    {
        //position.yは-1.0f~0.5f間で移行0.5fに近いほど高得点
        if (transform.position.y > 0.4f)
        {
            CreateJudgeText("perfect_prefab");
            RecordManager.Instance.Combo++;
            RecordManager.Instance.Perfect++;
            RecordManager.Instance.Score += (1000 /*テキトー*/ + RecordManager.Instance.Combo /**/);
            ShouldDestroy();
            CreateDeadMogura();
            SoundManager.Instance.PlaySE(SoundManager.SE.Kyouda);
        }
        else if (transform.position.y > 0.2f)
        {
            CreateJudgeText("good_prefab");
            RecordManager.Instance.Combo++;
            RecordManager.Instance.Good++;
            RecordManager.Instance.Score += (500 /*テキトー*/ + RecordManager.Instance.Combo /**/);
            ShouldDestroy();
            CreateDeadMogura();
            SoundManager.Instance.PlaySE(SoundManager.SE.Pasu);
        }
        else /*if (transform.position.y > 0)*/
        {
            CreateJudgeText("bad_prefab");
            RecordManager.Instance.Combo = 0;
            RecordManager.Instance.Bad++;
            RecordManager.Instance.Score += 100;
            ShouldDestroy();
            CreateDeadMogura();
            SoundManager.Instance.PlaySE(SoundManager.SE.Kon);
        }

    }


    void CreateDeadMogura()
    {
        var obj = PhotonNetwork.Instantiate(
            "deadMogura_prefab",
            transform.position /*pos*/,
            Quaternion.Euler(-90, 0, 180), //Quaternion.identity,
            0);

    }



    void CreateJudgeText(string prefabName)
    {
        var pos = transform.position;
        pos.y -= 1.0f;
        pos.z -= 0.5f;

        var obj = PhotonNetwork.Instantiate(
            prefabName,
            pos,
            Quaternion.Euler(-90, 0, 180), //Quaternion.identity,
            0);

    }


    //void CreateScoreText(string text)
    //{
    //    GameObject obj = MyUtil.Instance.CreateWithPrefab("ScoreTextPrefab", "Canvas");

    //    var pos = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
    //    pos.y += 15.0f;
    //    obj.transform.position = pos;

    //    obj.GetComponent<ScoreText>().SetText(text);

    //}


    //void OnGUI()
    //{
    //    var pos = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);

    //    var rect = new Rect(
    //         /* x */ pos.x - 100,
    //         /* y */ -pos.y + 200 + Screen.height / 2,
    //         /* w */ 200, /* h */ 20);

    //    int num = ClientManager.Instance.number_;

    //    var str = string.Format(
    //        "lane({0}) ViewID({1}) {2} {3}",
    //        laneNum_,
    //        photonView.viewID,
    //        (photonView.isSceneView) ? "scene" : "",
    //        (photonView.isMine) ? "mine" : "owner: " + photonView.ownerId);

    //    GUI.Label(rect, str, MyGUIUtil.Instance.getGUIStyle(new Color(0.0f, 0.0f, 0.0f, 0.5f), Color.white));

    //}


    // -----------------------------------------------------------------------------
    // OnClickDestroy()

    public void ShouldDestroy()
    {
        if (!DestroyByRpc)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
        else
        {
            this.photonView.RPC("DestroyRpc", PhotonTargets.AllBuffered);
        }
    }


    [PunRPC]
    public IEnumerator DestroyRpc()
    {
        GameObject.Destroy(this.gameObject);
        yield return 0; // if you allow 1 frame to pass, the object's OnDestroy() method gets called and cleans up references.
        PhotonNetwork.UnAllocateViewID(this.photonView.viewID);
    }


}
