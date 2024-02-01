using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class Customer: MonoBehaviour
{
	[SerializeField] private GameObject _ordersParent;
	[SerializeField] private List<ResourceTypes> _customerOrder = new List<ResourceTypes>();
	
	private Tween _currentTween;
	
	public void MoveToLocation(Vector3 targetLocation, float speed, bool atCounter)
	{
		if (_currentTween != null)
		{
			_currentTween.Kill();
		}
		
		_currentTween = transform.DOMove(targetLocation, speed).OnComplete(atCounter ? ShowCustomerOrder : null);
	}

	private void ShowCustomerOrder()
	{
		_ordersParent.SetActive(true);
		ResourceTransaction.instance.CustomerOrderRequest(_customerOrder, _ordersParent);
	}

	public void HideCustomerOrder()
	{
		_ordersParent.SetActive(false);
	}
}
