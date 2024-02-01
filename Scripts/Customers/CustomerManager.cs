using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
	[SerializeField] private CustomerQueue _customerQueue;
	
	[SerializeField] private Transform _spawnPoint;
	[SerializeField] private Transform _customerParent;
	
	[SerializeField] private float _elapsedTime = 0f;
	
	[SerializeField] private List<CustomerLine> _allLevelsLines = new List<CustomerLine>();

	private List<Customer> _allCustomersInLevel = new List<Customer>();
	private Queue<float> _queueTimings = new Queue<float>();
	
	private float _nextKeypoint;
	private int _customerIndex;

	private bool _runTimer = false;

	public void StartDay()
	{
		ClearPreviousCustomers();
		
		_runTimer = true;
		CreateCustomers();
	}

	private void ClearPreviousCustomers()
	{
		//Clears Queue first before deleting gameobjects 
		_customerQueue.ClearQueue();
		
		for (int i = 0; i < _allCustomersInLevel.Count; i++)
		{
			Destroy(_allCustomersInLevel[i].gameObject);
		}
		
		_allCustomersInLevel.Clear();
		_queueTimings.Clear();
		_customerIndex = 0;
		_elapsedTime = 0;
	}

	private void CreateCustomers()
	{
		LoadQueueTimings();
		
		for (int i = 0; i < _allLevelsLines[DayManager.instance._dayIndex]._customerLine.Count; i++)
		{
			var createdCustomer = Instantiate(_allLevelsLines[DayManager.instance._dayIndex]._customerLine[i].transform, 
				_spawnPoint.position, 
				Quaternion.Euler(Vector3.zero),
				_customerParent);
			
			_allCustomersInLevel.Add(createdCustomer.GetComponent<Customer>());
		}
	}

	private void Update()
	{
		if (!_runTimer) return;

		_elapsedTime += Time.deltaTime;

		if (_elapsedTime >= _nextKeypoint)
		{
			//Trigger next person Entering
			_customerQueue.MoveToEntrance(_allCustomersInLevel[_customerIndex]);

			if (_queueTimings.Count == 0)
			{
				//No more timings
				_runTimer = false;
				_elapsedTime = 0;
				return;
			}
			_nextKeypoint = _queueTimings.Dequeue();
			_customerIndex++;
		}
	}

	private void LoadQueueTimings()
	{
		_queueTimings.Clear();
		for (int i = 0; i < _allLevelsLines[DayManager.instance._dayIndex]._customerLine.Count; i++)
		{
			_queueTimings.Enqueue(_allLevelsLines[DayManager.instance._dayIndex]._lineActivateTimings[i]);
		}

		_nextKeypoint = _queueTimings.Dequeue();
		_customerIndex = 0;
	}

	public void StopQueueProgression()
	{
		_runTimer = false;
	}
}
