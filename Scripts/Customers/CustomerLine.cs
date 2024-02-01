using System;
using System.Collections.Generic;

[Serializable]
public class CustomerLine
{
	public List<float> _lineActivateTimings = new List<float>();
	public List<Customer> _customerLine = new List<Customer>();
}
