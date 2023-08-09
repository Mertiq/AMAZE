using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Level> levels;
    [SerializeField] private LevelGenerator levelGenerator;

    [HideInInspector] public Level activeLevel;
    public static GameManager Instance;
    [HideInInspector] public int targetScore;
    [HideInInspector] public int currentScore;

    private void Awake()
    {
        if (Instance is null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        activeLevel = levels[0];
    }

    private void ChangeLevel()
    {
        var activeLevelIndex = levels.IndexOf(activeLevel);

        activeLevelIndex++;

        if (activeLevelIndex < levels.Count)
        {
            activeLevel = levels[activeLevelIndex];
            currentScore = 0;
            levelGenerator.GenerateLevel();
        }
    }

    public void IncreaseScore()
    {
        currentScore++;
        GameOverControl();
    }

    private void GameOverControl()
    {
        if (currentScore >= targetScore) Invoke(nameof(ChangeLevel), 1f);
    }
}