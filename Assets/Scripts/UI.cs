using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public Board board;
    public CardScript cs;
    public CameraMove cam;
    public SoundController sfx;

    public Button RollButton;
    public TMP_Text MoveCounter;

    public TMP_Text CardCounter;

    public GameObject Canvas;
    public GameObject CardPrefab;

    public GameObject OpenCard;

    public GameObject ResultsPanel;
    public Image ResultsPanelBG;
    public GameObject Collect;
    public TMP_Text C_Letter;
    public TMP_Text C_Name;
    public TMP_Text C_Ammount;
    public GameObject Loss;
    public TMP_Text L_CouldHaveHappenedResult;
    public TMP_Text L_ActuallyHappenedResult;


    public void OnClick(string click)
    {
        switch (click)
        {
            case "Roll":
                board.CheckToRoll("Move");
                break;
            case "Roll_Card":
                board.CheckToRoll("Card");
                break;
            case "ToggleCamera":
                if (cam.CurrentPosition == CameraMove.CameraPositions.MainBoard)
                {
                    cam.MoveCamera(CameraMove.CameraPositions.Player);
                }
                else if (cam.CurrentPosition == CameraMove.CameraPositions.Player)
                {
                    cam.MoveCamera(CameraMove.CameraPositions.MainBoard);
                }
                break;
            case "CloseResults":
                sfx.PlaySound(0, 0);
                CloseResultsPanel();
                cam.MoveCamera(CameraMove.CameraPositions.MainBoard);
                SetRollButton(true);
                if (board.ts.isTutorialStarted)
                {
                    board.ts.IncrementStep();
                }
                break;
            case "ClockWise":
                sfx.PlaySound(0, 0);
                board.currentPlayer.MoveDirection(true);
                break;
            case "CounterClockWise":
                sfx.PlaySound(0, 0);
                board.currentPlayer.MoveDirection(false);
                break;
            case "OpenTradeInPanel":
                sfx.PlaySound(0, 0);
                if (board.ts.isTutorialStarted && board.ts.stepNumber == 18)
                {
                    board.ts.IncrementStep();
                }
                TradeInPanel(true);
                break;
            case "CloseTradeInPanel":
                sfx.PlaySound(0, 0);
                if (board.ts.isTutorialStarted && board.ts.stepNumber == 19)
                {
                    board.ts.IncrementStep();
                }
                TradeInPanel(false);
                break;
            case "OpenInventoryPanel":
                sfx.PlaySound(0, 0);
                if (board.ts.isTutorialStarted && board.ts.stepNumber == 15)
                {
                    board.ts.IncrementStep();
                }
                InventoryPanel(true);
                break;
            case "CloseInventoryPanel":
                sfx.PlaySound(0, 0);
                if (board.ts.isTutorialStarted && board.ts.stepNumber == 17)
                {
                    board.ts.IncrementStep();
                }
                InventoryPanel(false);
                break;
            case "WinGameTest":
                sfx.PlaySound(0, 0);
                OpenWinPanel();
                break;
            case "ContinueWin":
                sfx.PlaySound(0, 0);
                GetComponentInParent<Transition>().TransitionStart("MainMenu");
                break;
        }
    }
    public void SetMoveText(int amm)
    {
        MoveCounter.text = "MOVES: " + amm;
    }
    public void SetRollButton(bool isTrue)
    {
        RollButton.interactable = isTrue;
    }
    public void ShowCard(CardScript.Resource rs)
    {
        var card = Instantiate(CardPrefab, Canvas.transform);
        OpenCard = card;
        LeanTween.value(card, (float val) => { card.transform.localScale = new Vector3(val, val, val); }, 0, 1, .5f).setEaseOutBack();
        cs.DrawAndSetCard(card, rs);
        Debug.Log(board.ts.stepNumber);
        if (board.ts.stepNumber == 9)
        {
            card.transform.GetChild(4).GetComponent<Button>().interactable = false;
        }
    }
    public void CloseCard()
    {
        var card = OpenCard;
        LeanTween.value(card, (float val) => { card.transform.localScale = new Vector3(val, val, val); }, 1, 0, .5f).setEaseInBack();
        OpenCard = null;
    }
    public void UpdateCardDisplay()
    {
        Debug.Log("UPDATE!");
        CardCounter.text = string.Format("FBFP: {0} WORK: {1} MEAL: {2} SHELTER: {3} CLOTHING: {4}",
            board.GroupInventory.GetCardAmount(CardScript.Resource.FBFP.ToString()),
            board.GroupInventory.GetCardAmount(CardScript.Resource.WorkSchool.ToString()),
            board.GroupInventory.GetCardAmount(CardScript.Resource.Food.ToString()),
            board.GroupInventory.GetCardAmount(CardScript.Resource.Shelter.ToString()),
            board.GroupInventory.GetCardAmount(CardScript.Resource.Clothing.ToString()));
    }
    public void unsetButton(Button button)
    {
        button.interactable = false;
    }
    public void OpenResultsPanel(CardScript.Card card, bool isKeep)
    {
        ResultsPanel.SetActive(true);
        LeanTween.value(ResultsPanel, (float val) => { ResultsPanel.transform.localScale = new Vector3(val, val, val); }, 0, 1, .6f).setEaseOutBack();
        Color before = ResultsPanelBG.color;
        LeanTween.value(ResultsPanelBG.gameObject, (float val) => { ResultsPanelBG.color = new Color(before.r, before.g, before.b, val); }, 0, .945f, 1f);

        Collect.SetActive(isKeep);
        Loss.SetActive(!isKeep);
        if (!isKeep) 
        {
            sfx.PlaySound(6, 0);
        }

        C_Letter.text = cs.AbvNames[(int)card.Resource];
        C_Name.text = cs.CardNames[(int)card.Resource];
        C_Ammount.text = string.Format("1x {0} Card", card.Resource.ToString());

        L_CouldHaveHappenedResult.text = card.All_KeepResult;
        L_ActuallyHappenedResult.text = card.All_DiscardResult;
    }
    public void CloseResultsPanel()
    {
        Color before = ResultsPanelBG.color;
        LeanTween.value(ResultsPanelBG.gameObject, (float val) => { ResultsPanelBG.color = new Color(before.r, before.g, before.b, val); }, .945f, 0, .3f);
        LeanTween.value(ResultsPanel, (float val) => { ResultsPanel.transform.localScale = new Vector3(val, val, val); }, 1, 0, .4f).setEaseInBack().setOnComplete(() => { ResultsPanel.SetActive(true); });

        board.SetNextPlayer();
    }
    public GameObject WinPanelObject;
    public Image WinPanelBg;
    public void OpenWinPanel()
    {
        WinPanelObject.SetActive(true);
        LeanTween.value(WinPanelObject, (float val) => { WinPanelObject.transform.localScale = new Vector3(val, val, val); }, 0, 1, .6f).setEaseOutBack();
        Color before = WinPanelBg.color;
        LeanTween.value(WinPanelBg.gameObject, (float val) => { WinPanelBg.color = new Color(before.r, before.g, before.b, val); }, 0, .945f, 1f);
        sfx.PlaySound(10, 0);
    }
    public GameObject ArrowButtonsHolder;
    public float HideArrowMoveAmmount;
    public bool isShowingArrow = true;
    public void ShowUiLogic()
    {
        var pos = cam.CurrentPosition;
        switch (pos)
        {
            case CameraMove.CameraPositions.Dice:
                MoveArrows(false); //Hide
                break;
            case CameraMove.CameraPositions.MainBoard:
                if (board.spacesToMove != 0)
                {
                    MoveArrows(true); //Show
                }
                else
                {
                    MoveArrows(false); //Hide
                }
                break;
            case CameraMove.CameraPositions.Player:
                MoveArrows(false); //Hide
                break;
        }
    }
    public void MoveArrows(bool show)
    {
        if (isShowingArrow == show)
        {
            return;
        }
        isShowingArrow = show;
        if (show)
        {
            ArrowButtonsHolder.SetActive(true);
        }
        Vector3 pos = new Vector3(ArrowButtonsHolder.transform.localPosition.x, ArrowButtonsHolder.transform.localPosition.y + (show ? HideArrowMoveAmmount : -HideArrowMoveAmmount), ArrowButtonsHolder.transform.localPosition.z);
        LeanTween.moveLocal(ArrowButtonsHolder, pos, .3f).setEaseInBack();
        LeanTween.value(ArrowButtonsHolder, (float val) => { ArrowButtonsHolder.GetComponent<CanvasGroup>().alpha = val; }, show ? 0 : 1, show ? 1 : 0, .35f).setEaseInBack().setOnComplete(() => {
            if (!show)
            {
                ArrowButtonsHolder.SetActive(false);
            }
        }
        );
    }
    public bool tradeInPanelOpen;
    public GameObject TradeInPanelObj;
    public Transform tradeInPanel;
    public Image panelBackgroundColor;
    public Button[] purchaseButtons;
    public void TradeInPanel(bool show)
    {
        for (int i = 0; i < board.winCondition.Tokens.Length; i++)
        {
            bool isPurchasable = board.winCondition.Tokens[i].isPurchasable;
            var button = purchaseButtons[i];
            button.interactable = isPurchasable;
            button.transform.GetChild(1).GetComponent<TMP_Text>().color = isPurchasable ? Color.green : Color.red;
            if (board.winCondition.Tokens[i].isPurchased)
            {
                button.gameObject.SetActive(false);
                purchaseButtons[i+3].gameObject.SetActive(true);
            }
            else
            {
                button.gameObject.SetActive(true);
                purchaseButtons[i + 3].gameObject.SetActive(false);
            }
        }

        tradeInPanelOpen = !tradeInPanelOpen;
        if (show)
        {
            TradeInPanelObj.SetActive(true);
            LeanTween.value(gameObject, (float val) => { tradeInPanel.localScale = new Vector3(val, val, val); }, show ? 0 : 1, show ? 1 : 0, .3f).setEaseOutBack();
        }
        else
        {
            LeanTween.value(gameObject, (float val) => { tradeInPanel.localScale = new Vector3(val, val, val); }, show ? 0 : 1, show ? 1 : 0, .3f).setEaseInBack();
        }

        var color = panelBackgroundColor.color;
        LeanTween.value(gameObject, (float val) => { panelBackgroundColor.color = new Color(color.r, color.g, color.b, val); }, show ? 0 : 0.972549f, show ? 0.972549f : 0, .5f).setOnComplete(() => {
            if (!show)
            {
                TradeInPanelObj.SetActive(false);
            }
        }
        );
    }
    public bool InventoryPanelOpen;
    public GameObject InventoryPanelParentObj;
    public Transform inventoryPanel;
    public Image inventoryPanelBackgroundColor;
    public Transform CardHolder;
    public void InventoryPanel(bool show)
    {
        foreach (Transform child in CardHolder)
        {
            var resource = CardScript.Resource.None;
            switch (child.name)
            {
                case "Clothing Bedding":
                    resource = CardScript.Resource.Clothing;
                    break;
                case "Shelter":
                    resource = CardScript.Resource.Shelter;
                    break;
                case "Meals":
                    resource = CardScript.Resource.Food;
                    break;
                case "Work School":
                    resource = CardScript.Resource.WorkSchool;
                    break;
                case "FBFP":
                    resource = CardScript.Resource.FBFP;
                    break;
            }
            var amount = board.GroupInventory.GetCardAmount(resource.ToString());

            child.GetChild(3).GetComponent<TMP_Text>().text = "x" + amount;
        }

        InventoryPanelOpen = !InventoryPanelOpen;
        if (show)
        {
            InventoryPanelParentObj.SetActive(true);
            LeanTween.value(gameObject, (float val) => { inventoryPanel.localScale = new Vector3(val, val, val); }, show ? 0 : 1, show ? 1 : 0, .3f).setEaseOutBack();
        }
        else
        {
            LeanTween.value(gameObject, (float val) => { inventoryPanel.localScale = new Vector3(val, val, val); }, show ? 0 : 1, show ? 1 : 0, .3f).setEaseInBack();
        }

        var color = inventoryPanelBackgroundColor.color;
        LeanTween.value(gameObject, (float val) => { inventoryPanelBackgroundColor.color = new Color(color.r, color.g, color.b, val); }, show ? 0 : 0.972549f, show ? 0.972549f : 0, .5f).setOnComplete(() => {
            if (!show)
            {
                InventoryPanelParentObj.SetActive(false);
            }
        }
        );
    }
}
