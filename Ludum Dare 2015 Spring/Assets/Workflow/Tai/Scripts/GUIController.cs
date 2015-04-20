using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    private GameController gameController;

    [Header("Views")]
    public GameObject geViews;
    private GameObject geInstantiateViews;

    private ArrayList views;

    public int count = 0;
    public Sprite[] historySprites;

    public Image historyPanel;

    private ChildrenPanelController ChildPanelController;
    public Image levelIntroPanel;


    void Start()
    {
        LoadSceneViews();
        gameController = GameController.Instance.GetComponent<GameController>();
        gameController.GuiControllerReady(this);
    }

    private void LoadSceneViews()
    {
        geInstantiateViews = (GameObject)Instantiate(geViews);
        views = new ArrayList();

        foreach (Transform item in geInstantiateViews.transform)
        {
            if (item.GetComponent<RectTransform>())
            {
                views.Add(item.gameObject);
            }
        }
    }


    public void ActivateView(string name)
    {
        
        int length = views.Count;
        bool flagView = false;

        for (int i = 0; i < length; i++)
        {
            GameObject geView = (GameObject)views[i];

            if (geView.name == name || (geView.name.Contains("Background") && name != "PlayView"))
            {
                Debug.Log("Showing " +geView.name);
                flagView = true;
            }

            geView.SetActive(flagView);

            flagView = false;
        }
    }
    //----------------------------------------------------------------
    public void InitGameButton()//muestra la  GUI de History
    {
        ActivateView("HistoryView");
    }

    public void ShowGameOver()
    {
        ActivateView("LoserGameView");
    }
    public void HistoryGameButton()//muestra la  GUI de History
    {
        GameController.Instance.audioController.PlayGlobalFX("voice_girl_cry");
        ActivateView("HistoryView");
    }

    public void StarGameButton()//muestra la  GUI de Play
    {
        GameController.Instance.audioController.StopAmbient();
        if (historyPanel && count < 2)
        {
            count++;
            if (count == 1)
            {
                GameController.Instance.audioController.PlayGlobalFX("history_hulla");
            }
            if(count == 2)
                GameController.Instance.audioController.PlayGlobalFX("voice_girl_win");
            historyPanel.overrideSprite = historySprites[count];
        }
        else
        {
            count = 0;
            ActivateView("PlayView");
            ChildPanelController = GameObject.Find("ChildrenPanel").GetComponent<ChildrenPanelController>();
            gameController.ShowLevelIntro(0);
        }
    }

    public void ShowLevelIntro(int level)
    {
        ActivateView("LevelStartView");
        levelIntroPanel.overrideSprite = Resources.Load<Sprite>("Sprites/Days/DAY_" + (level + 1));
        
    }

    public void GetToLevel()
    {
        gameController.ActuallyStartLevel();
    }
    public void MainMenuButton()
    {
        ActivateView("InitView");
    }
    public void MenuGameButton()//muestra la  GUI de
    {
        ActivateView("MenuGameView");
        gameController.PauseGameTime();
    }
    public void ResumeGameButton()//muestra la  GUI de
    {
        ActivateView("PlayView");
        gameController.PauseGameTime();
    }

    public void WinnerGameButton()//muestra la  GUI de Init reinician
    {
        ActivateView("WinnerGameView");
    }

    public void LoserGameButton()//muestra la  GUI de Init reinician
    {
        gameController.RestartLevel();
    }

    public void AddChildGUI(int index, Sprite sprite)
    {
        ChildPanelController.AddNewChild(index, sprite);
    }

    public void UpdateChildGUI(int index, Sprite sprite)
    {
        ChildPanelController.UpdateChild(index, sprite);
    }
    public void RemoveChildGUI(int index)
    {
        ChildPanelController.RemoveChild(index);
    }

    public void ClearChildGUI()
    {
        
        ChildPanelController.ClearChildGUI();
    }

    public void CreditsGameButton()
    {
        ActivateView("CreditsView");
    }
}
