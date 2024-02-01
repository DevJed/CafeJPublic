using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BackDoor : MonoBehaviour
{
	[SerializeField] private GrannyJumpscare _grannyJumpscare;
	
	[SerializeField] private Transform _stalker;
	[SerializeField] private Transform _door;

	[SerializeField] private float _doorTimer = 0f;
	
	[SerializeField] private List<float> _allLevelTimings = new List<float>();
	[SerializeField] private List<float> _allLevelStalkerTimings = new List<float>();
	
	[Header("StalkerSettings")]
	[SerializeField] private Vector3 _stalkerStartPosition;
	[SerializeField] private Vector3 _stalkerEndPosition;
	[SerializeField] private float _stalkerTimer = 0f;
	
	[Header("StalkerVisuals")] 
	[SerializeField] private SpriteRenderer _stalkerSpriteRenderer;
	[SerializeField] private Sprite _normalStalkerSprite;
	[SerializeField] private Sprite _horrorStalkerSprite;
	
	
	
	//Door settings
	private bool _runTimer = false;
	private bool _doorOpen = false;
	private Vector3 _openRotation = new Vector3(0, 120f, 0);
	private Vector3 _closedRotation = new Vector3(0, 0f, 0);

	
	private void Start()
	{
		RestartStalker();
	}

	public void CheckBackdoorStatus()
	{
		if (DayManager.instance._dayIndex >= 2)
		{
			RestartStalker();
			_runTimer = true;
		}
		
		_stalkerSpriteRenderer.sprite = DayManager.instance._dayIndex == 4 ? _horrorStalkerSprite : _normalStalkerSprite;
	}

	private void Update()
	{
		if (!_runTimer) return;

		_doorTimer += Time.deltaTime;

		if (_doorOpen == false && _doorTimer >= _allLevelTimings[DayManager.instance._dayIndex])
		{
			OpenDoor();
		}

		//Move stalker more to the centre
		if (_doorOpen)
		{
			_stalkerTimer += Time.deltaTime;
			
			// Calculate the normalized time (between 0 and 1) based on the elapsed time and duration
			float t = Mathf.Clamp01(_stalkerTimer / _allLevelStalkerTimings[DayManager.instance._dayIndex]);

			_stalker.position = Vector3.Lerp(_stalkerStartPosition, _stalkerEndPosition, t);
			
			// Check if the movement is complete
			if (t >= 1f)
			{
				//Game Over, do something!
				_runTimer = false;
				_grannyJumpscare.JumpscareGranny();
			}
		}
	}

	public void RestartStalker()
	{
		_stalker.position = _stalkerStartPosition;
		_stalkerTimer = 0;
		
		_door.rotation = Quaternion.Euler(_closedRotation);
		_doorOpen = false;
		_doorTimer = 0f;
	}

	private void OpenDoor()
	{
		_door.rotation = Quaternion.Euler(_openRotation);
		_doorOpen = true;
		AudioManager.Instance.PlaySFX("OpenDoor");
	}
	
	public void CloseDoor(InputAction.CallbackContext context)
	{
		if (context.started || context.canceled) return;

		if (_doorOpen)
		{
			_door.rotation = Quaternion.Euler(_closedRotation);
			_doorOpen = false;
			_doorTimer = 0f;
			
			AudioManager.Instance.PlaySFX("DoorClose");
		}
	}

	public void StopTimer()
	{
		_runTimer = false;
	}
}
