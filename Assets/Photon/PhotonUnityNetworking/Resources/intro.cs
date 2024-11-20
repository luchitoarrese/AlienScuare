using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class intro : MonoBehaviourPunCallbacks
{
    public RectTransform loadingBar;
    public float maxLoadingTime = 5f;
    private float connectionTime = 0f;
    private bool isConnecting = false;
    private float maxBarWidth;
    [SerializeField] private TextMeshProUGUI buttonText;
    void Start()
    {

        maxBarWidth = loadingBar.parent.GetComponent<RectTransform>().rect.width;
        loadingBar.sizeDelta = new Vector2(0, loadingBar.sizeDelta.y);

        isConnecting = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    void Update()
    {
        if (isConnecting)
        {
            connectionTime += Time.deltaTime;


            float progress = Mathf.Clamp(connectionTime / maxLoadingTime, 0, 1);


            loadingBar.sizeDelta = new Vector2(progress * maxBarWidth * 2, loadingBar.sizeDelta.y);


            if (connectionTime >= maxLoadingTime)
            {
                Debug.Log("Error: Tiempo de conexión agotado.");
                isConnecting = false;
            }
        }
    }

    public override void OnConnectedToMaster()
    {

        isConnecting = false;
        PhotonNetwork.JoinLobby();
        Debug.Log("Se conecto al Master");
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        SceneManager.LoadScene("Lobby");
    }
}
