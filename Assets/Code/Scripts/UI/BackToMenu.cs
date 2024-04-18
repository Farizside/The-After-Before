using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BackToMenu : MonoBehaviour
{
    [SerializeField] private InputManager _input;
    
    public Button BtnMenu;
    // Start is called before the first frame update
    void Start()
    {
        BtnMenu.onClick.AddListener(Menu);
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
}
