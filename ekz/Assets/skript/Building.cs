using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Building : MonoBehaviour
{
	[SerializeField] private Vector2Int _size = Vector2Int.one;

	private void OnDrawGizmosSelected()
	{
		for (int i = 0; i < _size.x; i++)
		{
			for (int j = 0; j < _size.y; j++)
			{
				Gizmos.color = new Color(0.88f, 0.1f, 0.1f, 0.3f);
				Gizmos.DrawCube(transform.position + new Vector3(i, 0, j), new Vector3(1, 0.1f, 1));
			}
		}
	}

	public Building FlagPosition()
	{
		return this;
	}
}
