using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Checks to make sure all conditions are met to have a successful level
/// </summary>
public class DaySuccessChecker : MonoBehaviour
{
	[SerializeField] private CustomerQueue _customerQueue;
	[SerializeField] private LetterStack _letterStack;

	[SerializeField] private EndScreenUI _endScreenUI;

	[SerializeField] private UnityEvent _onEndOfDay;
	
	
	
	public void DayCompleteCheck()
	{
		bool queueStatus = _customerQueue.IsQueueEmpty();
		bool mailStatus = _letterStack.GetCorrectPlacementStatus();

		//If successful 
		if (queueStatus && mailStatus)
		{
			DayManager.instance.ProgressDayIndex();
			_endScreenUI.ShowCorrectDayClipboard();
		}
		else
		{
			_endScreenUI.ShowIncorrectDayClipboard();
		}
		
		_onEndOfDay.Invoke();
	}
}
