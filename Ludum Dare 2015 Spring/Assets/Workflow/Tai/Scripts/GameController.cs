using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameController()
    {
        GameController.Instance = this;
    }
    public static GameController Instance { get; private set; }

    [Header("Prefabs")]
    public GameObject goAudioController;
    public GameObject geGuiController;

    [Header("CONTROLLER")]
    public AudioController audioController;
    public GUIController guiController;
    public LevelController LevelController;


    private GameObject currentLevelGO;
    private int currentLevel = 0;
    
    public GameObject[] levels;

    private void Awake()
    {
        var go = Instantiate(geGuiController);

        go.transform.SetParent(transform);

        go = Instantiate(goAudioController);
        go.transform.SetParent(transform);
        audioController = go.GetComponent<AudioController>();
    }

    
    public void GuiControllerReady(GUIController gc)
    {
        guiController = gc;
        guiController.ActivateView("InitView");
    }

    public void AudioControllerReady()
    {
        
        audioController.PlayGlobalFX("music_intro");
    }

    public void PauseGame(bool shouldPause)
    {
        if (shouldPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ShowLevelIntro(int i)
    {
        PauseGame(false);
        DestroyObject(currentLevelGO);
        guiController.ClearChildGUI();
        audioController.PlayTheme("music_tiffany");
        audioController.PlayAmbient("ambient_kids");
        currentLevel = i;
        currentLevelGO = Instantiate(levels[currentLevel]);
        currentLevelGO.transform.parent = transform;
        currentLevelGO.name = "LevelController";
        LevelController = currentLevelGO.GetComponent<LevelController>();
        guiController.ShowLevelIntro(currentLevel);

        
    }
    public void ActuallyStartLevel()
    {
        
        guiController.ActivateView("PlayView");
        
    }
    public void GameOver()
    {
        PauseGame(true);
        audioController.StopAmbient();
        guiController.ShowGameOver();
    }

    public void Winner()
    {
        guiController.ActivateView("WinnerGameView");
        audioController.StopAmbient();
        audioController.PlayTheme("history_teaparty");
        audioController.PlayGlobalFX("voice_girl_win");
        
    }

    internal void FinishLevel()
    {
        audioController.PlayGlobalFX("voice_girl_win");
        currentLevel++;
        if (currentLevel == levels.Length)
        {
            Winner();
        }
        else
            ShowLevelIntro(currentLevel);
        PauseGame(true);
    }

    public void RestartLevel()
    {
        ShowLevelIntro(currentLevel);
    }
}