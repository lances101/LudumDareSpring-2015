using UnityEngine;
using System.Collections;

public class CreditsView : MonoBehaviour {

	// Use this for initialization
    public void ChangeView()
    {
        GameController.Instance.guiController.MainMenuButton();
    }
}
