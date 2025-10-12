using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class KeyboardColorizer : MonoBehaviour
{
    [Header(" Elements ")]
    private KeyboardKey[] keys;

    private void Awake()
    {
        keys = GetComponentsInChildren<KeyboardKey>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }
    private void OnDestroy()
    {
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
    private void Initialize()
    {
        for(int i = 0; i<keys.Length; i++)
        {
            keys[i].Initialize();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Colorize(string secretWord, string wordToCheck)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            char keyLetter = keys[i].GetLetter();
            for (int j = 0; j < wordToCheck.Length; j++)
            {
                if (keyLetter != wordToCheck[j])
                    continue;

                if (keyLetter == secretWord[j])
                {
                    keys[i].SetValid();
                }
                else if (secretWord.Contains(keyLetter))
                {
                    keys[i].SetPotential();
                }
                else
                {
                    keys[i].SetInvalid();
                }

            }
        }
    }
}
