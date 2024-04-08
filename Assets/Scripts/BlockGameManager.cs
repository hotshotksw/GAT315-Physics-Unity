using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class BlockGameManager : MonoBehaviour
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

    [SerializeField] GameObject[] UIList;

	[SerializeField] TMP_Text ScoreUI;
	[SerializeField] TMP_Text FinalScoreUI;


	[Header("Variables")]
	[SerializeField] GameObject player;
	[SerializeField] GameObject playerSpline;
	[SerializeField] float splineSpeed = 0;
	[SerializeField] IntVariable score;

	[Header("Misc.")]

	public State state = State.TITLE;

	private void OnEnable()
	{
	}
	private void OnDisable()
	{
	}

	void Start()
	{
		state = State.TITLE;
	}

	private void Update()
	{
		switch (state)
		{
			case State.TITLE:
                if (UIList[0] != null)
                {
                    LoadScreen(0);

                }
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
				playerSpline.GetComponent<PathFollower>().speed = 0;
				playerSpline.GetComponent<PathFollower>().tdistance = 0;
				break;
			case State.START_GAME:
                LoadScreen(1);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
				playerSpline.GetComponent<PathFollower>().speed = splineSpeed;
				playerSpline.GetComponent<PathFollower>().tdistance = 0;
				state = State.PLAY_GAME;
				break;
			case State.PLAY_GAME:

				if(playerSpline.GetComponent<PathFollower>() != null)
				{
					if(playerSpline.GetComponent<PathFollower>().tdistance >= 1)
					{
						state = State.WIN;
					}
				}
				break;
			case State.WIN:
				playerSpline.GetComponent<PathFollower>().speed = 0;
				playerSpline.GetComponent<PathFollower>().tdistance = 0;
				FinalScoreUI.text = ScoreUI.text;
				LoadScreen(2);
				break;
			default:
				break;
		}
		
		ScoreUI.text = "SCORE: " + score.value;
	}

	public void OnStartGame()
	{
		state = State.START_GAME;
	}

	//public void QuitGame()
	//{
	//	LoadScreen(0);
	//	state = State.TITLE;
	//}

	public void OnAddPoints(int points)
	{
		print(points);
	}

	public void LoadScreen(int selection)
	{

		for (int i = 0; i < UIList.Length; i++)
		{
			if (UIList[i] != null && UIList[selection] == UIList[i])
			{
				UIList[i].SetActive(true);
			}
			else
			{
				if (UIList[i] != null)
				{
					UIList[i].SetActive(false);
				}
			}
		}
	}
}
