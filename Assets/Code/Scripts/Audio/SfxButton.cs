using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxButton : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void OnHoverEnter()
    {
            AudioManager.Instance.PlaySound2D("Hover"); 
        
    }

    public void OnClick()
    {
            AudioManager.Instance.PlaySound2D("Click"); 
    }
}
