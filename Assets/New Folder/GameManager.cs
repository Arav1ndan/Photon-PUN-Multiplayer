using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    #region Photon Callbacks
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
    #region Public Methods
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    #endregion
    #region Private methods
    void LoadArena()
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Tring to load a level but we are not the master Client");
            return;
        }
        Debug.LogFormat("PhotonNetwork : Loading level : {0}",PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("Room for "+ PhotonNetwork.CurrentRoom.PlayerCount);
    }
    #endregion
    #region Photon Callbacks
    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnplayerEnteredRoom() {0}",other.NickName);
        if(PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom is MasterClient {0}", PhotonNetwork.IsMasterClient);
            LoadArena();
        }
    }
    public override void OnPlayerLeftRoom(Player other)
    {
       Debug.LogFormat("OnPlayerLeftRoom() {0}",other.NickName);
       if(PhotonNetwork.IsMasterClient)
       {
            Debug.LogFormat("OnPlayerleftRoom isMasterClient {0}",PhotonNetwork.IsMasterClient);
            LoadArena();
       }
    }
    #endregion
}
