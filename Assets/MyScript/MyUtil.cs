using UnityEngine;
using System.Collections;

public class MyUtil : MonoBehaviour
{

    static MyUtil instance_ = null;
    static public MyUtil Instance
    {
        //get { return instance_; }
        get
        {
            if (instance_ == null)
            {
                GameObject obj = new GameObject("MyUtil");
                instance_ = obj.AddComponent<MyUtil>();
            }
            return instance_;
        }
    }

    void Start()
    {
        // シーン切り替え時に破棄しない
        GameObject.DontDestroyOnLoad(this.gameObject);

    }

    /// <summary>
    /// Resources にある prefab を (GameObjectとして)インスタンス化する。
    /// </summary>
    /// <param name="prefabName">prefabの名前</param>
    /// <param name="parentName">親GameObjectを設定する場合には、その名前</param>
    /// <returns></returns>
    public GameObject CreateWithPrefab(string prefabName, string parentName = null)
    {
        GameObject prefab = Resources.Load(prefabName) as GameObject;
        GameObject obj = Instantiate(prefab);

        if (!string.IsNullOrEmpty(parentName))
        {
            var parent = GameObject.Find(parentName);
            obj.transform.SetParent(parent.transform, false);
        }

        return obj;

    }

    public NoteHeaderInfo ReadNoteHeaderInfo(string fname)
    {
        NoteHeaderInfo info = new NoteHeaderInfo();

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
                info.Title = s[++idx];
            }
            else if (s[idx] == "BPM")
            {
                info.Bpm = float.Parse(s[++idx]);
            }
            else if (s[idx] == "level")
            {
                info.Level = int.Parse(s[++idx]);
            }
            else if (s[idx] == "NotesNum")
            {
                idx += 6;
                break;
            }

            idx++;
        }

        return info;

    }

}
