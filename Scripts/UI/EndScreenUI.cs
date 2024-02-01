using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenUI : MonoBehaviour
{
	[SerializeField] private InteractableCanvas _endLevelCanvas;
	[SerializeField] private Image _background;
	[SerializeField] private Transform _correctClipboard;
	[SerializeField] private Transform _incorrectClipboard;
	[SerializeField] private TextMeshProUGUI _clipboardMessageLabel;
	

	[SerializeField] private Vector3 _clipboardOffScreenPosition;
	[SerializeField] private Vector3 _clipboardOnScreenPosition;
	[SerializeField] private Color _offColour;
	[SerializeField] private Color _onScreenColour;

	[SerializeField] private List<string> _allClipboardMessages = new List<string>();
	
	
	
	public void ShowCorrectDayClipboard()
	{
		_background.color = _offColour;
		_endLevelCanvas.EnableInteractableCanvas();
		ChangeClipboardMessage();

		_correctClipboard.DOLocalMove(_clipboardOnScreenPosition, 0.45f);
		_background.DOColor(_onScreenColour, 0.2f);
	}
	
	
	public void ShowIncorrectDayClipboard()
	{
		_background.color = _offColour;
		_endLevelCanvas.EnableInteractableCanvas();

		_incorrectClipboard.DOLocalMove(_clipboardOnScreenPosition, 0.45f);
		_background.DOColor(_onScreenColour, 0.2f);
	}

	private void ChangeClipboardMessage()
	{
		_clipboardMessageLabel.text = _allClipboardMessages[DayManager.instance._dayIndex - 1];
	}

	public void HideCorrectClipboard()
	{
		_correctClipboard.DOLocalMove(_clipboardOffScreenPosition, 0.45f);
		_background.DOColor(_offColour, 0.2f);
	}

	public void HideIncorrectClipboard()
	{
		_incorrectClipboard.DOLocalMove(_clipboardOffScreenPosition, 0.45f);
		_background.DOColor(_offColour, 0.2f);
	}
}
