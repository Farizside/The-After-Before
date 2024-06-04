using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AboutUI : MonoBehaviour
{
    public GameObject[] AboutMenu;
    public Button NextBtn;
    public Button PrevBtn;
    private int _currPageIndex = 0;

    void Start()
    {
        for (int i = 0; i < AboutMenu.Length; i++)
        {
            AboutMenu[i].SetActive(i == 0);
        }

        NextBtn.onClick.AddListener(ShowNextPage);
        PrevBtn.onClick.AddListener(ShowPrevPage);

        UpdateButtonStatus();
    }

    void ShowNextPage()
    {
        if (_currPageIndex < AboutMenu.Length - 1)
        {
            AboutMenu[_currPageIndex].SetActive(false);
            _currPageIndex++;
            AboutMenu[_currPageIndex].SetActive(true);
            UpdateButtonStatus();
        }
    }

    void ShowPrevPage()
    {
        if (_currPageIndex > 0)
        {
            AboutMenu[_currPageIndex].SetActive(false);
            _currPageIndex--;
            AboutMenu[_currPageIndex].SetActive(true);
            UpdateButtonStatus();
        }
    }

    void UpdateButtonStatus()
    {
        PrevBtn.interactable = _currPageIndex > 0;
        NextBtn.interactable = _currPageIndex < AboutMenu.Length - 1;
    }
}