using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [Header(" Elements")]
    [SerializeField] private WordContainer[] wordContainers;
    [SerializeField] private Button tryButton;
    [SerializeField] private KeyboardColorizer keyboardColorizer;
    [Header(" Settings")]
    private int currentWordContainerIndex;
    private bool canAddLetter = true;

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
        Initialize();

        KeyboardKey.onKeyPressed += KeyPressedcallback;
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }
    private void OnDestroy()
    {
        KeyboardKey.onKeyPressed -= KeyPressedcallback;
        GameManager.onGameStateChanged -= GameStateChangedCallback;

    }
    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Game:
                Initialize();
                break;
            case GameState.LevelComplete:

                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Initialize ()
    {
        currentWordContainerIndex = 0;
        canAddLetter = true;
        DisableTryButton();

        for(int i =0; i< wordContainers.Length; i++)
        {
            wordContainers[i].Initialize();
        }
    }
    private void KeyPressedcallback(char letter)
    {
        if (!canAddLetter)
            return;
        //if (wordContainers[currentWordContainerIndex].IsComplete())
        //    currentWordContainerIndex++;
        //wordContainers[currentWordContainerIndex].Add(letter);
        wordContainers[currentWordContainerIndex].Add(letter);
        if (wordContainers[currentWordContainerIndex].IsComplete())
        {
            canAddLetter = false;
            EnableTryButton();
        }
        
    }
    public void BackspacePressedCallback()
    {
        if (!GameManager.instance.IsGameState())
            return;
        bool removeLetter = wordContainers[currentWordContainerIndex].RemoveLetter();
        if(removeLetter)
            canAddLetter = true;
    }
    public void CheckWord()
    {
        if (!wordContainers[currentWordContainerIndex].IsComplete())
            return;
        string wordToCheck = wordContainers[currentWordContainerIndex].GetWord();
        string secretWord = WordManager.instance.GetSerectWord();

        wordContainers[currentWordContainerIndex].Colorize(secretWord);
        keyboardColorizer.Colorize(secretWord, wordToCheck);

        if (wordToCheck == secretWord)
            SetLevelComplete();
        else
        {
            //Debug.Log("False");
            currentWordContainerIndex++;
            DisableTryButton();

            if (currentWordContainerIndex >= wordContainers.Length)
            {
                //Debug.Log("Game over");
                DataManager.instance.ResetScore();
                GameManager.instance.SetGameState(GameState.Gameover);
            }
            else
            {
                canAddLetter = true;
            }
        }
    }
    private void SetLevelComplete()
    {
        UpdateData();
        GameManager.instance.SetGameState(GameState.LevelComplete);
    }
    private void UpdateData()
    {
        int scoreToAdd = 6 - currentWordContainerIndex;

        DataManager.instance.IncreaseScore(scoreToAdd);
        DataManager.instance.AddCoins(scoreToAdd * 3);
    }
    private void EnableTryButton()
    {
        tryButton.interactable = true;
    }
    private void DisableTryButton()
    {
        tryButton.interactable = false;
    }
    public WordContainer GetCurrentWordContainer()
    {
        return wordContainers[currentWordContainerIndex];
    }

}
