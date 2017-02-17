using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{

    public enum BGM
    {
        Test = 0,
        Kari,
        Holiday,
        Max,
    }

    public enum SE
    {
        Kyouda = 0,
        Pasu,
        Kon,
        Select,
        Pikon,
        Max,
    }

    static SoundManager instance_ = null;
    static public SoundManager Instance
    {
        get
        {
            if (instance_ == null)
            {
                GameObject obj = new GameObject("SoundManager");
                instance_ = obj.AddComponent<SoundManager>();
                instance_.GetComponent<SoundManager>().enabled = true;
            }
            return instance_;
        }
    }

    public AudioSource[] bgmSources_ = new AudioSource[(int)BGM.Max];
    public AudioSource[] seSources_ = new AudioSource[(int)SE.Max];

    AudioSource CreateAudioSource(string fname)
    {
        var source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.clip = Resources.Load("Audio/" + fname) as AudioClip;

        return source;
    }

    void Awake()
    {
        // シーン切り替え時に破棄しない
        GameObject.DontDestroyOnLoad(this.gameObject);

        string[] bgmName =
        {
            "test",
            "BS_2",
            "Holiday!",
        };
        for (int i = 0; i < (int)BGM.Max; i++)
            bgmSources_[i] = CreateAudioSource(bgmName[i]);

        string[] seName =
        {
            "kyouda",
            "pasu",
            "kon",
            "select01",
            "coin03"
        };
        for (int i = 0; i < (int)SE.Max; i++)
            seSources_[i] = CreateAudioSource(seName[i]);

    }

    public float GetBgmTime(BGM bgm)
    {
        return bgmSources_[(int)bgm].time;
    }

    public bool IsPlayBgm(BGM bgm)
    {
        return bgmSources_[(int)bgm].isPlaying;
    }

    public void PlaySE(SE se)
    {
        seSources_[(int)se].Play();

        //// int -> enum
        //SE s = (SE)System.Enum.ToObject(typeof(SE), 1);

    }

    public void PlayBGM(BGM bgm)
    {
        bgmSources_[(int)bgm].Play();

    }

}
