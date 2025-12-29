using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int gameLevel = 1;
    private static int totalScore = 0;

    public static void ResetGameData()
    {
        gameLevel = 1;
        totalScore = 0;
    }

    public event EventHandler onGamePause;
    public event EventHandler onGameUnPause;

    private int gameScore = 0;
    private float timer;
    private bool isPlaying;

    [SerializeField] private List<GameLevel> levelList;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    
    
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Lander.Instance.onPickUpCoin += Lander_onPickUpCoin;
        Lander.Instance.onLanded += Lander_onLanded;
        Lander.Instance.onStateChanged += Lander_onStateChanged;
        GameInput.instance.onMenuButtonPressed += GameInput_onMenuButtonPressed;
        LoadCurrentLevel();
    }

    private void GameInput_onMenuButtonPressed(object sender, System.EventArgs e)
    {
        PauseUnpauseGame();
    }

    private void Lander_onStateChanged(object sender, Lander.onStateChangedEventArgs e)
    {
        isPlaying = e.state == Lander.State.Normal;

        if (e.state == Lander.State.Normal) {
            cinemachineCamera.Target.TrackingTarget = Lander.Instance.transform;
            CinemachineCameraZoom2D.Instance.setNormalOrthographicSize();
        }
    }

    private void Update()
    {
        if (isPlaying)
        {
            timer += Time.deltaTime;
        }
    }
    private void LoadCurrentLevel()
    {
        GameLevel level =  getGameLevel();
        if (level == null)
        {
            SceneLoader.LoadScene(SceneLoader.SceneEnum.GameOverScene);
        }
        else
        {
            SimplePool2.ClearPool();
            GameLevel currentGameLevel = Instantiate(level, Vector3.zero, Quaternion.identity);
            Lander.Instance.transform.position = currentGameLevel.getLanderStartPosition();
            cinemachineCamera.Target.TrackingTarget = currentGameLevel.getCameraStartPosition();
            CinemachineCameraZoom2D.Instance.setTargetOrthographicSize(currentGameLevel.getZoomedOutOrthographicSize());
        }
    }

    private GameLevel getGameLevel()
    {
        foreach (var level in levelList)
        {
            if (level.getLevelNumber() == gameLevel)
            {
                return level;
            }
        }
        return null;
    }
    private void Lander_onLanded(object sender, Lander.onLandedEventArgs e)
    {
        addScore(e.score);
    }

    private void Lander_onPickUpCoin(object sender, Lander.onPickUpCoinEventArg e)
    {
        addScore(100);
    }

    private void addScore(int amount)
    {
        gameScore += amount;
    }

    public float getTimer()
    {
        return Mathf.RoundToInt(timer);
    }
    public int getScore()
    {
        return gameScore;
    }
    public int getTotalScore()
    {
        return totalScore;
    }
    public void GoToNextLevel()
    {
        totalScore += gameScore;
        gameLevel++;
        SimplePool2.ClearPool();
        SceneLoader.LoadScene(SceneLoader.SceneEnum.GamePlayScene);
        
    }
    public void PauseUnpauseGame()
    {
        if(Time.timeScale >= 1.0f)
        {
            PauseGame();
        }
        else
        {
            UnPauseGame();
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        onGamePause?.Invoke(this, EventArgs.Empty);
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1.0f;
        onGameUnPause?.Invoke(this, EventArgs.Empty);
    }

}
