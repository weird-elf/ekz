using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
	public void Destroed()
	{
		Destroy(gameObject);
	}

	public void Off()
	{
		this.enabled = false;
	}
}
