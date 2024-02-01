using TMPro;
using UnityEngine;

public class Resource : MonoBehaviour
{
	[SerializeField] private int _amount;
	[SerializeField] private TextMeshProUGUI _resourceLabel;

	public int GetAmount => _amount;
	
	private void Start()
	{
		_resourceLabel.text = "x" + _amount.ToString();
	}

	public void IncreaseAmount()
	{
		_amount++;
		_resourceLabel.text = "x" + _amount.ToString();
	}
	
	public bool DecreaseAmount()
	{
		if (_amount <= 0)
		{
			return false;
		}
		
		_amount--;
		_resourceLabel.text = "x" + _amount.ToString();
		return true;
	}

	public void ResetAmount()
	{
		_amount = 0;
		_resourceLabel.text = _amount.ToString();
	}
}
