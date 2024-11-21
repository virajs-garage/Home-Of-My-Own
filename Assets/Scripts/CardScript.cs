using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public UI ui;
    public Board board;
    [Serializable]
    public class Card
    {
        public Resource Resource;
        public int FBFPCardRequirement;
        public string All_KeepResult;
        public string Kids_KeepResult;
        public string All_DiscardResult;
        public bool isCardForAdults;
        public int NumberOfCards;
        public bool ExtraTurn;
    }
    public enum Resource
    {
        None,
        Food,
        Shelter,
        Clothing,
        WorkSchool,
        FBFP,
    }
    [NonSerialized]
    public List<string> AbvNames = new List<string>
    {
        "",
        "M",
        "S",
        "C",
        "W",
        "FBFP",
    };
    [NonSerialized]
    public List<string> CardNames = new List<string>
    {
        "",
        "MEALS",
        "SHELTER",
        "CLOTHING<br>&<br>BEDDING",
        "WORK<br>&<br>SCHOOL",
        "FORT<br>BEND<br>FAMILY<br>PROMISE",
    };
    public void DrawAndSetCard(GameObject card, Resource resource)
    {
        var pile = new List<Card>();
        foreach(Card c in CardList)
        {
            if(c.Resource == resource)
            {
                pile.Add(c);
            }
        }
        var PickedCard = pile[UnityEngine.Random.Range(0, pile.Count)];
        board.RollingForCard = PickedCard;

        var change = card.GetComponent<CardChangeables>();
        change.RollForText.text = "ROLL THE DICE FOR " + ((resource != Resource.WorkSchool) ? resource.ToString().ToUpper() : "WORK / SCHOOL");
        change.Letter.text = AbvNames[(int)resource];
        change.Even.text = "KEEP: " + PickedCard.All_KeepResult;
        change.Odd.text = "DISCARD: " + PickedCard.All_DiscardResult;
        change.RollButton.onClick.AddListener(() => { ui.OnClick("Roll_Card"); });
        change.RollButton.onClick.AddListener(() => { ui.unsetButton(change.RollButton); });
    }
    private List<Card> CardList = new List<Card>
    {
    // Food cards
    new Card
    {
        Resource = Resource.Food,
        FBFPCardRequirement = 0,
        All_KeepResult = "Penny's pantry lets you stop by and get the food that your kids will actually eat.",
        Kids_KeepResult = "",
        All_DiscardResult = "You have nowhere to store your food.",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.Food,
        FBFPCardRequirement = 0,
        All_KeepResult = "A hot fresh dinner is provided by a congregation.",
        Kids_KeepResult = "",
        All_DiscardResult = "You made it too late to pick up.",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.Food,
        FBFPCardRequirement = 0,
        All_KeepResult = "Dinner is provided by a Harvest Network restaurant, like Chick-Fil-A, Outback, etc.",
        Kids_KeepResult = "",
        All_DiscardResult = "You don't have a way to heat up your food.",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.Food,
        FBFPCardRequirement = 0,
        All_KeepResult = "Receive Fresh food from Second Mile.",
        Kids_KeepResult = "",
        All_DiscardResult = "You have nowhere to store your food.",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.Food,
        FBFPCardRequirement = 0,
        All_KeepResult = "Receive Fresh food from Helping Hands.",
        Kids_KeepResult = "",
        All_DiscardResult = "You have nowhere to store your food.",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.Food,
        FBFPCardRequirement = 0,
        All_KeepResult = "Stock up on bread, sardines, and pizza donated by Whole Foods.",
        Kids_KeepResult = "",
        All_DiscardResult = "You don't have a way to heat up your food.",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.Food,
        FBFPCardRequirement = 0,
        All_KeepResult = "Dinner is provided by a Harvest Network restaurant, like Chick-Fil-A, Outback, etc.",
        Kids_KeepResult = "",
        All_DiscardResult = "You have nowhere to store your food.",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
      new Card
    {
        Resource = Resource.Food,
        FBFPCardRequirement = 0,
        All_KeepResult = "Visit a food bank.",
        Kids_KeepResult = "",
        All_DiscardResult = "You have nowhere to store your food.",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.Food,
        FBFPCardRequirement = 0,
        All_KeepResult = "A hot fresh dinner is provided by a congregation.",
        Kids_KeepResult = "",
        All_DiscardResult = "You made it too late to pick up",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    
    // Shelter cards
    new Card
    {
        Resource = Resource.Shelter,
        FBFPCardRequirement = 3,
        All_KeepResult = "Qualify for A Home of My Own - receive a check for your 1st month's rent!",
        Kids_KeepResult = "",
        All_DiscardResult = "They have no way to reach you.",
        isCardForAdults = true,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.Shelter,
        FBFPCardRequirement = 3,
        All_KeepResult = "Project housewarming has a lamp for you.",
        Kids_KeepResult = "",
        All_DiscardResult = "You don't have anywhere to store it.",
        isCardForAdults = true,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.Shelter,
        FBFPCardRequirement = 3,
        All_KeepResult = "Qualify for A Home of My Own - receive a check for your 1st month's rent!",
        Kids_KeepResult = "",
        All_DiscardResult = "They have no way to reach you.",
        isCardForAdults = true,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.Shelter,
        FBFPCardRequirement = 0,
        All_KeepResult = "You have a lead on an affordable apartment after working with our Housing Navigators.",
        Kids_KeepResult = "",
        All_DiscardResult = "You do not have time to file and submit the paperwork.",
        isCardForAdults = true,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.Shelter,
        FBFPCardRequirement = 0,
        All_KeepResult = "Register with Rapid Rehousing to work on getting support for placement.",
        Kids_KeepResult = "",
        All_DiscardResult = "You ran out of cell phone minutes.",
        isCardForAdults = true,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.Shelter,
        FBFPCardRequirement = 1,
        All_KeepResult = "Register with Raise Up Families to qualify for rent assistance for 8 months",
        Kids_KeepResult = "",
        All_DiscardResult = "You aren't currently employed.",
        isCardForAdults = true,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.Shelter,
        FBFPCardRequirement = 1,
        All_KeepResult = "Start working with Mainstream Services to have Catholic Charities & Baker Ripley contact you with assistance.",
        Kids_KeepResult = "",
        All_DiscardResult = "You ran out of cell phone minutes.",
        isCardForAdults = true,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.Shelter,
        FBFPCardRequirement = 1,
        All_KeepResult = "Qualify for assistance with the Housing Authority.",
        Kids_KeepResult = "",
        All_DiscardResult = "You don't have time to file and submit the paperwork.",
        isCardForAdults = true,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.Shelter,
        FBFPCardRequirement = 1,
        All_KeepResult = "Qualify with Abigail's Place for a 3 month stay. This is for women only!",
        Kids_KeepResult = "",
        All_DiscardResult = "You need a place for your spouse.",
        isCardForAdults = true,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.Shelter,
        FBFPCardRequirement = 1,
        All_KeepResult = "Qualify with Abigail's Place for a 3 month stay. This is for women only!",
        Kids_KeepResult = "",
        All_DiscardResult = "You need a place for your spouse.",
        isCardForAdults = true,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    
    // Clothing/Warmth cards
    new Card
    {
        Resource = Resource.Clothing,
        FBFPCardRequirement = 0,
        All_KeepResult = "Receive new pillows from a congregation.",
        Kids_KeepResult = "",
        All_DiscardResult = "Try moving one more time!",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = true
    },
    new Card
    {
        Resource = Resource.Clothing,
        FBFPCardRequirement = 0,
        All_KeepResult = "Receive a blanket donated by a congregation.",
        Kids_KeepResult = "",
        All_DiscardResult = "Try moving one more time!",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = true
    },
    new Card
    {
        Resource = Resource.Clothing,
        FBFPCardRequirement = 0,
        All_KeepResult = "Receive a blanket purchased by Family Promise.",
        Kids_KeepResult = "",
        All_DiscardResult = "Try moving one more time!",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = true
    },
    new Card
    {
        Resource = Resource.Clothing,
        FBFPCardRequirement = 1,
        All_KeepResult = "'Now I lay me down to sleep' has a blanket, plush, and book for you",
        Kids_KeepResult = "",
        All_DiscardResult = "Try moving one more time!",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = true
    },
    new Card
    {
        Resource = Resource.Clothing,
        FBFPCardRequirement = 0,
        All_KeepResult = "Houston Threads gets you some cool, brand new Nike shoes.",
        Kids_KeepResult = "",
        All_DiscardResult = "You are not registered for the program",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.Clothing,
        FBFPCardRequirement = 0,
        All_KeepResult = "Receive clothing provided by Clothed by Faith.",
        Kids_KeepResult = "",
        All_DiscardResult = "Try moving one more time!",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = true
    },
    new Card
    {
        Resource = Resource.Clothing,
        FBFPCardRequirement = 1,
        All_KeepResult = "Receive an air matress.",
        Kids_KeepResult = "",
        All_DiscardResult = "Try moving one more time!",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = true
    },
    new Card
    {
        Resource = Resource.Clothing,
        FBFPCardRequirement = 0,
        All_KeepResult = "\"Project Housewarming\" gets you a nice set of sheets.",
        Kids_KeepResult = "",
        All_DiscardResult = "Try moving one more time!",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = true
    },
    new Card
    {
        Resource = Resource.Clothing,
        FBFPCardRequirement = 0,
        All_KeepResult = "Made it to the Dept. of Health and Human Services to upgrade Medicare.",
        Kids_KeepResult = "",
        All_DiscardResult = "Try moving one more time!",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = true
    },
    
    // Work/School cards
    new Card
    {
        Resource = Resource.WorkSchool,
        FBFPCardRequirement = 2,
        All_KeepResult = "Parents, thanks to Project Gas C.A.P., Receive an uber card for rides to work.",
        Kids_KeepResult = "Kids, YEP! (Youth empowerment planning) gets you to your music lessons",
        All_DiscardResult = "Car stopped working",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.WorkSchool,
        FBFPCardRequirement = 2,
        All_KeepResult = "Parents, file for McKinney-Vento to receive help.",
        Kids_KeepResult = "Kids, YEP! (Youth empowerment planning) gets you to band practive using UBER.",
        All_DiscardResult = "You have a scheduling conflict",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
     new Card
    {
        Resource = Resource.WorkSchool,
        FBFPCardRequirement = 2,
        All_KeepResult = "Parents, you got a lead for a job with the help of Workforce Solutions!",
        Kids_KeepResult = "Kids, YEP! (Youth empowerment planning) makes sure you get a new haircut.",
        All_DiscardResult = "You have a scheduling conflict",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.WorkSchool,
        FBFPCardRequirement = 1,
        All_KeepResult = "Parents, you received some great financial coaching with the help of THRIVE.",
        Kids_KeepResult = "Kids, YEP! (Youth empowerment planning) helps rent an instrument for band class.",
        All_DiscardResult = "Your car stopped working",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.WorkSchool,
        FBFPCardRequirement = 1,
        All_KeepResult = "Parents, you received an affordable loan for a car. with On the Road Lending.",
        Kids_KeepResult = "Kids, YEP! (Youth empowerment planning) gets you basketball practice.",
        All_DiscardResult = "You have a scheduling conflict",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.WorkSchool,
        FBFPCardRequirement = 2,
        All_KeepResult = "Parents,THRIVE helps you file your taxes",
        Kids_KeepResult = "Kids, YEP! (Youth empowerment planning) pays your camp tuition.",
        All_DiscardResult = "You have a scheduling conflict",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.WorkSchool,
        FBFPCardRequirement = 2,
        All_KeepResult = "Parents, you got a lead for a job with the help of Worforce Solutions!",
        Kids_KeepResult = "Kids, YEP! (Youth empowerment planning) makes sure you get a new haircut,",
        All_DiscardResult = "You have a scheduling conflict",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.WorkSchool,
        FBFPCardRequirement = 1,
        All_KeepResult = "Parents, you were able to get a tax refund with the help of THRIVE.",
        Kids_KeepResult = "Kids, YEP! (Youth empowerment planning) gets you a laptop to do your homework",
        All_DiscardResult = "Try moving one more time!",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = true
    },
    new Card
    {
        Resource = Resource.WorkSchool,
        FBFPCardRequirement = 2,
        All_KeepResult = "Parents, thanks to Project Gas C.A.P., you can fix your car.",
        Kids_KeepResult = "Kids, YEP! (Youth empowerment planning) gets you a laptop to do your homework",
        All_DiscardResult = "Try moving one more time!",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = true
    },
    
    // FBFP cards
    new Card
    {
        Resource = Resource.FBFP,
        FBFPCardRequirement = 0,
        All_KeepResult = "You fill out SNAP paperwork.",
        Kids_KeepResult = "",
        All_DiscardResult = "Have to work late",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.FBFP,
        FBFPCardRequirement = 0,
        All_KeepResult = "You have register with Mckinney-Vento so your kids can stay at their school regardless of home address.",
        Kids_KeepResult = "",
        All_DiscardResult = "You don't have all your paperwork.",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.FBFP,
        FBFPCardRequirement = 0,
        All_KeepResult = "You fill out your CHIP/Medicaid paperwork.",
        Kids_KeepResult = "",
        All_DiscardResult = "There was a scheduling conflict.",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.FBFP,
        FBFPCardRequirement = 0,
        All_KeepResult = "You have your driver's license.",
        Kids_KeepResult = "",
        All_DiscardResult = "Driver's License has expired.",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.FBFP,
        FBFPCardRequirement = 0,
        All_KeepResult = "You have Social Security Card.",
        Kids_KeepResult = "",
        All_DiscardResult = "You lost your Social Security card in the flood.",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.FBFP,
        FBFPCardRequirement = 0,
        All_KeepResult = "You have your child's custody papers.",
        Kids_KeepResult = "",
        All_DiscardResult = "Try moving one more time!",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = true
    },
    new Card
    {
        Resource = Resource.FBFP,
        FBFPCardRequirement = 0,
        All_KeepResult = "You have a copy of your bank statement.",
        Kids_KeepResult = "",
        All_DiscardResult = "You have no bank account.",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.FBFP,
        FBFPCardRequirement = 0,
        All_KeepResult = "ou have a copy of your last three paychecks.",
        Kids_KeepResult = "",
        All_DiscardResult = "You lost your job.",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.FBFP,
        FBFPCardRequirement = 0,
        All_KeepResult = "You have a copy of Marriage License.",
        Kids_KeepResult = "",
        All_DiscardResult = "Try moving one more time!",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = true
    },
    new Card
    {
        Resource = Resource.FBFP,
        FBFPCardRequirement = 0,
        All_KeepResult = "Enroll in United Way's THRIVE program, helping you find a job and learn finance skills.",
        Kids_KeepResult = "",
        All_DiscardResult = "Out of cell phone minutes.",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.FBFP,
        FBFPCardRequirement = 0,
        All_KeepResult = "Register with Second Mile Food Pantry",
        Kids_KeepResult = "",
        All_DiscardResult = "Try moving one more time!",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = true
    },
    new Card
    {
        Resource = Resource.FBFP,
        FBFPCardRequirement = 0,
        All_KeepResult = "Register with Helping Hands Food Pantry",
        Kids_KeepResult = "",
        All_DiscardResult = "Try moving one more time!",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = true
    },
    new Card
    {
        Resource = Resource.FBFP,
        FBFPCardRequirement = 0,
        All_KeepResult = "Register with \"A Way Home\" with the Coalition for the Homeless.  There they will match you with different agencies that can help you.",
        Kids_KeepResult = "",
        All_DiscardResult = "Try moving one more time!",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = true
    },
    new Card
    {
        Resource = Resource.FBFP,
        FBFPCardRequirement = 0,
        All_KeepResult = "Register with \"On the Road Lending\"",
        Kids_KeepResult = "",
        All_DiscardResult = "You haven't registered yet.",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.FBFP,
        FBFPCardRequirement = 0,
        All_KeepResult = "Get a copy of your COVID-19 test, which is required by FBFP.",
        Kids_KeepResult = "",
        All_DiscardResult = "You haven't been vaccinated yet.",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.FBFP,
        FBFPCardRequirement = 0,
        All_KeepResult = "Enroll all adults in medical care with Access Health.",
        Kids_KeepResult = "",
        All_DiscardResult = "You haven't registered yet.",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.FBFP,
        FBFPCardRequirement = 0,
        All_KeepResult = "You have children's birth certificates..",
        Kids_KeepResult = "",
        All_DiscardResult = "You relocated and lost track.",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.FBFP,
        FBFPCardRequirement = 0,
        All_KeepResult = "You have your children's immunization records.",
        Kids_KeepResult = "",
        All_DiscardResult = "The doctor's office is closed.",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = false
    },
    new Card
    {
        Resource = Resource.FBFP,
        FBFPCardRequirement = 0,
        All_KeepResult = "You have children's school information.",
        Kids_KeepResult = "",
        All_DiscardResult = "Try moving one more time!",
        isCardForAdults = false,
        NumberOfCards = 1,
        ExtraTurn = true
    },

    };
}