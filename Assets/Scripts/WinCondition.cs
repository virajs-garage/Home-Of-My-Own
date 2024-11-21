using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public int neededCardAmount;
    public Price[] Prices;
    public Token[] Tokens;

    public UI uiScript;

    [Serializable]
    public class Price
    {
        public string name;
        public CardScript.Resource[] resources;
    }

    [Serializable]
    public class Token
    {
        public string name;
        public bool isPurchased;
        public bool isPurchasable;
    }

    public void CheckToWin()
    {
        int amountPurchased = 0;
        foreach (Token t in Tokens)
        {
            if (t.isPurchased)
            {
                amountPurchased++;
            }
        }
        if(amountPurchased == 3)
        {
            uiScript.TradeInPanel(false);
            uiScript.OpenWinPanel();
        }
    }
    public void BuyToken(int tokenNum)
    {
        Tokens[tokenNum].isPurchased = true;
        uiScript.purchaseButtons[tokenNum].gameObject.SetActive(false);
        uiScript.purchaseButtons[tokenNum + 3].gameObject.SetActive(true);
        CheckToWin();
    }
}
