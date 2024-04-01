using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BankController : MonoBehaviour
{
    [SerializeField] private GameEvent _onCoinAdded;
    [SerializeField] private TMP_InputField _inputField;
    
    public void AddCoin()
    {
        int coins;
        if(int.TryParse(_inputField.text, out coins))
        {
            _onCoinAdded.Raise(coins);
        }
    }
}
