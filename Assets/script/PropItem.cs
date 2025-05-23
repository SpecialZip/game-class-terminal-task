using Photon.Pun;
using UnityEngine;

public class PropItem : MonoBehaviour
{
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0, 1, 0);
    
    private void Update()
    {
        transform.Rotate(rotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // 触发重生逻辑
        GameObject.Find("Props").GetComponent<PropRespawn>().Respawn(
            transform.position, 
            transform.rotation
        );

        // 网络游戏判断
        bool isNetwork = PlayerPrefs.GetString("GameMode") == "Network";
        if (isNetwork)
        {
            PhotonView photonView = other.GetComponent<PhotonView>();
            if (photonView != null && photonView.IsMine)
            {
                PropManager.Instance.CollectProp();
            }
        }
        else
        {
            PropManager.Instance.CollectProp();
        }

        Destroy(gameObject);
    }
}