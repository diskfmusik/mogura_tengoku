using UnityEngine;
using System.Collections;

public class MyGUIUtil : MonoBehaviour
{

    static MyGUIUtil instance_ = null;
    static public MyGUIUtil Instance
    {
        //get { return instance_; }
        get
        {
            if (instance_ == null)
            {
                GameObject obj = new GameObject("MyGUIUtil");
                instance_ = obj.AddComponent<MyGUIUtil>();
            }
            return instance_;
        }
    }


    void Awake()
    {
        // シーン切り替え時に破棄しない
        GameObject.DontDestroyOnLoad(this.gameObject);

    }


    // -----------------------------------------------------------------------------
    // PointedAtGameObjectInfo

    public GUIStyle getGUIStyle(Color bgColor, Color textColor)
    {
        GUIStyle guiStyle = new GUIStyle();
        GUIStyleState styleState = new GUIStyleState();

        // GUI背景色のバックアップ 
        Color backColor = GUI.backgroundColor;

        // GUI背景の色を設定 
        GUI.backgroundColor = bgColor;

        // 背景用テクスチャを設定 
        styleState.background = Texture2D.whiteTexture;

        // テキストの色を設定 
        styleState.textColor = textColor;

        // スタイルの設定。 
        guiStyle.normal = styleState;

        return guiStyle;
    }


}
