using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClientManager : MonoBehaviour
{


    static ClientManager instance_;

    public static ClientManager Instance
    {
        get { return instance_; }
    }


    public int number_;
    private Text numText_;


    void Awake()
    {
        if (instance_)
        {
            GameObject.Destroy(this);
            return;
        }

        // シーン切り替え時に破棄しない
        GameObject.DontDestroyOnLoad(this.gameObject);
        instance_ = this;

        var obj = GameObject.Find("Canvas/ClientNumber");
        numText_ = obj.GetComponent<Text>();

    }


    void Update()
    {
        //numText_.text = "client_number : " + number_;

        GameObject obj = GameObject.Find("SpawnObj");

        if (obj == null) return;

        string id = obj.GetComponent<SpawnObj>().userId_;
        numText_.text = "userId_ : " + id;
        number_ = int.Parse(id);


        // --------------------------------------
        // camera pos update

        var camera = GameObject.Find("Main Camera").GetComponent<Camera>();

        float y = 0.46f;
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

            case 2:
                camera.transform.position = new Vector3(-5f, y, z);
                if (isCameraRotate) camera.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 3:
                camera.transform.position = new Vector3(-2.5f, y, z);
                if (isCameraRotate) camera.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 4:
                camera.transform.position = new Vector3(0f, y, z);
                if (isCameraRotate) camera.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 5:
                camera.transform.position = new Vector3(2.5f, y, z);
                if (isCameraRotate) camera.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 6:
                camera.transform.position = new Vector3(5f, y, z);
                if (isCameraRotate) camera.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;

        }



    }


}
