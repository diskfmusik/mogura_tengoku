using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

//ほかシーンへの切り替えは
//ここだけいじろう！！！！！！

public class GameManeger : MonoBehaviour
{
    //① シングルトン
    //   GameManeger をstaticにする
    static GameManeger instance;

    //④シングルトン呼び出し専用
    public static GameManeger Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    [SerializeField]
    private int vertical_m = 0;
    [SerializeField]
    private int horizontal_m = 0;

    int ver_max = 3;
    int hor_max = 10;

    //文字うち
    public bool type;
    //やったぜハイスコア
    public bool newrecord;
    //文字うち完了
    public bool type_con;
    //文字うち後ランキング表完成
    public bool rank_type;

    //文字待機
    public string mozis;
    //ハイスコアネーム
    public string hi_name;

    //縦横の座標の確認をする
    public bool ver_hor(int ver, int hor)
    {
        if (vertical_m == ver && horizontal_m == hor)
            return true;
        return false;
    }

    [SerializeField]
    GameObject name_Text;
    [SerializeField]
    GameObject name_In;
    [SerializeField]
    GameObject name_Title;
    [SerializeField]
    GameObject name_HowTo;

    [SerializeField]
    GameObject Rank_Title;
    [SerializeField]
    GameObject Rank_Prehabs;
    [SerializeField]
    GameObject Rank_warning;

    List<GameObject> names = new List<GameObject>();


    void Awake()
    {
        instance = this;
    }

    //② シングルトン
    //   GameManeger instanceを呼び出せる
    public static GameManeger GetInstance()
    {
        return instance;
    }

    // Use this for initialization
    void Start()
    {
        //Testよう
        GetComponent<Scoreload>().NewScoreSave(Random.Range(1, 50000));


        type = false;
        type_con = false;
        rank_type = false;
        //移動してきたスコアが10位以内だったら
        bool Name_Write = GetComponent<Scoreload>().hi_score();
        if (Name_Write)
        {
            newrecord = true;
            write_create();
            Mozi_Create();
        }
        else
        {
            newrecord = false;
            type_con = true;
        }
    }
    void write_create()
    {
        GameObject Inst = Instantiate(name_In);
        //親子関係を築く
        Inst.transform.SetParent(GameObject.Find("Canvas").transform, false);

        GameObject To = Instantiate(name_HowTo);
        To.transform.SetParent(GameObject.Find("Canvas").transform, false);

        GameObject Title = Instantiate(name_Title);
        Title.transform.SetParent(GameObject.Find("Canvas").transform, false);
    }

    void Ranking_Create()
    {
        GameObject Title = Instantiate(Rank_Title);
        Title.transform.SetParent(GameObject.Find("Canvas").transform, false);

        GameObject war = Instantiate(Rank_warning);
        war.transform.SetParent(GameObject.Find("Canvas").transform, false);

        int Count = 0;
        bool hi = false;

        string[] PA = new string[20];
        //新スコア
        string[] PB = new string[20];

        int Sco = GetComponent<Scoreload>().score_get();
        GetComponent<Scoreload>().listload(PA);

        for (int i = 0; i < 2; i++)
        {

            for (int k = 0; k < 5; k++)
            {
                int A = (k + (i * 5)) * 2;

                if (!hi && Sco >= int.Parse(PA[A + 1]))
                {
                    GameObject Prehab = Instantiate(Rank_Prehabs);
                    Prehab.transform.SetParent(GameObject.Find("Canvas").transform, false);
                    Prehab.transform.position += new Vector3(340 * i, -70 * k, 0);
                    Prehab.GetComponent<Rank_Insert>().Rankin(A / 2, hi_name, Sco.ToString());

                    PB[A] = hi_name;
                    PB[A + 1] = Sco.ToString();

                    Count += 2;
                    hi = true;
                }
                else
                {
                    GameObject Prehab = Instantiate(Rank_Prehabs);
                    Prehab.transform.SetParent(GameObject.Find("Canvas").transform, false);
                    Prehab.transform.position += new Vector3(340 * i, -70 * k, 0);
                    Prehab.GetComponent<Rank_Insert>().Rankin(A / 2, PA[A - Count], PA[A + 1 - Count]);
                    PB[A] = PA[A - Count];
                    PB[A + 1] = PA[A + 1 - Count];
                }
            }

        }

        if (newrecord)
            GetComponent<Scoreload>().listsave(PB);

        rank_type = true;
    }

    void Mozi_Create()
    {


        //textの初期配置
        for (int i = 0; i < ver_max; i++)
        {
            for (int k = 0; k < hor_max; k++)
            {
                GameObject nameko = Instantiate(name_Text);

                nameko.transform.SetParent(GameObject.Find("Canvas/name_peace").transform, false);
                nameko.transform.position = new Vector3(75 + (55 * k), 250 - (100 * i), 0);

                string mozi = "0";
                switch (i)
                {
                    case 0:
                        switch (k)
                        {
                            case 0:
                                mozi = "A";
                                break;
                            case 1:
                                mozi = "B";
                                break;
                            case 2:
                                mozi = "C";
                                break;
                            case 3:
                                mozi = "D";
                                break;
                            case 4:
                                mozi = "E";
                                break;
                            case 5:
                                mozi = "F";
                                break;
                            case 6:
                                mozi = "G";
                                break;
                            case 7:
                                mozi = "H";
                                break;
                            case 8:
                                mozi = "I";
                                break;
                            case 9:
                                mozi = "J";
                                break;

                        }
                        break;
                    case 1:
                        switch (k)
                        {
                            case 0:
                                mozi = "K";
                                break;
                            case 1:
                                mozi = "L";
                                break;
                            case 2:
                                mozi = "M";
                                break;
                            case 3:
                                mozi = "N";
                                break;
                            case 4:
                                mozi = "O";
                                break;
                            case 5:
                                mozi = "P";
                                break;
                            case 6:
                                mozi = "Q";
                                break;
                            case 7:
                                mozi = "R";
                                break;
                            case 8:
                                mozi = "S";
                                break;
                            case 9:
                                mozi = "T";
                                break;
                        }
                        break;
                    case 2:
                        switch (k)
                        {
                            case 0:
                                mozi = "U";
                                break;
                            case 1:
                                mozi = "V";
                                break;
                            case 2:
                                mozi = "W";
                                break;
                            case 3:
                                mozi = "X";
                                break;
                            case 4:
                                mozi = "Y";
                                break;
                            case 5:
                                mozi = "Z";
                                break;
                            case 6:
                                mozi = "!";
                                break;
                            case 7:
                                mozi = "?";
                                break;
                            case 8:
                                mozi = "BS";
                                break;
                            case 9:
                                mozi = "OK";
                                break;
                        }
                        break;
                }

                names.Add(nameko);

                //nameko内のNameSelectを持ってくる
                nameko.GetComponent<NameSelect>().name_Start(i, k, mozi);

            }
        }
    }


    float prevVert = 0f;
    // Update is called once per frame
    void Update()
    {
        //ランキング生成完了--------------
        if (rank_type)
        {
            //===========================
            //タイトルニモドル
            //============================

            //  ゲーム終了後に１にスコアを代入
            //  GetComponent<Scoreload>().textSave(1);

        }
        //文字うちが完了したら--------------
        else if (type_con)
        {

            Ranking_Create();
            //スコアリセット不正防止
            GetComponent<Scoreload>().NewScoreSave(0);
        }
        else
        {
            //キーコントロール


            if (Input.GetButtonDown("Select"))
            {
                vertical_m--;
                SoundManager.Instance.PlaySE(SoundManager.SE.Select);
            }
            if (Input.GetButtonDown("Accept"))
            {
                vertical_m++;
                SoundManager.Instance.PlaySE(SoundManager.SE.Select);
            }


            float vert = Input.GetAxis("Vertical");

            if (Input.GetButtonDown("Left"))
            {
                horizontal_m--;
                SoundManager.Instance.PlaySE(SoundManager.SE.Select);

            }
            if (vert > 0 &&
                prevVert < 0)
            {
                horizontal_m++;
                SoundManager.Instance.PlaySE(SoundManager.SE.Select);

            }
            //決定
            //if (Input.GetKeyDown(KeyCode.J))
            if (Input.GetButtonDown("Btn3"))
            {
                type = true;
                SoundManager.Instance.PlaySE(SoundManager.SE.Pikon);
            }


            prevVert = vert;

            if (vertical_m < 0)
                vertical_m += ver_max;
            if (vertical_m >= ver_max)
                vertical_m -= ver_max;
            if (horizontal_m < 0)
                horizontal_m += hor_max;
            if (horizontal_m >= hor_max)
                horizontal_m -= hor_max;
        }

    }


}
