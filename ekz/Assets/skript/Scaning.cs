using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaning : MonoBehaviour
{
	[SerializeField] private float _radiusScan;
	[SerializeField] private float _pointRadius;

	private LayerMask _layerMask = 9;
	private Queue<Resource> resources = new Queue<Resource>();

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;

		Gizmos.DrawSphere(transform.position, _pointRadius);

		Gizmos.DrawWireSphere(transform.position, _radiusScan);
	}

	public Queue<Resource> Scaner(Queue<Resource> resources)
	{
		Collider[] hit = Physics.OverlapSphere(transform.position, _radiusScan, _layerMask);

		for (int i = 0; i < hit.Length; i++)
		{
			if (hit[i].TryGetComponent(out Resource resource))
			{
				if (!resources.Contains(resource) && !resource.transform.GetComponentInParent<Unit>())
				{
					resources.Enqueue(resource);
				}
			}
		}

		return resources;
	}

}
