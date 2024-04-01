using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinController : MonoBehaviour
{
    int coin = 0;
    [SerializeField] private TMP_Text _coinText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCoin(Component sender, object data)
    {
        if (data is int)
        {
            coin += (int) data;
            _coinText.text = $"Coin: {coin}";
        }
    }
}
