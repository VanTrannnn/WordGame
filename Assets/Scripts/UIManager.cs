using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header(" Elements ")]
    [SerializeField] private CanvasGroup MenuCG;
    [SerializeField] private CanvasGroup gameCG;
    [SerializeField] private CanvasGroup levelCompleteCG;
    [SerializeField] private CanvasGroup gameoverCG;
    [SerializeField] private CanvasGroup settingsCG;

    [Header(" Menu Elements ")]
    [SerializeField] private TextMeshProUGUI menuCoins;
    [SerializeField] private TextMeshProUGUI menuBestScore;

    [Header(" Level Complete Elements ")]
    [SerializeField] private TextMeshProUGUI levelCompleteCoins;
    [SerializeField] private TextMeshProUGUI levelCompleteSerectWord;
    [SerializeField] private TextMeshProUGUI levelCompleteScore;
    [SerializeField] private TextMeshProUGUI levelCompleteBestScore;

    [Header(" Gameover Elements ")]
    [SerializeField] private TextMeshProUGUI gameoverCoins;
    [SerializeField] private TextMeshProUGUI gameoverSerectWord;
    [SerializeField] private TextMeshProUGUI gameoverBestScore;

    [Header(" Game Elements ")]
    [SerializeField] private TextMeshProUGUI gameScore;
    [SerializeField] private TextMeshProUGUI gameCoins;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowMenu();
        HideGame();
        HideLevelComplete();
        HideGameover();
        GameManager.onGameStateChanged += GameStateChangedCallback;
        DataManager.onCoinsUpdated += UpdateCoinsTexts;
    }
    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
        DataManager.onCoinsUpdated -= UpdateCoinsTexts;

    }
    private void GameStateChangedCallback(GameState gameState)
    {
        switch(gameState){
            case GameState.Menu:
                ShowMenu();
                HideGame();
                break;

            case GameState.Game:
                ShowGame();
                HideMenu();
                HideLevelComplete();
                HideGameover();
                break;

            case GameState.LevelComplete:
                ShowLevelComplete();
                HideGame();
                break;

            case GameState.Gameover:
                ShowGameover();
                HideGame(); 
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateCoinsTexts()
    {
        menuCoins.text = DataManager.instance.GetCoins().ToString();
        gameCoins.text = menuCoins.text;
        levelCompleteCoins.text = menuCoins.text;
        gameoverCoins.text = menuCoins.text;
    }
    private void ShowMenu()
    {
        menuCoins.text = DataManager.instance.GetCoins().ToString();
        menuBestScore.text = DataManager.instance.GetBestScore().ToString();

        ShowCG(MenuCG);
    }
    private void HideMenu()
    {
        HideCG(MenuCG);
    }
    private void ShowGame()
    {
        gameScore.text = DataManager.instance.GetScore().ToString();
        gameCoins.text = DataManager.instance.GetCoins().ToString();

        ShowCG(gameCG);
    }
    private void HideGame()
    {
        HideCG(gameCG);
    }
    private void ShowLevelComplete()
    {
        levelCompleteCoins.text = DataManager.instance.GetCoins().ToString();
        levelCompleteSerectWord.text = WordManager.instance.GetSerectWord();
        levelCompleteScore.text = DataManager.instance.GetScore().ToString();
        levelCompleteBestScore.text = DataManager.instance.GetBestScore().ToString();
        ShowCG(levelCompleteCG);
    }
    private void HideLevelComplete()
    {
        HideCG(levelCompleteCG);
    }
    private void ShowGameover()
    {
        gameoverCoins.text = DataManager.instance.GetCoins().ToString();
        gameoverSerectWord.text = WordManager.instance.GetSerectWord();
        gameoverBestScore.text = DataManager.instance.GetBestScore().ToString();

        ShowCG(gameoverCG);
    }
    private void HideGameover()
    {
        HideCG(gameoverCG);
    }
    public void ShowSettings()
    {
        ShowCG(settingsCG);
    }
    public void HideSettings()
    {
        HideCG(settingsCG);
    }
    private void ShowCG(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    private void HideCG(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
    
}
