using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
[Serializable]
public class HoldableCard
{
    public string Name { get; set; }
    public int Amount { get; set; }
}
[Serializable]
public class Inventory
{
    private Dictionary<string, HoldableCard> cards = new Dictionary<string, HoldableCard>();

    public Board boardScript;

    public void AddCard(string cardName, int amount)
    {
        if (cards.ContainsKey(cardName))
        {
            cards[cardName].Amount += amount;
        }
        else
        {
            HoldableCard newCard = new HoldableCard { Name = cardName, Amount = amount };
            cards.Add(cardName, newCard);
        }
        Debug.Log("Added!");

        boardScript.CheckReadyToBuy();
    }
    public void RemoveCard(string cardName, int amount)
    {
        if (cards.ContainsKey(cardName))
        {
            HoldableCard existingCard = cards[cardName];
            existingCard.Amount -= amount;

            if (existingCard.Amount <= 0)
            {
                cards.Remove(cardName);
            }
        }
    }
    public int GetCardAmount(string cardName)
    {
        if (cards.ContainsKey(cardName))
        {
            return cards[cardName].Amount;
        }
        return 0;
    }
}