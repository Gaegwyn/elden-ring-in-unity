using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager instance { get; private set; }

    [Header("NETWORK JOIN")]
    [SerializeField] bool StartGameAsClient;

    private void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (StartGameAsClient)
        {
            StartGameAsClient = false;
            // Shut down Network first in order to join as client. TODO: I bet we're going to have a host or join button eventually...
            NetworkManager.Singleton.Shutdown();
            NetworkManager.Singleton.StartClient();
        }
    }
}
