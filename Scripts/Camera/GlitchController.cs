using System;
using UnityEngine;

public class GlitchController : MonoBehaviour
{
	[SerializeField] private Material _glitchMaterial;
	[SerializeField] private float _noiseAmount;
	[SerializeField] private float _glitchStrength;

	
	private void Start()
	{
		_glitchMaterial.SetFloat("_NoiseAmount", _noiseAmount);
		_glitchMaterial.SetFloat("_GlitchStrength", _glitchStrength);
	}

	private void OnDisable()
	{
		_glitchMaterial.SetFloat("_NoiseAmount", 0);
		_glitchMaterial.SetFloat("_GlitchStrength", 0);
	}
}
