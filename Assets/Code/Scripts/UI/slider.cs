using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class slider : MonoBehaviour
{
    public Button VolumeBtn; 
    public GameObject VolumeSlider; 
    private CanvasGroup _sliderCanvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        _sliderCanvasGroup = VolumeSlider.GetComponent<CanvasGroup>();
        HideSlider();
        VolumeBtn.onClick.AddListener(Slider);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Slider()
    {
        // Tampilkan atau sembunyikan slider
        if (_sliderCanvasGroup.alpha == 0)
        {
            ShowSlider();
        }
        else
        {
            HideSlider();
        }
    }

    void ShowSlider()
    {
        _sliderCanvasGroup.alpha = 1;
        _sliderCanvasGroup.interactable = true;
        _sliderCanvasGroup.blocksRaycasts = true;
    }

    void HideSlider()
    {
        _sliderCanvasGroup.alpha = 0;
        _sliderCanvasGroup.interactable = false;
        _sliderCanvasGroup.blocksRaycasts = false;
    }
}
