using System;
using UnityEngine;

public class ClockFace : MonoBehaviour
{
	[SerializeField] private Transform _clockHand;
	[SerializeField] private Timer _focusedTimer;

	private float _dayLength;

	public void StartClock()
	{
		_dayLength = DayManager.instance.GetDayLength();
		_clockHand.rotation = Quaternion.Euler(Vector3.zero);
	}
	

	private void Update()
	{
		float currentTime = _focusedTimer.GetTimerValue();

		// Calculate rotation angle based on time that has passed
		float rotationAngle = (currentTime / _dayLength) * 360.0f;

		// Apply rotation to the clock hand
		_clockHand.localRotation = Quaternion.Euler(0f, 0f, -rotationAngle);
	}
}
