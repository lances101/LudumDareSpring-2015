using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {
    [Header("Views")]
    public GameObject initGameView;
    public GameObject historyGameView;
    public GameObject playGameView;
    public GameObject menuGameView;
    public GameObject winnerGameView;
    public GameObject loserGameView;

    private ArrayList views;
    private int index;
    
    void Awake()
    {
        LoadSceneViews();
    }

    private void LoadSceneViews()
    {
        views = new ArrayList();
        views.Add(initGameView);
        views.Add(historyGameView);
        views.Add(playGameView);
        views.Add(menuGameView);
        views.Add(winnerGameView);
        views.Add(loserGameView);
    }

	void Start () {
        index = 0;
        ActivateView(index);
	}
	
	// Update is called once per frame
    public void ActivateView(int index)
    {
        int length = views.Count;
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
}
