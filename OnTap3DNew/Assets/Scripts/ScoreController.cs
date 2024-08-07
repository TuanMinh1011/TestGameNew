using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreController : MonoBehaviour
{
	public UnityEvent<int> onCollected;

	private void OnTriggerEnter(Collider collision)
	{
		if (collision.CompareTag("Player"))
		{
			onCollected.Invoke(10);
			this.gameObject.SetActive(false);
		}
	}
}
