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
    private int index;

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

        index = 0;
        ActivateView(index);
    }


    public void ActivateView(int index)
    {
        //Debug.Log("Cargar la vista = " + index);
        int length = views.Count;
        //Debug.Log("Numero de vistas = " + length);

        bool flagView = false;

        for (int i = 0; i < length; i++)
        {
            GameObject geView = (GameObject)views[i];

            if (index == i)
            {
                flagView = true;
            }

            geView.SetActive(flagView);

            flagView = false;
        }
    }
    //----------------------------------------------------------------
    public void InitGameButton()//muestra la  GUI de History
    {
        index = 0;
        ActivateView(index);
    }

    public void HistoryGameButton()//muestra la  GUI de History
    {
        index = 1;
        ActivateView(index);
    }

    public void StarGameButton()//muestra la  GUI de Play
    {
        if (historyPanel && count < 2)
        {
            count++;
            historyPanel.sprite = historySprites[count];
        }
        else
        {
            index = 2;
            Debug.Log("index = " + index);
            count = 0;
            ActivateView(index);
            gameController.StarGame();
        }
    }

    public void PlayGameButton()//muestra la  GUI de Play
    {
        index = 3;
        ActivateView(index);
        gameController.StarGame();
    }

    public void MenuGameButton()//muestra la  GUI de
    {
        Debug.Log("Porque entro aqui");
        if (index == 3)
            index = 2;
        else
            index = 3;

        ActivateView(index);
        gameController.PauseGameTime();
    }

    public void WinnerGameButton()//muestra la  GUI de Init reinician
    {
        index = 0;
        ActivateView(index);
    }

    public void LoserGameButton()//muestra la  GUI de Init reinician
    {
        index = 0;
        ActivateView(index);
    }

    public void RemoveChildGUI(int index)
    {
        throw new System.NotImplementedException();
    }
}
