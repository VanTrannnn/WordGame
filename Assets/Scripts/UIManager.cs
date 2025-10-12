using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header(" Elements ")]
    [SerializeField] private CanvasGroup gameCG;
    [SerializeField] private CanvasGroup levelCompleteCG;
    [SerializeField] private CanvasGroup gameoverCG;


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
        ShowGame();
        HideLevelComplete();
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }
    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }
    private void GameStateChangedCallback(GameState gameState)
    {
        switch(gameState){
            case GameState.Game:
                ShowGame();
                HideLevelComplete();
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
