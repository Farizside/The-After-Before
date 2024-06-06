using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private InputManager _input;
    [SerializeField] private List<GameObject> _tutorialPrefabs;
    [SerializeField] [CanBeNull] private GameObject _nextButton;
    [SerializeField] [CanBeNull] private GameObject _backButton;

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
            if (_nextButton != null) _nextButton.SetActive(true);
            if (_backButton != null) _backButton.SetActive(false);
        }
        else if(_tutorialIndex == _tutorialPrefabs.Count - 1)
        {
            if (_nextButton != null) _nextButton.SetActive(false);
            if (_backButton != null) _backButton.SetActive(true);
        }
        else
        {
            if (_nextButton != null) _nextButton.SetActive(true);
            if (_backButton != null) _backButton.SetActive(true);
        }
    }

    public void NextTutorial()
    {
        if (!_nextButton) return;
        if (_tutorialIndex == _tutorialPrefabs.Count - 1) return;
        _tutorialIndex++;
        ShowTutorial();
    }

    public void BackTutorial()
    {
        if (!_backButton) return;
        if (_tutorialIndex == 0) return;
        _tutorialIndex--;
        ShowTutorial();
    }

    public void SkipTutorial()
    {
        GameManager.Instance.UpdateGameState(GameState.Gameplay);
        
        gameObject.SetActive(false);
    }
}
