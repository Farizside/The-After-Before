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
        Debug.Log("Next button clicked");
        if (_currPageIndex < AboutMenu.Length - 1)
        {
            AboutMenu[_currPageIndex].SetActive(false);
            _currPageIndex++;
            AboutMenu[_currPageIndex].SetActive(true);
            UpdateButtonStatus();
        }
        else
        {
            Debug.Log("Next ga bisa di last page");
        }
    }

    void ShowPrevPage()
    {
        Debug.Log("Prev button clicked");
        if (_currPageIndex > 0)
        {
            AboutMenu[_currPageIndex].SetActive(false);
            _currPageIndex--;
            AboutMenu[_currPageIndex].SetActive(true);
            Debug.Log("Current page index: " + _currPageIndex);
            UpdateButtonStatus();
        }
        else
        {
            Debug.Log("Prev ga bisa di first page");
        }
    }

    void UpdateButtonStatus()
    {
        PrevBtn.interactable = _currPageIndex > 0;
        NextBtn.interactable = _currPageIndex < AboutMenu.Length - 1;
    }
}