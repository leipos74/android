using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class CoinsCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private const string CoinsLabelTransalationKey = "coin_label";

    private void Start()
    {
        SystemManager sm = SystemManager.Instance;
        UpdateTextWithCurrentCoins(sm.Coin);
        sm.OnCoinChange += UpdateTextWithCurrentCoins;
    }

    private void UpdateTextWithCurrentCoins(int coin)
    {
        //string text = TransalotionManager.GetString(CoinsLabelTransalationKey, coins.ToString());
        string text = "Coins: " + coin.ToString();
        _text.text = text;
    }

    private void OnDisable()
    {
        SystemManager sm = SystemManager.Instance;
        sm.OnCoinChange -= UpdateTextWithCurrentCoins;

    }
}
