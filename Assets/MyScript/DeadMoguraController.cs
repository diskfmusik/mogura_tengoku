using UnityEngine;
using System.Collections;

public class DeadMoguraController : Photon.MonoBehaviour
{

    int count_ = 0;
    public bool DestroyByRpc;

    int DeadCount = 15;

    void Start()
    {

    }

    void Update()
    {

        var pos = transform.position;
        pos.y += 0.01f;
        transform.position = pos;

        if (++count_ > DeadCount)
        {
            ShouldDestroy();
        }

    }

    public void ShouldDestroy()
    {
        if (!DestroyByRpc)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
        else
        {
            this.photonView.RPC("DestroyRpc", PhotonTargets.AllBuffered);
        }
    }

    [PunRPC]
    public IEnumerator DestroyRpc()
    {
        GameObject.Destroy(this.gameObject);
        yield return 0; // if you allow 1 frame to pass, the object's OnDestroy() method gets called and cleans up references.
        PhotonNetwork.UnAllocateViewID(this.photonView.viewID);
    }

}
