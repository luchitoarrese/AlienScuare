using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs_Inst : MonoBehaviour
{
    [SerializeField] private List<PhotonView> playerPrefab;
    void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab[(Random.Range(0, 1))].name, Vector3.zero, Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
