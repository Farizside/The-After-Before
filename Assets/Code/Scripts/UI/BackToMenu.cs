using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BackToMenu : MonoBehaviour
{
    [SerializeField] private InputManager _input;
    [SerializeField] [CanBeNull] private GameObject _currCanvas;
    [SerializeField] [CanBeNull] private GameObject _mainMenuCanvas;
    
    public Button BtnMenu;
    // Start is called before the first frame update
    void Start()
    {
        if (!_currCanvas)
        {
            BtnMenu.onClick.AddListener(Menu);
        }
        else
        {
            BtnMenu.onClick.AddListener(Back);
            _input.BackEvent += Back;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Menu()
    {
        GameManager.Instance.HandleResume();
        SceneManager.LoadScene("Mainmenu");
    }

    private void Back()
    {
        _currCanvas.SetActive(false);
        _mainMenuCanvas.SetActive(true);
    }
}
