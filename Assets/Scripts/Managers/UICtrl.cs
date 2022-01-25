using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class UICtrl : MonoBehaviour
{
    //public LevelData levels;
    public static int lastLevel;

    public static System.Action<bool> OnSetEditMode;

    public string gameScene = "SampleScene";
    public string menuScene = "MainMenu";
    public string outroScene = "Outro";

    public TMP_InputField nicknameInput;
    public TextMeshProUGUI txtError;

    public static bool editMode = false;
    public void RestartLevel()
    {
        //levels.Load(lastLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void LoadOutro()
    {
        if(nicknameInput)
        {
            if(nicknameInput.text.Length > 2)
            {
                ScoreCtrl.SendScore(nicknameInput.text);
            }
            else
            {
                AddWarning("Nickname too short!");
                return;
            }
        }
        SceneManager.LoadScene(outroScene);
    }

    public void AddWarning(string message)
    {
        //Debug.LogError(message);
        if(txtError)
        {
            txtError.SetText(message);
            txtError.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        //RestartLevel();
    }

    public void NextLevel()
    {
        //lastLevel++;
        //RestartLevel();
    }

    public void EditMode()
    {
        editMode = !editMode;
        OnSetEditMode?.Invoke(editMode);

        if(!editMode)
        {
            //Game.editor.SaveAndTest();
        }
        else
        {
            //Game.editor.Load();
        }
    }
}
