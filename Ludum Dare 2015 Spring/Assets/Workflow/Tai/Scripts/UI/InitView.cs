using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InitView : MonoBehaviour {
    private GUIController guiController;

	void Start () 
    {
        guiController = GameController.Instance.GetComponent<GameController>().guiController;
	}

    public void CreditsView()
    {
        guiController.CreditsGameButton();
    }
	public void HistoryView ()
	{
	    
        guiController.HistoryGameButton();// vista history
	}
}
