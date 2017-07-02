using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    /// <summary>
    /// Tạo singleton
    /// </summary>
    private static GameplayManager _instance = null;
    private GameplayManager() { }
    public static GameplayManager Instance
    {
        get { return _instance; }
    }

    public PhotonNetworkManager photonNetworkManager;

    public Transform[] respawnPoint;

    public string appVersion;

    void Awake()
    {
        if (_instance)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;

        DontDestroyOnLoad(this.gameObject);
        Application.runInBackground = true;
        PhotonNetwork.sendRate = 20;
        PhotonNetwork.sendRateOnSerialize = 20;
    }

    void Start()
    {
        photonNetworkManager.Connect();
    }
}
