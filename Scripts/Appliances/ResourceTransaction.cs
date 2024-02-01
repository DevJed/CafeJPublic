using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResourceTransaction : MonoBehaviour
{
	public static ResourceTransaction instance;

	[SerializeField] private CustomerQueue _customerQueue;
	
	[SerializeField] private Resource _primaryResource;
	[SerializeField] private Resource _secondaryResource;
	[SerializeField] private Resource _ternaryResource;

	[Header("Debug")]
	[SerializeField] private List<ResourceTypes> _currentOrder = new List<ResourceTypes>();

	private GameObject _customerOrderUIParent;
	
	/// <summary>
	/// Singleton Set up
	/// </summary>
	private void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}

		instance = this;
	}

	public void CustomerOrderRequest(List<ResourceTypes> newOrder, GameObject ordersParent)
	{
		if (_currentOrder.Count != 0)
		{
			Debug.LogError("Order is not finished, should not be added");
		}

		_currentOrder = newOrder;
		_customerOrderUIParent = ordersParent;
	}
	
	public void GivePrimaryResource(InputAction.CallbackContext context)
	{
		if (context.started || context.canceled) return;
        if (_primaryResource.GetAmount > 0 && CheckResource(ResourceTypes.Coffee))
		{
			_primaryResource.DecreaseAmount();
            CheckRemainingOrder();
		}
		else
		{
			//Negative UX 
            AudioManager.Instance.PlaySFX("CustomerIncorrect");
		}
	}

	public void GiveSecondaryResource(InputAction.CallbackContext context)
	{
		if (context.started || context.canceled) return;
        if (_secondaryResource.GetAmount > 0 && CheckResource(ResourceTypes.Juice))
		{
			_secondaryResource.DecreaseAmount();
            CheckRemainingOrder();
		}
		else
		{
			//Negative UX 
            AudioManager.Instance.PlaySFX("CustomerIncorrect");
		}
	}

	public void GiveTernaryResource(InputAction.CallbackContext context)
	{
		if (context.started || context.canceled) return;
        if (_ternaryResource.GetAmount > 0 && CheckResource(ResourceTypes.HotChocolate))
		{
			_ternaryResource.DecreaseAmount();
			CheckRemainingOrder();
		}
		else
		{
			//Negative UX 
            AudioManager.Instance.PlaySFX("CustomerIncorrect");
		}
	}

	private void CheckRemainingOrder()
	{
		//Check if list is empty!
		if (_currentOrder.Count == 0)
		{
			AudioManager.Instance.PlaySFX("CustomerSuccessful");
			_customerQueue.MakeCustomerLeave();
		}
	}

	//REWRITE FOR ENUMS
	public bool CheckResource(ResourceTypes target)
	{
		if (_currentOrder.Contains(target))
		{
			//Destroys bubble so that it cannot be seen again. 
			int index = _currentOrder.IndexOf(target);
			Destroy(_customerOrderUIParent.transform.GetChild(index).gameObject);
			
			_currentOrder.Remove(target);
			return true;
		}

		return false;
	}

	public void ResetResources()
	{
		_primaryResource.ResetAmount();
		_secondaryResource.ResetAmount();
		_ternaryResource.ResetAmount();

		_currentOrder.Clear();
	}
}

public enum ResourceTypes
{
	Coffee,
	HotChocolate,
	Juice
}

