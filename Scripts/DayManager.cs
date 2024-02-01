using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
	public static DayManager instance;

	[SerializeField] private BackDoorUI _backDoorUI;
	

	[SerializeField] private Timer _timer;
	[SerializeField] private float _daySeconds;
	
	public int _dayIndex;
	
	[Space(10)]
	public UnityEvent OnDayStart;
	public UnityEvent OnDayFinished;

	[SerializeField] private int _overrideDayIndex = -1;
	
	public float GetDayLength() => _daySeconds;


	/// <summary>
	/// Singleton Set up
	/// </summary>
	private void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}

		instance = this;
	}

	private void Start()
	{
		//Load level 
		if (ES3.KeyExists("dayIndex"))
		{
			_dayIndex = (int)ES3.Load("dayIndex");
		}
		else //create save file for day
		{
			ES3.Save("dayIndex", 0);
		}

		if (_overrideDayIndex != -1)
		{
			_dayIndex = _overrideDayIndex;
		}
		
		CheckDayEvents();
		StartDay();
		
	}

	private void Update()
	{
		if (Math.Abs(_timer.GetTimerValue() - _daySeconds) < 0.15f)
		{
			TimerComplete();
		}
	}

	/// <summary>
	/// This is for BOTH USE CASES Of Succeeding and failure, so needs an if statement really
	/// </summary>
	private void TimerComplete()
	{
		//When UI Up, reset Logic 
		_timer.ResetTimer(false);
		OnDayFinished.Invoke();
	}

	private void CheckDayEvents()
	{
		if (_dayIndex == 4)
		{
			AudioManager.Instance.PlayHorrorAudio();
		}

		if (_dayIndex >= 2)
		{
			_backDoorUI.ShowBackDoorInfo();
		}
		else
		{
			_backDoorUI.HideBackDoorInfo();
		}
	}

	/// <summary>
	/// Checks to make sure you haven't finished the game
	/// </summary>
	
	public void StartDay()
	{
		//If you have finished the game, goes back to level 5 (index 4)
		if (_dayIndex >= 5)
		{
			_dayIndex = 4;
			ES3.Save("dayIndex", _dayIndex);
		}
		
		OnDayStart.Invoke();
		_timer.EnableTimer();
		
		CheckDayEvents();
	}
	
	public void ProgressDayIndex()
	{
		//Check to make sure your not going to progress into a incorrect index
		_dayIndex++;
		ES3.Save("dayIndex", _dayIndex);
	}
	
	public void ClipboardContinueButton()
	{
		//Check if game is over
		if (_dayIndex >= 5)
		{
			AudioManager.Instance.PlayMenuAudio();
			_dayIndex = 4;
			ES3.Save("dayIndex", _dayIndex);
			SceneManager.LoadScene("MainMenu");
			return;
		}
		
		StartDay();
	}
}
