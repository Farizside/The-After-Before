using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private InputManager _input;
    [SerializeField] private List<GameObject> _tutorialPrefabs;
    [SerializeField] private GameObject _nextButton;
    [SerializeField] private GameObject _backButton;
    [SerializeField] private GameObject _skipButton;

    private int _tutorialIndex = 0;

    private void OnEnable()
    {
        _input.NextEvent += NextTutorial;
        _input.BackEvent += BackTutorial;
        _input.SkipEvent += SkipTutorial;
        
        ShowTutorial();
    }

    private void OnDisable()
    {
        _input.NextEvent -= NextTutorial;
        _input.BackEvent -= BackTutorial;
        _input.SkipEvent -= SkipTutorial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowTutorial();
        }
    }

    private void ShowTutorial()
    {
        for (int i = 0; i < _tutorialPrefabs.Count; i++)
        {
            if (i == _tutorialIndex)
            {
                _tutorialPrefabs[i]?.SetActive(true);
            }
            else
            {
                _tutorialPrefabs[i]?.SetActive(false);
            }
        }
        
        if (_tutorialIndex == 0)
        {
            _nextButton.SetActive(true);
            _backButton.SetActive(false);
            _skipButton.SetActive(true);
        }
        else if(_tutorialIndex == _tutorialPrefabs.Count - 1)
        {
            _nextButton.SetActive(false);
            _backButton.SetActive(true);
            _skipButton.SetActive(true);
        }
        else
        {
            _nextButton.SetActive(true);
            _backButton.SetActive(true);
            _skipButton.SetActive(true);
        }
    }

    public void NextTutorial()
    {
        if (_tutorialIndex == _tutorialPrefabs.Count - 1) return;
        _tutorialIndex++;
        ShowTutorial();
    }

    public void BackTutorial()
    {
        if (_tutorialIndex == 0) return;
        _tutorialIndex--;
        ShowTutorial();
    }

    public void SkipTutorial()
    {
        GameManager.Instance.UpdateGameState(GameState.Gameplay);
        InputManager.SetGameplay();
        
        gameObject.SetActive(false);
    }
}
