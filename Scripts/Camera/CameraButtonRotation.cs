using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class CameraButtonRotation : MonoBehaviour
{
	[SerializeField] private float _rotationAmount;
	[SerializeField] private float _rotationDuration;

	[Space(20)] 
	[SerializeField] private UnityEvent _OnClockwiseComplete;
	[SerializeField] private UnityEvent _OnAntiClockwiseComplete;
	[SerializeField] private UnityEvent _OnLookDownComplete;
	[SerializeField] private UnityEvent _OnLookUpComplete;

	private bool _isRotating = false;
	private bool _isLookingDown = false;

	private void Update()
	{
		if (_isLookingDown && (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Q)))
		{
			_isRotating = true;
			transform.DORotate(new Vector3(0f, 0f, 0f), _rotationDuration)
				.OnComplete(LookUpCompleted);
			return;
		}
		
		if (Input.GetKeyDown(KeyCode.Q) && _isRotating == false)
		{
			_isRotating = true;
			transform.DORotate(new Vector3(0, transform.eulerAngles.y - _rotationAmount, 0), _rotationDuration)
				.OnComplete(AntiClockwiseRotationCompleted);
		} 
		else if (Input.GetKeyDown(KeyCode.E) && _isRotating == false)
		{
			_isRotating = true;
            transform.DORotate(new Vector3(0, transform.eulerAngles.y + _rotationAmount, 0), _rotationDuration)
	            .OnComplete(ClockwiseRotationCompleted);
		}
		else if (Input.GetKeyDown(KeyCode.L) && _isRotating == false)
		{
			_isRotating = true;
			transform.DORotate(new Vector3(90f, 0f, 0f), _rotationDuration).OnComplete(LookDownCompleted);
		}
	}

	private void ClockwiseRotationCompleted()
	{
		_isRotating = false;
		_OnClockwiseComplete.Invoke();
	}

	private void AntiClockwiseRotationCompleted()
	{
		_isRotating = false;
		_OnAntiClockwiseComplete.Invoke();
	}
	
	private void LookDownCompleted()
	{
		_isRotating = false;
		_isLookingDown = true;
		_OnLookDownComplete.Invoke();
	}
	
	private void LookUpCompleted()
	{
		_isRotating = false;
		_isLookingDown = false;
		_OnLookUpComplete.Invoke();
	}
}
