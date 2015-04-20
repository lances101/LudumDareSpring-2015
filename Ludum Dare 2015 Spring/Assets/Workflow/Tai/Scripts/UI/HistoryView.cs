using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HistoryView : MonoBehaviour
{
    private GUIController guiController;
    public Image backgroundPanel;
    // Use this for initialization
    void Start()
    {
        guiController = GameController.Instance.GetComponent<GameController>().guiController;
            guiController.historyPanel = backgroundPanel;
        
    }

    public void PlayView()
    {
        guiController.StarGameButton();
    }
}
