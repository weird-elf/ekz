using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnResources : MonoBehaviour
{
	[SerializeField] private Transform _minPosition;
	[SerializeField] private Transform _maxPosition;
	[SerializeField] private Resource _resource;
	[SerializeField] private Transform _container;

	[SerializeField] private float _spawnTime;
	private Resource _tempResource;
	private WaitForSeconds _wait;

	private void Start()
	{
		_wait = new WaitForSeconds(_spawnTime);
		StartCoroutine(Spawn());
	}

	private IEnumerator Spawn()
	{
		while (enabled)
		{
			_tempResource = Instantiate(_resource, _container);
			_tempResource.transform.position = new Vector3(Random.Range(_minPosition.transform.position.x, _maxPosition.transform.position.x), 0, Random.Range(_minPosition.transform.position.z, _maxPosition.transform.position.z));
			yield return _wait;
		}
	}
}
