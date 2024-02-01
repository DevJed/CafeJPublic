using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	[SerializeField] private PlayerInput _playerInput;
	[SerializeField] private int _currentDirection = 0;
	
	public enum LookingDirections{
		North,
		East,
		South,
		West,
		Down
	}
	
	public void ClockwiseMovement()
	{
		_currentDirection++;
		if (_currentDirection >= 4)
		{
			_currentDirection = 0;
		}
		
		_playerInput.SwitchCurrentActionMap(((LookingDirections)_currentDirection).ToString());
	}
	
	public void AntiClockwiseMovement()
	{
		_currentDirection--;
		if (_currentDirection <= -1)
		{
			_currentDirection = 3; //Resets to West
		}
		
		_playerInput.SwitchCurrentActionMap(((LookingDirections)_currentDirection).ToString());
	}

	public void LookingDownMovement()
	{
		_currentDirection = 4;
		
		_playerInput.SwitchCurrentActionMap(((LookingDirections)_currentDirection).ToString());
	}

	public void LookingUpMovement()
	{
		_currentDirection = 0;
		
		_playerInput.SwitchCurrentActionMap(((LookingDirections)_currentDirection).ToString());
	}
	
}
