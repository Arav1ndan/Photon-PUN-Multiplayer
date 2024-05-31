using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Xml.Serialization;

public class launcher : MonoBehaviourPunCallbacks
{
    #region Private Serializable Fields
    [SerializeField] private byte MaxPlayerPerRoom = 4;
    [SerializeField] private GameObject ControlPanel;
    [SerializeField] private GameObject ProgressLabel;
    #endregion
    #region Private Fields
    string gameVersion = "1";
    bool isConnecting;
    #endregion
    #region MonoBehaviourPunCallbacks CallBacks
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Start()
    {
        ProgressLabel.SetActive(false);
        ControlPanel.SetActive(true);
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        ProgressLabel.SetActive(false);
        ControlPanel.SetActive(true);
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {

        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers = MaxPlayerPerRoom});
    }
    public override void OnJoinedRoom()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("WE load the Room For 1");

            PhotonNetwork.LoadLevel("Room for 1");
        }
    }
    #endregion

    #region public Methods
    public void Connect()
    {
        ProgressLabel.SetActive(true );
        ControlPanel.SetActive(false);
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
        if(isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
            isConnecting = false;
        }
        
    }
    #endregion
}
