using System;
using System.Collections;
using UnityEngine;

public class CloseGameTimer : MonoBehaviour
{
	
	[SerializeField] private float _closeGameDelay;

	private IEnumerator Start()
	{
		yield return new WaitForSeconds(_closeGameDelay);
		
		Application.Quit();
	}
}
