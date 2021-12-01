using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  private static GameController _instance;
  public static GameController Instance { get { return _instance; } }

  [SerializeField] AudioSource musicSource;

  [SerializeField] private GameObject startScreen;
  [SerializeField] private GameObject pauseScreen;
  [SerializeField] private GameObject gameOverScreen;
  private bool gamePaused;
  private bool gameOver;

  private bool gameStopped;
  public bool GameStopped => gameStopped;

  [SerializeField] Text gameOverPointText;

  // public Texture2D CrosshairCursor;
  // public Texture2D StandardCursor;

  private PlayerBase playerBase;
  private Inventory inventory;

  private int bugCount;
  public int BugCount => bugCount;

  private void Awake()
  {
    if(_instance != null && _instance != this)
    {
      Destroy(this.gameObject);
    } else {
      _instance = this;
    }

    playerBase = FindObjectOfType<PlayerBase>();
    inventory = FindObjectOfType<Inventory>();

    if(!GameStarted.Started)
    {
      gameStopped = true;
      startScreen.SetActive(true);
      Time.timeScale = 0; //Game starts on intro screen. Loading another scene seemed unneeded.
      // Cursor.SetCursor(StandardCursor, Vector2.zero, CursorMode.Auto);
    }
    else
    {
      StartMusic();
      gameStopped = false;
      inventory.SelectItem(0);
    }
  }

  void Update()
  {
    if(!GameStarted.Started)
    {
      if(Input.anyKeyDown)
      {
        StartGame();
      }      
      return;
    }
    if(!gameOver)
    {
      if(!gamePaused && Input.GetKeyDown(KeyCode.Escape))
      {
        PauseGame(true);
      }
      else if(gamePaused && Input.anyKeyDown)
      {
        PauseGame(false);
      }
    }
  }

  public Vector3 GetPlayerBaseLocation()
  {
    return playerBase.PulledDistance;
  }

  public void AddToBugsPulling(Bug bug)
  {
    playerBase.BugsPulling.Add(bug);
  }

  public void RemoveBugsPulling(Bug bug)
  {
    playerBase.BugsPulling.Remove(bug);
  }

  public void IncreaseBugCount()
  {
    ++bugCount;
  }

  public void DecreaseBugCount()
  {
    --bugCount;
  }

  public void GameOver()
  {
    // Cursor.SetCursor(StandardCursor, Vector2.zero, CursorMode.Auto);
    gameStopped = true;
    gameOver = true;
    gameOverScreen.SetActive(true);
    Time.timeScale = 0;
    gameOverPointText.text = PointsSystem.Instance.GetPoints().ToString();
    StopMusic();
  }

  public void ResetGame()
  {
    Scene scene = SceneManager.GetActiveScene(); 
    SceneManager.LoadScene(scene.name);
    Time.timeScale = 1;
    inventory.SelectItem(0);
    // Cursor.SetCursor(CrosshairCursor, Vector2.zero, CursorMode.Auto);
  }

  public void PauseGame(bool pause)
  {
    gameStopped = pause;
    if(pause)
    {
      StopMusic();
      // Cursor.SetCursor(StandardCursor, Vector2.zero, CursorMode.Auto);
    }
    else
    {
      StartGame();
      // Cursor.SetCursor(CrosshairCursor, Vector2.zero, CursorMode.Auto);
    }
    gamePaused = pause;
    pauseScreen.SetActive(pause);
    Time.timeScale = (pause ? 0 : 1);
  }

  public void StartGame()
  {
    gameStopped = false;
    Time.timeScale = 1;
    startScreen.SetActive(false);
    GameStarted.Started = true;
    inventory.SelectItem(0);
    // Cursor.SetCursor(CrosshairCursor, Vector2.zero, CursorMode.Auto);
    StartMusic();
  }

  public void StartMusic()
  {
    musicSource.Play();
  }

  public void StopMusic()
  {
    musicSource.Pause();
  }

  public void SetMusicSpeed(float amount)
  {
    musicSource.pitch = amount;
  }
}

public static class GameStarted
{
  public static bool Started;
}