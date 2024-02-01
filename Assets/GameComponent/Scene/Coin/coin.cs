using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    [SerializeField, Min(0)] private int _coinValue = 1;
   public void GetCoin()
    {
        SystemManager sm = SystemManager.Instance;
        sm.ModifyCoins(_coinValue);

        Destroy(gameObject);
    }
}
