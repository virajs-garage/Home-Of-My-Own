using System.Collections;
using System.Collections.Generic;
using DanielLochner.Assets.SimpleScrollSnap;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Rendering.PostProcessing;

public class MainMenu : MonoBehaviour
{
    public GameObject[] Panels;
    public SimpleScrollSnap KidsCounter;
    public SimpleScrollSnap AdultsCounter;

    public int totalPlayers;

    public GameObject ErrorText;
    Vector3 ErrorTextPos;

    public Transform Canvas;
    public GameObject ClickParticlePrefab;

    public CanvasGroup TutorialPanelCanvas;
    public PostProcessVolume v;
    public DepthOfField d;

    private void Awake()
    {
        v.profile.TryGetSettings(out d);

        if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1)
        {
            Debug.Log("First Time Opening");
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);

            TutorialPanel(true);
        }
        else
        {
            TutorialPanelCanvas.gameObject.SetActive(false);
            Debug.Log("NOT First Time Opening");

        }
    }
    private void Start()
    {
        ErrorText.SetActive(false);
        ErrorTextPos = ErrorText.transform.localPosition;
    }

    public void OnClick(string type)
    {
        //InstantiateClickParticle();

        switch (type)
        {
            case "Play":
                OpenPanel(1);
                break;
            case "Back":
                OpenPanel(0);
                break;
            case "Settings":
                OpenPanel(3);
                break;
            case "Credits":
                OpenPanel(4);
                break;
            case "Start":
                StartGame();
                break;
            case "Color":
                SetPlayerCount();
                break;
            case "0":
                KidsCounter.GoToPreviousPanel();
                break;
            case "1":
                KidsCounter.GoToNextPanel();
                break;
            case "2":
                AdultsCounter.GoToPreviousPanel();
                break;
            case "3":
                AdultsCounter.GoToNextPanel();
                break;
            case "TutorialYes":
                PlayerPrefs.SetInt("DoTutorial", 1);
                TutorialPanelCanvas.gameObject.SetActive(false);
                PlayerPrefs.SetInt("Kids", 1);
                PlayerPrefs.SetInt("Adults", 1);
                GetComponentInParent<Transition>().TransitionStart("Board");
                break;
            case "TutorialNo":
                TutorialPanel(false);
                break;
        }
    }
    public void TutorialPanel(bool show)
    {
        if(show)
        {
            TutorialPanelCanvas.gameObject.SetActive(true);
            LeanTween.value(gameObject, (float val) => { d.focusDistance.value = val; }, 20, 2, .8f);
            LeanTween.value(gameObject, (float val) => { TutorialPanelCanvas.alpha = val; }, 0, 1, .6f);
        }
        else
        {
            LeanTween.value(gameObject, (float val) => { d.focusDistance.value = val; }, 2, 20, .4f).setOnComplete(() => {
                TutorialPanelCanvas.gameObject.SetActive(false);
            });
            LeanTween.value(gameObject, (float val) => { TutorialPanelCanvas.alpha = val; }, 1, 0, .3f);
        }
    }
    public void InstantiateClickParticle()
    {
        Vector3 mousePositionScreen = Input.mousePosition;

        // Convert the mouse position to world coordinates
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(new Vector3(mousePositionScreen.x, mousePositionScreen.y, Canvas.position.z));

        var p = Instantiate(ClickParticlePrefab, mousePositionWorld, Quaternion.identity, Canvas);
        mousePositionWorld.z = -802;
        p.transform.localPosition = mousePositionWorld;
    }
    public void OpenPanel(int panelNum)
    {
        foreach(GameObject p in Panels)
        {
            p.SetActive(false);
        }
        Panels[panelNum].SetActive(true);
    }
    public void StartGame()
    {
        GetComponentInParent<Transition>().TransitionStart("Board");
    }
    public void SetPlayerCount()
    {
        int kids = KidsCounter.SelectedPanel + 1;
        int adults = AdultsCounter.SelectedPanel + 1;

        totalPlayers = kids + adults;

        if (totalPlayers > 4)
        {
            var startingColor = ErrorText.GetComponent<TMP_Text>().color;
            ErrorText.SetActive(true);

            ErrorText.transform.localPosition = ErrorTextPos;
            LeanTween.moveLocalY(ErrorText, ErrorText.transform.localPosition.y + 80, .5f);
            LeanTween.value(ErrorText, (float val) => { ErrorText.GetComponent<TMP_Text>().color = new Color(startingColor.r, startingColor.g, startingColor.b, val); }, 1, 0, .5f);
            return;
        }

        Debug.Log("Kids: " + kids + " Adults: " + adults);

        PlayerPrefs.SetInt("Kids", kids);
        PlayerPrefs.SetInt("Adults", adults);

        OpenPanel(2);
        OpenColorPanel();
    }
    public Transform PlayerHolderParent;
    public GameObject PlayerPrefab;
    public TMP_Text ButtonText;
    public List<GameObject> PlayerColorDisplays = new List<GameObject>();
    public int SelectedColorDisplayNumber = 0;
    public float[] Pos;
    public bool isReadyToPlay;
    public Color[] ColorsList;
    public GameObject[] ColorIsTakenDisplays;
    public Button[] ColorButtons;

    public void OpenColorPanel()
    {
        int kids = PlayerPrefs.GetInt("Kids");
        int adults = PlayerPrefs.GetInt("Adults");
        string playerType;
        for (int i = 0; i < totalPlayers; i++)
        {
            if (kids <= 0)
            {
                adults--;
                playerType = "(CHILD)";
            }
            else
            {
                kids--;
                playerType = "(ADULT)";
            }

            var p = Instantiate(PlayerPrefab, PlayerHolderParent);
            p.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "PLAYER " + (i + 1) + "<br>" + playerType;
            PlayerColorDisplays.Add(p);
        }
        var pos = PlayerHolderParent.transform.localPosition;
        float newX = Pos[totalPlayers - 2];
        PlayerHolderParent.transform.localPosition = new Vector3(newX, pos.y, pos.z);
        SetPlayerColorDisplays();
    }
    public void NextPlayer()
    {
        if (isReadyToPlay)
        {
            StartGame();
            return;
        }
        SelectedColorDisplayNumber++;

        if (SelectedColorDisplayNumber + 1 >= totalPlayers) 
        {
            ButtonText.text = "PLAY";
            isReadyToPlay = true;
        }
        ColorIsTakenDisplays[PlayerPrefs.GetInt("PLAYER " + (SelectedColorDisplayNumber))].SetActive(true);
        ColorButtons[PlayerPrefs.GetInt("PLAYER " + (SelectedColorDisplayNumber))].interactable = false;

        var pos = PlayerHolderParent.transform.localPosition;
        float newX = PlayerHolderParent.transform.localPosition.x - (Pos[totalPlayers - 2] * 2 / (totalPlayers - 1));
        //PlayerHolderParent.transform.localPosition = new Vector3(newX, pos.y, pos.z);
        LeanTween.value(gameObject, (Vector3 pos) => { PlayerHolderParent.transform.localPosition = pos; }, PlayerHolderParent.transform.localPosition, new Vector3(newX, pos.y, pos.z), .5f).setEaseOutBack();

        SetPlayerColorDisplays();
    }
    public void SetPlayerColorDisplays()
    {
        foreach (GameObject p in PlayerColorDisplays)
        {
            if (p != PlayerColorDisplays[SelectedColorDisplayNumber])
            {
                LeanTween.scale(p, new Vector3(.2f, .2f, .2f), .5f);
                Color sc = p.GetComponent<Image>().color;
                LeanTween.value(gameObject, (float val) => { p.GetComponent<Image>().color = new Color(sc.r, sc.g, sc.b, val); }, 1, .2f, .5f);
            }
            else
            {
                LeanTween.scale(p, Vector3.one, .5f);
                Color sc = p.GetComponent<Image>().color;
                LeanTween.value(gameObject, (float val) => { p.GetComponent<Image>().color = new Color(sc.r, sc.g, sc.b, val); }, .2f, 1, .5f);
            }
        }
    }
    [SerializeField]
    public void ClickColor(int ColorNum)
    {
        var Player = PlayerColorDisplays[SelectedColorDisplayNumber];

        Color startingColor = Player.GetComponent<Image>().color;
        LeanTween.value(gameObject, (Color col) => { Player.GetComponent<Image>().color = col; }, startingColor, ColorsList[ColorNum], .2f);

        PlayerPrefs.SetInt("PLAYER " + (SelectedColorDisplayNumber + 1), ColorNum);
        Debug.Log(PlayerPrefs.GetInt("PLAYER " + (SelectedColorDisplayNumber + 1)));
    }
}
