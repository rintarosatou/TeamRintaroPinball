using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    // Start is called before the first frame update
    void Awake()
    {
        // シングルトンにしておくとどこからでも呼び出せます
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }
}
