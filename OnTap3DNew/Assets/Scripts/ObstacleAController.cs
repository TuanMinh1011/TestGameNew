using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacle
{
	void AttackPlayer(Transform playerTransform);
	void Deactivate();
}

public class ObstacleAController : MonoBehaviour, IObstacle
{
	[SerializeField] private float forceStrength;
	[SerializeField] private Material nearMaterial;

	public Action<int> OnTriggerAction;

	private Vector3 direction;
	private Vector3 target;
	private float distance;
	private Material defaultMaterial;

	private MeshRenderer meshRenderer;

	private void Awake()
	{
		meshRenderer = GetComponent<MeshRenderer>();
	}

	private void Update()
	{
		transform.Translate(direction * forceStrength * Time.deltaTime, Space.World);

		float distance = Vector3.Distance(transform.position, target);

		if (distance < 7f)
		{
			meshRenderer.material = nearMaterial;
		}
	}
	public void AttackPlayer(Transform playerTransform)
	{
		if (meshRenderer.material != nearMaterial)
		{
			defaultMaterial = meshRenderer.material;
		}

		this.gameObject.SetActive(true);

		if (playerTransform != null)
		{
			target = playerTransform.position;
			//Vector3 direction = (playerTransform.position - transform.position).normalized;
			//rb.AddForce(direction * forceStrength);
			direction = (playerTransform.position - transform.position);
			direction.Normalize();
		}
		else
		{
			Debug.LogError("Player Transform is null.");
		}
	}

	public void Deactivate()
	{
		this.gameObject.SetActive(false);
	}
	private void OnDisable()
	{
		meshRenderer.material = defaultMaterial;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Deactivate();
			OnTriggerAction.Invoke(10);
		}
	}
}
