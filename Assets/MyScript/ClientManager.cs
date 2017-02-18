using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ClientManager : MonoBehaviour
{

    public int frame_ = 0;

    static ClientManager instance_ = null;
    public static ClientManager Instance
    {
        //get { return instance_; }
        get
        {
            if (instance_ == null)
            {
                GameObject obj = new GameObject("ClientManager");
                instance_ = obj.AddComponent<ClientManager>();
            }
            return instance_;
        }
    }

    public int number_;
    //private Text numText_;

    //void Awake()
    //{
    //    if (instance_)
    //    {
    //        GameObject.Destroy(this);
    //        return;
    //    }

    //    // シーン切り替え時に破棄しない
    //    GameObject.DontDestroyOnLoad(this.gameObject);
    //    instance_ = this;

    //    var obj = GameObject.Find("Canvas/ClientNumber");
    //    numText_ = obj.GetComponent<Text>();

    //}

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        /*
        var obj = GameObject.Find("Canvas/ClientNumber");
        numText_ = obj.GetComponent<Text>();
        */
    }

    void Update()
    {
        //numText_.text = "client_number : " + number_;

        GameObject obj = GameObject.Find("SpawnObj");

        if (obj == null) return;

        string id = obj.GetComponent<SpawnObj>().userId_;
        //numText_.text = "userId_ : " + id;
        number_ = int.Parse(id);

        string sname = SceneManager.GetActiveScene().name;

        // --------------------------------------
        // camera pos update

        var camera = GameObject.Find("Main Camera").GetComponent<Camera>();

        //float y = 0.46f;
        float y = 0.3f;
        float z = 7.8f;

        bool isCameraRotate = true;

        /* case 1 -> master */
        switch (number_)
        {

            /*
            case 1:
                camera.transform.position = new Vector3(-5f, y, z);
                camera.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            /* */

            /* */
            case 1:
                //string sname = SceneManager.GetActiveScene().name;
                if (sname == "scene_inGame")
                {
                    camera.transform.position = new Vector3(0, 1, 15);
                    //camera.transform.position = new Vector3(0, 1, -10);
                }
                break;
            /**/

            case 2:
                if (frame_ == 0) CanvasDelete(sname);
                camera.transform.position = new Vector3(-5f, y, z);
                if (isCameraRotate) camera.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 3:
                if (frame_ == 0) CanvasDelete(sname);
                camera.transform.position = new Vector3(-2.5f, y, z);
                if (isCameraRotate) camera.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 4:
                if (frame_ == 0) CanvasDelete(sname);
                camera.transform.position = new Vector3(0f, y, z);
                if (isCameraRotate) camera.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 5:
                if (frame_ == 0) CanvasDelete(sname);
                camera.transform.position = new Vector3(2.5f, y, z);
                if (isCameraRotate) camera.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 6:
                if (frame_ == 0) CanvasDelete(sname);
                camera.transform.position = new Vector3(5f, y, z);
                if (isCameraRotate) camera.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;

        }

        frame_++;

    }

    void CanvasDelete(string sname)
    {
        switch (sname)
        {
            case "scene_titleTest":
                GameObject.Find("Canvas/mogura_tengoku").GetComponent<Text>().enabled = false;
                GameObject.Find("Canvas/play_game").GetComponent<Text>().enabled = false;
                break;

            case "scene_select":
                GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
                GameObject.Find("WaterProDaytime").transform.position = new Vector3(1000, 1000, 0);
                if (!GameObject.Find("Bg"))
                {
                    MyUtil.Instance.CreateWithPrefab("Bg");
                }
                break;

            case "scene_inGame":
                GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
                break;

            case "scene_result_0":
                GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
                GameObject.Find("WaterProDaytime").transform.position = new Vector3(1000, 1000, 0);
                if (!GameObject.Find("Bg"))
                {
                    MyUtil.Instance.CreateWithPrefab("Bg");
                }
                break;

            case "score":
                GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
                GameObject.Find("WaterProDaytime").transform.position = new Vector3(1000, 1000, 0);
                if (!GameObject.Find("Bg"))
                {
                    MyUtil.Instance.CreateWithPrefab("Bg");
                }
                break;
        }
    }

}
