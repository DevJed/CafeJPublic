using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RotatingMachine : MonoBehaviour
{
	[SerializeField] private RectTransform _cursor;
	[SerializeField] private RectTransform _targetZone;

	[SerializeField] private Image _cursorImage;
	
	[Header("Speed")] 
	[SerializeField] private bool _clockwise;
	
	[SerializeField] private float _currentSpeed;
	[SerializeField] private float _minSpeed;

	[Header("TargetZone")]
	[SerializeField] private float _targetZoneSize;

	[Space(20)]
	public UnityEvent OnCorrectInput;
	public UnityEvent OnIncorrectInput;

	private bool _allowInput = true;

	/// <summary>
	/// Sets random rotation on start
	/// </summary>
	private void Start()
	{
		_targetZone.localRotation = Quaternion.Euler(new Vector3(0, _targetZone.localRotation.y, Random.Range(0, 360f - _targetZoneSize)));
	}

	private void Update()
	{
		_cursor.Rotate(Vector3.forward, (_clockwise ? -_currentSpeed : _currentSpeed) * Time.deltaTime);

		if (_currentSpeed >= _minSpeed)
		{
			_currentSpeed -= (0.5f * Time.deltaTime) * _currentSpeed % _minSpeed;
		}
	}

	//When Key is pressed, check if valid
	public void CheckInput(InputAction.CallbackContext context)
	{
		if (context.started || context.canceled || !_allowInput) return;
		var cursorRotation = _cursor.eulerAngles.z;

		var minZone = _targetZone.eulerAngles.z;
		var maxZone = _targetZone.eulerAngles.z + _targetZoneSize;

		//Correct Input
		if (cursorRotation >= minZone && cursorRotation <= maxZone)
		{
			CorrectInput();
		}
		else //Incorrect Input
		{
			IncorrectInput();
		}
	}
	
	/// <summary>
	/// When the user clicks in the correct zone
	/// </summary>
	private void CorrectInput()
	{
		_targetZone.localRotation = Quaternion.Euler(new Vector3(0, _targetZone.localRotation.y, Random.Range(0, 360f - _targetZoneSize)));
		_currentSpeed *= 2.5f;
		
		OnCorrectInput.Invoke();
		
		AudioManager.Instance.PlaySFX("CoffeeMachine");
	}
	
	private void IncorrectInput()
	{
		OnIncorrectInput.Invoke();
		AudioManager.Instance.PlaySFX("CoffeeMachineFailure");
	}

	public void TemporaryDisableActivate()
	{
		StartCoroutine(TemporaryDisable());
	}

	private IEnumerator TemporaryDisable()
	{
		_cursorImage.enabled = false;
		_allowInput = false;
		
		yield return new WaitForSeconds(1f);
		
		_cursorImage.enabled = true;
		_allowInput = true;
	}
}
