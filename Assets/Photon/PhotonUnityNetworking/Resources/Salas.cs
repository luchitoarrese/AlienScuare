using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Salas : MonoBehaviourPunCallbacks
{
   
    [SerializeField] private Text textoCrearSala;
    [SerializeField] private Text textoUnirseSala;



    public void CrearSala()
    {
        
        PhotonNetwork.CreateRoom(textoCrearSala.text);
    }

    public void IngresarSala()
    {
        PhotonNetwork.JoinRoom(textoUnirseSala.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
