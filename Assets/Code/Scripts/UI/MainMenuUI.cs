using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button BtnGamePlay;
    public Button BtnSetting;
    public Button BtnAbout;
    public Button BtnExit;
    public GameObject CanvasMenu;
    public GameObject CanvasSetting;
    public GameObject CanvasAbout;
    // Start is called before the first frame update
    void Start()
    {
        BtnGamePlay.onClick.AddListener(GamePlay);
        BtnSetting.onClick.AddListener(Setting);
        BtnAbout.onClick.AddListener(About);
        BtnExit.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GamePlay()
    {
        SceneManager.LoadScene("Gameplay");
    }

    private void Setting()
    {
        CanvasMenu.SetActive(false);
        CanvasSetting.SetActive(true);
    }

    private void About()
    {
        CanvasMenu.SetActive(false);
        CanvasAbout.SetActive(true);
    }

    private void Exit()
    {
        Application.Quit();
    }
}
