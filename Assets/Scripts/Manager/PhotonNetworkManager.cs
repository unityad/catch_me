using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class PhotonNetworkManager : PunBehaviour {

    public void Connect()
    {
        if (!PhotonNetwork.connected)
            PhotonNetwork.ConnectUsingSettings(GameplayManager.Instance.appVersion);
    }


    void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null);
    }

    void CreatePlayer()
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.Instantiate("Player", GameplayManager.Instance.respawnPoint[0].position, Quaternion.identity, 0, null);
        }
        else
        {
            PhotonNetwork.Instantiate("Player", GameplayManager.Instance.respawnPoint[1].position, Quaternion.identity, 0, null);
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        CreatePlayer();
    }

    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
        CreateRoom();
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        CreateRoom();
    }

    public override void OnConnectionFail(DisconnectCause cause)
    {
        Debug.Log("Disconnected!");
        Connect();
    }

}
