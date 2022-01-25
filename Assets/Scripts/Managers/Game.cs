using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static event System.Action OnLevelStarted;
    public static event System.Action<GameOverParams> OnGameOver;


    public static ResourceCtrl resources;
    public static ImageCtrl images;
    public static AudioCtrl sounds; 
    public static Camera mainCam;

    public static float levelStartTime = -1f;
    public static bool Started
    {
        get => levelStartTime >= 0;
        set {
            if (value)
            {
                levelStartTime = Time.time;
            }
            else
            {
                levelStartTime = -1f;
            }
        }
    }

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
        StartLevel();
    }

    private void OnEnable()
    {
        UICtrl.OnSetEditMode += OnSetEditMode;
    }

    private void OnDisable()
    {
        UICtrl.OnSetEditMode -= OnSetEditMode;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 10f;
           
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            resources.Add(ResourceType.SkillPoints, 100);
        }
#endif
    }

    void OnSetEditMode(bool state)
    {
        if(state)
        {
            ClearObjects();
        }
    }

    public static void ClearObjects()
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("RemoveOnLevelClear"))
        {
            Destroy(item);
        }
    }


    public void StartLevel()
    {
        Started = true;
        OnLevelStarted?.Invoke();
    }

    public static void Victory()
    {
        if (!Started) return;
        OnGameOver?.Invoke(new GameOverParams { win = true });
        Started = false;
    }

    public static void Defeat()
    {
        if (!Started) return;
        OnGameOver?.Invoke(new GameOverParams { win = false });
        Started = false;
    }
}

public struct GameOverParams
{
    public bool win;
}
