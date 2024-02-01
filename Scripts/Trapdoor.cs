using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Trapdoor : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _slotALabel;
	[SerializeField] private TextMeshProUGUI _slotBLabel;
	[SerializeField] private TextMeshProUGUI _slotCLabel;
	
	[SerializeField] private UnityEvent _onCodeCorrect;
	
	[Header("Debug Data")]
	[SerializeField] private int _valueA;
	[SerializeField] private int _valueB;
	[SerializeField] private int _valueC;
	
	public void PressPrimaryButton(InputAction.CallbackContext context)
	{
		if (context.started || context.canceled) return;
		_valueA++;
		if (_valueA >= 10)
		{
			_valueA = 0;
		}

		_slotALabel.text = _valueA.ToString();
		CheckCombination();
	}
	
	public void PressSecondaryButton(InputAction.CallbackContext context)
	{
		if (context.started || context.canceled) return;
		_valueB++;
		if (_valueB >= 10)
		{
			_valueB = 0;
		}

		_slotBLabel.text = _valueB.ToString();
        CheckCombination();
	}
	
	public void PressTernaryButton(InputAction.CallbackContext context)
	{
		if (context.started || context.canceled) return;
		_valueC++;
		if (_valueC >= 10)
		{
			_valueC = 0;
		}

		_slotCLabel.text = _valueC.ToString();
        CheckCombination();
	}
	
	
	private void CheckCombination()
	{
		AudioManager.Instance.PlaySFX("UIButton");
		
		if (_valueA == 4 && _valueB == 1 && _valueC == 3)
		{
			//Secret Ending
			_onCodeCorrect.Invoke();
			AudioManager.Instance.StopMusic();
			SceneManager.LoadScene("Basement");
		}
	}
}
