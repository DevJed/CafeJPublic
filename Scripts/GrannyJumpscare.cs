using System.Collections;
using DG.Tweening;
using UnityEngine;

public class GrannyJumpscare : MonoBehaviour
{
	[SerializeField] private DaySuccessChecker _daySuccessChecker;
	
	
	[SerializeField] private Vector3 _onScreenPosition;
	[SerializeField] private Vector3 _offScreenPosition;
	[SerializeField] private float _speed;
	

	[SerializeField] private RectTransform _normalGranny;
	[SerializeField] private RectTransform _horrorGranny;
	

	public void JumpscareGranny()
	{
		StartCoroutine(nameof(DelayClipboard));
		AudioManager.Instance.PlaySFX("Granny");
		if (DayManager.instance._dayIndex >= 4)
		{
			_horrorGranny.DOLocalMove(_onScreenPosition, _speed);
			
			return;
		}

		_normalGranny.DOLocalMove(_onScreenPosition, _speed);
	}

	public void HideGranny()
	{
		_normalGranny.position = _offScreenPosition;
		_horrorGranny.position = _offScreenPosition;
	}

	private IEnumerator DelayClipboard()
	{
		yield return new WaitForSeconds(1.5f);
		_daySuccessChecker.DayCompleteCheck();

		yield return new WaitForSeconds(1f);
		HideGranny();
	}
}
