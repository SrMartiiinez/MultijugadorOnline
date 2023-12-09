using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks {

    public string roomName = "room";

    public void Connect() {
        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.SendRate = 10;

        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateRoom() {
        PhotonNetwork.CreateRoom(roomName);
    }

    public void JoinRoom() {
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnCreatedRoom() {
        if (PhotonNetwork.IsMasterClient) {
            PhotonNetwork.LoadLevel("Multiplayer");
        }
    }

    public void ChangeRoomName(string _roomName) {
        roomName = _roomName;
    }
}
