using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComboManager : MonoBehaviour
{

    static ComboManager instance_;

    public static ComboManager Instance
    {
        get { return instance_; }
    }

    private Text comboText_;
    int nowCombo_ = 0;
    public int NowCombo
    {
        get { return nowCombo_; }
    }

    void Start()
    {
        instance_ = this;

        var obj = GameObject.Find("Canvas/ComboText");
        comboText_ = obj.GetComponent<Text>();

    }

    void Update()
    {

        if (nowCombo_ >= 5)
        {
            comboText_.enabled = true;
            comboText_.text = nowCombo_ + " Combo";
        }
        else
        {
            comboText_.enabled = false;

        }

    }

    public void AddCombo()
    {
        nowCombo_++;
    }

    public void ResetCombo()
    {
        nowCombo_ = 0;
    }

}
