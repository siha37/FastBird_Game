using MyFolder._01.Script._01.Manager;
using MyFolder._01.Script._02.Object.Obstacle;
using MyFolder._01.Script._02.Object.Player;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PipeSpawner pipeSpawner;
    [SerializeField] private PlayerController player;
    [SerializeField] private ScoreManager scoreManager;
    private float baseSpawnInterval;
    private float gapHeight;
    private float maxHeightChange;
    
    [Header("Interval")]
    [SerializeField] private float baseSpawnIntervalMax = 8f;
    [SerializeField] private float baseSpawnIntervalMin = 4f;
    [SerializeField] private float baseSpawnIntervalOffset = 0.015f;
    [Header("GapHeight")]
    [SerializeField] private float gapHeightMax = 8f;
    [SerializeField] private float gapHeightMin = 4f;
    [SerializeField] private float gapHeightOffset = 2.5f;
    [Header("Max Height")]
    [SerializeField] private float baseHeightChangeMax = 3f;
    [SerializeField] private float baseHeightChangeMin = 1f;
    [SerializeField] private float baseHeightChangeOffset = 0.02f;
    [Header("BackGroundSpeed")]
    [SerializeField] private float bgSpeedMax = 4f;
    [SerializeField] private float bgSpeedMin = 1f;
    [SerializeField] private float bgSpeedOffset = 0.035f;

    [SerializeField] private int level = 0;
    [SerializeField] private int compireScore = 0;
    [SerializeField] private int levelPivot = 10;

    public void Awake()
    {
        scoreManager.scoreUpPointDelegate += ScoreUpdate;
        player.SetBackgroundSpeed(0);
    }

    public void PlayStart()
    {
        level = 0;
        ApplyDifficultyLevel(level);
    }

    private void ScoreUpdate(int score)
    {
        if (score != compireScore)
        {
            if (score % levelPivot == 0)
            {
                level++;
                ApplyDifficultyLevel(level);
                compireScore = score;
            }
        }
    }
    
    private void ApplyDifficultyLevel(int level)
    {
        baseSpawnInterval = Mathf.Max(baseSpawnIntervalMin,baseSpawnIntervalMax -baseSpawnIntervalOffset * level); // 최소 0.8초까지 감소
        gapHeight = Mathf.Max(gapHeightMin, gapHeightMax - Mathf.Log(level + 1) / Mathf.Log(gapHeightOffset)); // 최소 2까지 줄이기
        maxHeightChange = Mathf.Min(baseHeightChangeMax, baseHeightChangeMin + baseHeightChangeOffset * level); // 변화 폭 점점 커짐
        
        player.SetBackgroundSpeed(Mathf.Min(bgSpeedMax, bgSpeedMin +bgSpeedOffset* level));
        pipeSpawner.GetSetbaseSpawnInterval = baseSpawnInterval;
        pipeSpawner.GetSetGapHeight = gapHeight;
        pipeSpawner.GetSetMaxHeightChange = maxHeightChange;
    }

}
