using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PUNManager : MonoBehaviour
{

    static private PUNManager instance_ = null;

    Text text_;
    private string gameVersion = "v1.00";

    void Start()
    {
        if (instance_)
        {
            GameObject.Destroy(this);
            return;
        }

        Physics2D.gravity = new Vector2(-9.81f, -9.81f); // タイトル画面用に変える

        var cm = ClientManager.Instance;
        var mu = MyUtil.Instance;
        var mgu = MyGUIUtil.Instance;
        var sm = SoundManager.Instance;
        var rm = RecordManager.Instance;

        // シーン切り替え時に破棄しない
        GameObject.DontDestroyOnLoad(this.gameObject);
        instance_ = this;

        text_ = GameObject.Find("Canvas/PUNmng").GetComponent<Text>();

        //
        PhotonNetwork.ConnectUsingSettings(gameVersion);

    }


    // マスターサーバ接続時のコールバック
    // ↑ ConnectUsingSettings() を呼んだ時に
    void OnJoinedLobby()
    {
        string userName = "Name : aaaa";
        string userId = "ID : 000" + ClientManager.Instance.number_;
        PhotonNetwork.autoCleanUpPlayerObjects = false;

        // ルームプロパティ
        ExitGames.Client.Photon.Hashtable customProp = new ExitGames.Client.Photon.Hashtable();
        customProp.Add("userName", userName);
        customProp.Add("userId", userId);
        PhotonNetwork.SetPlayerCustomProperties(customProp);


        // ルームオプションの設定
        RoomOptions myOptions = new RoomOptions();


        myOptions.CustomRoomProperties = customProp;
        myOptions.CustomRoomPropertiesForLobby = new string[] { "userName", "userId" };
        myOptions.IsOpen = true;
        myOptions.IsVisible = true;


        myOptions.MaxPlayers = 6; // 20人まで

        // IDの公開
        myOptions.PublishUserId = true;

        // ロビーの設定
        TypedLobby myTypedLobby = new TypedLobby();
        myTypedLobby.Name = "Default";
        myTypedLobby.Type = LobbyType.Default;

        // ルームを任意の設定で作る
        PhotonNetwork.JoinOrCreateRoom("DefaultRoom", myOptions, myTypedLobby);

    }


    // ルーム入室成功
    void OnJoinedRoom()
    {
        Debug.Log("IN ROOM");

        GameObject go = PhotonNetwork.Instantiate("Cube", Vector3.zero, Quaternion.identity, 0);
        go.name = "SpawnObj";

    }


    void Update()
    {
        if (SceneManager.GetActiveScene().name != "scene_titleTest") return;


        if (PhotonNetwork.connectionStateDetailed.ToString() != "Joined")
        {
            text_.text = PhotonNetwork.connectionStateDetailed.ToString();
        }
        else
        {
            text_.text = "接続中" + " name:" + PhotonNetwork.room.name + " num:" + PhotonNetwork.room.playerCount;
        }

    }


}
