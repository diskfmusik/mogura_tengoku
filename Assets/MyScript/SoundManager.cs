using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{


    public enum BGM
    {
        Main = 0,
        Max,
    }


    public enum SE
    {
        Kyouda = 0,
        Pasu,
        Kon,
        Max,
    }


    static SoundManager instance_ = null;
    static public SoundManager Instance
    {
        get { return instance_; }
        /*
        get
        {
            if (instance_ == null)
            {
                GameObject obj = new GameObject("SoundManager");
                instance_ = obj.AddComponent<SoundManager>();
            }
            return instance_;
        }
        /**/
    }


    /*
    [SerializeField]
    AudioSource[] bgmSources;

    [SerializeField]
    AudioSource[] seSources;
    /**/


    /* */
    //public AudioSource[] bgmSources_ = new AudioSource[1];
    //public AudioSource[] seSources_ = new AudioSource[3];
    public AudioSource[] bgmSources_;
    public AudioSource[] seSources_;
    /**/

    //List<AudioSource> sources = new List<AudioSource>();


    void Awake()
    {
        instance_ = this;

        // シーン切り替え時に破棄しない
        GameObject.DontDestroyOnLoad(this.gameObject);

        /*
        bgmSources_ = new AudioSource[1];
        bgmSources_[0].clip = Resources.Load("Audio/test") as AudioClip;

        seSources_ = new AudioSource[3];
        seSources_[0].clip = Resources.Load("Audio/kyouda") as AudioClip;
        seSources_[1].clip = Resources.Load("Audio/pasu") as AudioClip;
        seSources_[2].clip = Resources.Load("Audio/kon") as AudioClip;
        /**/




        //gameObject.AddComponent<AudioSource>().clip = Resources.Load("Audio/test") as AudioClip;



    }

    void Start()
    {
        //AudioSource[] audioSources = GetComponents<AudioSource>();

        //for (int i = 0; i < audioSources.Length; i++)
        //{
        //    sources.Add(audioSources[i]);

        //}

    }


    void Update()
    {

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
