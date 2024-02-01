using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager
{
    private static SystemManager instance;
    public static SystemManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SystemManager();
            }
            return instance;
        }
    }

    private const string CoinKey = "Coins: ";
    private int _coin = 0;


    public delegate void CoinChange(int coin);
    public event CoinChange OnCoinChange;

    public int Coin
    {
        get => _coin;

    }

    public bool ModifyCoins(int value)
    {
        if (Coin + value < 0)
        {
            return false;
        }
        _coin += value;
        OnCoinChange?.Invoke(_coin);

        return true;
    }

    private SystemManager()
    {
        LoadData();
    }

    private void LoadData()
    {
        LoadCoins();
    }

    private void LoadCoins()
    {
        _coin = PlayerPrefs.GetInt(CoinKey, 0);
    }
}
