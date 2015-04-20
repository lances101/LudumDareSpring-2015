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

    void Awake()
    {
        LoadSceneViews();
    }

    private void LoadSceneViews()
    {
        geInstantiateViews = (GameObject)Instantiate(geViews);
        views = new ArrayList();

        foreach (Transform item in geInstantiateViews.transform)
        {
            if (item.GetComponent<RectTransform>())
            {
                if (item.GetComponent<PlayView>())
                {
                    Debug.Log("PlayView");
                }

                views.Add(item.gameObject);
            }
        }
    }

    void Start()
    {
        gameController = GameObject.Find("GameController(Clone)").GetComponent<GameController>();

        ActivateView("InitView");
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

    public void HistoryGameButton()//muestra la  GUI de History
    {
        ActivateView("HistoryView");
    }

    public void StarGameButton()//muestra la  GUI de Play
    {
        if (historyPanel && count < 2)
        {
            count++;
            historyPanel.overrideSprite = historySprites[count];
        }
        else
        {
            count = 0;
            ActivateView("PlayView");
            gameController.StarGame();
        }
    }


    public void MenuGameButton()//muestra la  GUI de
    {
        ActivateView("MenuGameView");
        gameController.PauseGameTime();
    }

    public void WinnerGameButton()//muestra la  GUI de Init reinician
    {
        ActivateView("WinnerGameView");
    }

    public void LoserGameButton()//muestra la  GUI de Init reinician
    {
        ActivateView("LoserGameView");
    }

    public void RemoveChildGUI(int index)
    {
        throw new System.NotImplementedException();
    }
}
