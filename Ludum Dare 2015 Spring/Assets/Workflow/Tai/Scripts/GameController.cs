using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    [Header("CONTROLLER")]
    public GameObject[] levels;
    private LevelController levelController;
    private GameObject geLevel;

    public GameObject geGuiController;
    private GUIController guiController;
    public Utils utils;

    [Header("SOUNDS")]
    private AudioSource audioSource;
    public AudioClip soundTrack;
    public AudioClip girlFX1;

    private bool flagPause = false;

    void Awake()
    {
        utils = new Utils();
    }
	void Start () {
        geLevel = levels[0];

        Instantiate(geGuiController).transform.parent = this.transform;
        guiController = geGuiController.GetComponent<GUIController>();
	}

    public void PauseGameTime()
    {
        if (!flagPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        flagPause = !flagPause;
    }

    public void StarGame()
    {
        geLevel = Instantiate(geLevel.gameObject);
        geLevel.transform.parent = this.transform;
        geLevel.name = "LevelController";
        levelController = geLevel.GetComponent<LevelController>();

    }

    private void LoadSounds()
    {
        //Carga los sonidos.......
    }

    public void GameOver()
    {
        //Para el juego y muestra la nueva gui, he inicia la vista principal....
    }

    public void Winner()
    {
        //Para el juego y muestra la nueva gui, he inicia la vista principal....
    }

    internal void FinishLevel()
    {
        //Finaliza el nivel y carga la scena final
    }
}
