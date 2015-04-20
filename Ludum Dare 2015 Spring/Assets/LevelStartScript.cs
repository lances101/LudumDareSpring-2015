using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelStartScript : MonoBehaviour
{

    private GUIController guiController;
    public GameObject background;

    void Start()
    {
        guiController = GameController.Instance.GetComponent<GameController>().guiController;
        guiController.levelIntroPanel = background.GetComponent<Image>();
    }
    public void GetToLevel()
    {
        GameController.Instance.guiController.GetToLevel();
    }
}
