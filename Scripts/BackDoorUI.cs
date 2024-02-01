using UnityEngine;

public class BackDoorUI : MonoBehaviour
{
	[SerializeField] private Canvas _aboveDoorCanvas;
	[SerializeField] private GameObject _corkboardInstructions;
	
	
	public void ShowBackDoorInfo()
	{
		_corkboardInstructions.SetActive(true);
		_aboveDoorCanvas.enabled = true;
	}

	public void HideBackDoorInfo()
	{
		_corkboardInstructions.SetActive(false);
		_aboveDoorCanvas.enabled = false;
	}
}
