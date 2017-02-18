using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class RecordManager : MonoBehaviour
{

    public int Score { get; set; }
    public int Combo { get; set; }
    public int Perfect { get; set; }
    public int Good { get; set; }
    public int Bad { get; set; }
    public int Miss { get; set; }
    int maxCombo_ = 0;

    int displayScore_ = 0;
    public float comboTextSize_ = 1.0f;

    static RecordManager instance_ = null;
    public static RecordManager Instance
    {
        //get { return instance_; }
        get
        {
            if (instance_ == null)
            {
                GameObject obj = new GameObject("RecordManager");
                instance_ = obj.AddComponent<RecordManager>();
            }
            return instance_;
        }
    }

    void Awake()
    {
        // シーン切り替え時に破棄しない
        GameObject.DontDestroyOnLoad(this.gameObject);

    }

    void Update()
    {
        string sname = SceneManager.GetActiveScene().name;

        switch (sname)
        {

            case "scene_inGame":
                float diff = Score - displayScore_;
                if (diff < 10)
                {
                    displayScore_ = Score;
                }
                else
                {
                    displayScore_ += (int)(diff / 10.0f);
                }
                GameObject.Find("Canvas/TotalScoreText").GetComponent<Text>().text = displayScore_.ToString();

                comboTextSize_ -= 0.05f;
                if (comboTextSize_ < 1.0f) comboTextSize_ = 1.0f;

                var comboText = GameObject.Find("Canvas/ComboText").GetComponent<Text>();
                comboText.transform.localScale = new Vector3(comboTextSize_, comboTextSize_, 1);

                if (Combo >= 5)
                {
                    comboText.text = Combo + " Combo";
                }
                else
                {
                    comboText.text = "";
                }
                if (Combo > maxCombo_) maxCombo_ = Combo;

                break;

            case "scene_result_0":
                displayScore_ = 0;
                GameObject.Find("Canvas/Score").GetComponent<Text>().text = Score.ToString();
                GameObject.Find("Canvas/Combo").GetComponent<Text>().text = "MaxCombo : " + maxCombo_;
                GameObject.Find("Canvas/Perfect").GetComponent<Text>().text = "Perfect [" + Perfect + "]";
                GameObject.Find("Canvas/Good").GetComponent<Text>().text = "Good [" + Good + "]";
                GameObject.Find("Canvas/Bad").GetComponent<Text>().text = "Bad [" + Bad + "]";
                GameObject.Find("Canvas/Miss").GetComponent<Text>().text = "Miss [" + Miss + "]";
                break;

        }

    }

    public void Reset()
    {
        Score = 0;
        Combo = 0;
        Perfect = 0;
        Good = 0;
        Bad = 0;
        Miss = 0;
        maxCombo_ = 0;
    }

}
