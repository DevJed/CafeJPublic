using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CustomerQueue : MonoBehaviour
{
	[SerializeField] private CustomerManager _customerManager;
	
	[SerializeField] private Transform _entrancePoint;
	[SerializeField] private Transform _exitPoint;
	[SerializeField] private List<Transform> _queuePoints = new List<Transform>();

	[SerializeField] private int _customersInStore;
	[SerializeField] private int _customersLeftStore;
	[SerializeField] private float _movementSpeed;

	private Customer _focusedCustomer;

	private List<Customer> _storeLine = new List<Customer>();
	
	public void MoveToEntrance(Customer customer)
	{
		_focusedCustomer = customer;
		
		customer.transform.DOMove(_entrancePoint.position, _movementSpeed).OnComplete(ArrivedAtEntrance);
		_storeLine.Add(customer);
	}
	
	//Move to Entrance (If there is less than 4 people in store) 
	void ArrivedAtEntrance()
	{
		//Check to see which QueuePoints are free (see how many in room) 
		_focusedCustomer.MoveToLocation(_queuePoints[_customersInStore].position, _movementSpeed, _customersInStore == 0 );
		
		//Move to the queuePoint that is free
		_customersInStore++;
	}
	
	public void MakeCustomerLeave()
	{
		//Move front customer out of the store 
		_storeLine[_customersLeftStore].HideCustomerOrder();
		_storeLine[_customersLeftStore].transform.DOMove(_exitPoint.position, _movementSpeed);
		
		//Sets values
		_customersLeftStore++;
		_customersInStore--;

		MoveQueueAlong();
	}

	public void MoveQueueAlong()
	{
		for (int i = 0; i < _queuePoints.Count; i++)
		{
			//If you have ran out of customers in the store, don't try to add a customer to a queue point
			if (i >= _customersInStore)
			{
				return;
			}
			
			var customer = _storeLine[_customersLeftStore + i];
			
			customer.MoveToLocation(_queuePoints[i].transform.position, 0.5f, i == 0);
		}
	}

	/// <summary>
	/// Check if queue is empty for end screen
	/// </summary>
	public bool IsQueueEmpty()
	{
		return _customersLeftStore == _storeLine.Count;
	}

	public void ClearQueue()
	{
		_customersInStore = 0;
		_customersLeftStore = 0;
		_storeLine.Clear();
	}
	
}
