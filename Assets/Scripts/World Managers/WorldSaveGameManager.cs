using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveGameManager : MonoBehaviour
{
    public static WorldSaveGameManager instance { get; private set; }

    [SerializeField] int WorldSceneIndex = 1;

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

    public IEnumerator LoadNewGame()
    {
        AsyncOperation LoadOperation = SceneManager.LoadSceneAsync(WorldSceneIndex);

        yield return null;
    }

    public int GetWorldSceneIndex()
    { 
        return WorldSceneIndex; 
    }
}
