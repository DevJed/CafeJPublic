using System.Collections.Generic;
using UnityEngine;

public class ProgressBarManager : MonoBehaviour
{
	[SerializeField] private List<SegmentProgressBar> _allProgressBars = new List<SegmentProgressBar>();
	
	public void ResetAllProgressBars()
	{
		for (int i = 0; i < _allProgressBars.Count; i++)
		{
			_allProgressBars[i].ResetProgress();
		}
	}
}
