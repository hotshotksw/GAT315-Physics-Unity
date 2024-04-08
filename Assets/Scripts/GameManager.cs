using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        TITLE,
        START_GAME,
        PLAY_GAME,
        GAME_OVER,
        WIN,
		PAUSE
    }

    [Header("UI")]
    [SerializeField] GameObject titleUI;
	[SerializeField] GameObject LevelUI;
	[SerializeField] GameObject GameUI;
	[SerializeField] GameObject WinUI;
	[SerializeField] GameObject PauseUI;
	[SerializeField] GameObject GameOverUI;
	[SerializeField] TMP_Text livesUI;
	[SerializeField] Slider healthUI;

	[SerializeField] GameObject[] UIList;

	[SerializeField] TMP_Text levelTime;
	[SerializeField] TMP_Text winTime;

    [Header("Variables")]
    [SerializeField] FloatVariable health;
    [SerializeField] IntVariable score;
	[SerializeField] GameObject respawn;

    public State state = State.TITLE;
    //[SerializeField] float timer = 0;
    [SerializeField] int lives = 0;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioClip[] musicList;
    bool musicPlayed = false;
    [SerializeField] GameObject[] enemies;
    GameObject[] pickups;

    private float timer = 0.0f;

    [Header("Events")]
	//[SerializeField] IntEvent scoreEvent;
	[SerializeField] VoidEvent gameStartEvent;
	[SerializeField] GameObjectEvent respawnEvent;

	public int Lives {  
		get { return lives; } 
		set { 
			lives = value; 
			livesUI.text = lives.ToString(); 
		} 
	}

    private void OnEnable()
    {
    }
    private void OnDisable()
    {
    }


    void Start()
	{
		pickups = GameObject.FindGameObjectsWithTag("Pickup");

        foreach (var enemy in enemies)
		{
			enemy.gameObject.SetActive(false);
		}

		DespawnPickups();
	}

	void Update()
	{
		switch (state)
		{
			case State.TITLE:
				if(titleUI != null)
				{
					LoadScreen(0);
					
				} else
				{
					state = State.START_GAME;
					break;
				}
				Time.timeScale = 1;
				Lives = 3;

				if (!musicPlayed)
				{
					PlayMusic(0);
					musicPlayed = true;
				}

				if (Input.GetKeyDown(KeyCode.Escape))
				{
					Application.Quit();
				}
				timer = 0;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				break;
			case State.START_GAME:
                musicPlayed = false;
                LoadScreen(1);
                timer += Time.deltaTime;
				if (timer >= 2.0f)
				{
                    LoadScreen(2);
                    health.value = 100;
                    gameStartEvent.RaiseEvent();
					SpawnPickups();
					SpawnEnemies();
                    respawnEvent.RaiseEvent(respawn);
                    state = State.PLAY_GAME;
                }
				break;
			case State.PLAY_GAME:
                //EventManager.OnTimerStart();
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                if (Input.GetKeyDown(KeyCode.Escape)) 
				{
					Pause(true);
				}
                break;
			case State.GAME_OVER:
				//EventManager.OnTimerStop();
				musicSource.Stop();
				if (GameOverUI != null)
				{
					LoadScreen(5);
				} else
				{
					state = State.TITLE;
				}
                
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                if (!musicPlayed)
                {
                    musicSource.Stop();
                    musicSource.PlayOneShot(musicList[3]);
                    musicPlayed = true;
                }
                break;
			case State.WIN:
				if (GameOverUI != null)
				{
					LoadScreen(4);
				}
				else
				{
					state = State.TITLE;
				}
				Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
				winTime.text = levelTime.text;
                if (!musicPlayed)
				{
					musicSource.Stop();
					musicSource.PlayOneShot(musicList[2]);
					musicPlayed=true;
				}
				break;
			case State.PAUSE:
				if (PauseUI == null)
				{
					Pause(false);
				}

				Time.timeScale = 0;
                //EventManager.OnTimerStop();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                if (Input.GetKeyDown(KeyCode.Escape)) 
				{
					Pause(false);
				}
                break;
			default:
				break;
		}

		if (healthUI != null) healthUI.value = health.value / 100.0f;
	}

	public void Pause(bool p)
	{
		if(p)
		{
            LoadScreen(3);
            state = State.PAUSE;
        } else
		{
            LoadScreen(2);
            state = State.PLAY_GAME;
        }
	}

	public void SpawnEnemies()
	{
        foreach (var enemy in enemies)
        {
            enemy.gameObject.SetActive(true);
        }
    }

    public void DespawnEnemies()
    {
        foreach (var enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }
    }

    public void DespawnPickups()
	{
        foreach (var pickup in pickups)
        {
            pickup.SetActive(false);
        }
    }

    public void SpawnPickups()
    {
        foreach (var pickup in pickups)
        {
            pickup.SetActive(true);
        }
    }

    public void OnStartGame()
	{
        foreach (var pickup in pickups)
        {
            pickup.SetActive(false);
        }
        PlayMusic(1);
		timer = 0;
		
		state = State.START_GAME;
	}

	public void OnPlayerDead()
	{
		if (state != State.WIN)
		{
            Lives--;
            state = State.START_GAME;
            if (Lives < 0)
            {
				musicPlayed = false;
				state = State.GAME_OVER;
                return;
            }
        }
	}

	public void OnPlayerWin()
	{
		state = State.WIN;
        //EventManager.OnTimerStop();
        
    }

	public void OnWinContinue()
	{
		musicPlayed = false;
        //EventManager.OnTimerUpdate(0);
        state = State.TITLE;
	}

	public void OnAddPoints(int points)
	{
		print(points);
	}

	public void PlayMusic(int id)
	{
		musicSource.Stop();
		musicSource.clip = musicList[id];
		musicSource.Play();
	}

	public void QuitGame()
	{
		LoadScreen(0);
		//EventManager.OnTimerStop();
        //EventManager.OnTimerUpdate(0);
        state = State.TITLE;
	}

	public void LoadScreen(int selection)
	{
		for(int i = 0; i < UIList.Length;i++)
		{
			if (UIList[selection] == UIList[i])
			{
				UIList[i].SetActive(true);
			} else
			{
				UIList[i].SetActive(false);
			}
		}
	}
}