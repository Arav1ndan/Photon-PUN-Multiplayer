using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

using System.Collections;

[RequireComponent(typeof(InputField))]
public class PlayerNameInputField : MonoBehaviour
{
    #region Private Constants
    const string PlayerNamePrefKey = "Playername";
    #endregion
    #region Monobehaviout CallBacks
    void Start()
    {
        string defaultName = string.Empty;
        InputField InputField = this.GetComponent<InputField>();
        if (InputField != null)
        {
            if(PlayerPrefs.HasKey(PlayerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(PlayerNamePrefKey);
                InputField.text = defaultName;    
            }
        }
        PhotonNetwork.NickName =  defaultName;
    }
    #endregion
    #region Public Methods
    public void SetPlayerName(string value)
    {
        if(string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }
        PhotonNetwork.NickName=value;
        PlayerPrefs.SetString(PlayerNamePrefKey,value);
    }
    #endregion
}
