using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugMoguraController : MonoBehaviour
{

    static KeyCode[] keyAssign = {
        KeyCode.A,
        KeyCode.S,
        KeyCode.D,
        KeyCode.F,
        KeyCode.G,
    };

    enum STATUS
    {
        UP,
        DOWN
    }

    // var

    private int laneNum_ = 0; // モグラ番号
    private STATUS status_ = STATUS.UP;

    // func

    void Start()
    {
    }


    public void SetLane(int lane)
    {
        //photonView.TransferOwnership(1); // master 所有     いらない。
        laneNum_ = lane;
    }


    void Update()
    {

        Move();

        // 対応するボタンが押された瞬間
        if (Input.GetKeyDown(keyAssign[laneNum_]))
        {
            Judge();

        }


    }


    void Move()
    {
        Vector3 spd = new Vector3();

        //上に出てくる処理。上限まで来たら下がる処理へ移行
        if (status_ == STATUS.UP)
        {
            if (transform.position.y < 0.5f)
            {
                spd.y = 0.05f;/*= new Vector3(0, 0.05f, 0);*/
                //Vector3 pos = transform.position;
                //pos.y += 0.05f;
                //transform.position = pos;
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
                spd.y = -0.05f;/*= new Vector3(0, 0.05f, 0);*/
                //Vector3 pos = transform.position;
                //pos.y -= 0.05f;
                //transform.position = pos;
                transform.position += spd;
            }
            else
            {
                ComboManager.Instance.ResetCombo();
                Destroy(gameObject);
            }
        }

    }


    void Judge()
    {
        //position.yは-1.0f~0.5f間で移行0.5fに近いほど高得点
        if (transform.position.y > 0.4f)
        {
            CreateJudgeText("perfect_prefab");
            ScoreManager.Instance.AddScore(1000 /*テキトー*/ + ComboManager.Instance.NowCombo /**/);
            ComboManager.Instance.AddCombo();
            Destroy(gameObject);
            SoundManager.Instance.PlaySE(SoundManager.SE.Kyouda);
        }
        else if (transform.position.y > 0.2f)
        {
            CreateJudgeText("good_prefab");
            ScoreManager.Instance.AddScore(500 /*テキトー*/ + ComboManager.Instance.NowCombo /**/);
            ComboManager.Instance.AddCombo();
            Destroy(gameObject);
            SoundManager.Instance.PlaySE(SoundManager.SE.Pasu);
        }
        else /**/ if (transform.position.y > 0)/**/
        {
            CreateJudgeText("bad_prefab");
            ScoreManager.Instance.AddScore(100);
            ComboManager.Instance.ResetCombo();
            Destroy(gameObject);
            SoundManager.Instance.PlaySE(SoundManager.SE.Kon);
        }

    }


    void CreateJudgeText(string prefabName)
    {
        GameObject obj = MyUtil.Instance.CreateWithPrefab(prefabName);

        var pos = transform.position;
        pos.y += 0.5f;
        obj.transform.position = pos;

    }


}
