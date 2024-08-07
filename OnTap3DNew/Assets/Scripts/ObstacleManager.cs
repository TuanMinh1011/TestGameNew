using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleManager : MonoBehaviour
{
	[SerializeField] private Transform playerTransform;
	[SerializeField] private ObjectPool obstancleA;
	[SerializeField] private ObjectPool obstancleB;
	[SerializeField] private ObjectPool scorePool;

	[SerializeField] private float spawnDistance = 10f;
	[SerializeField] private float spawnDistanceScore = 20f;

	[SerializeField] private GameManager gameManager;

	private void Start()
	{
		InvokeRepeating("SpawnObstacles", 2f, 5f);
		InvokeRepeating("SpawnScore", 0f, 10f);
	}

	private void SpawnScore()
	{
		Vector3 randomPosition = playerTransform.position + (Random.insideUnitSphere * spawnDistanceScore);
		randomPosition.y = playerTransform.position.y;

		var score = scorePool.GetPooledObject();
		score.transform.position = randomPosition;
		score.SetActive(true);
		score.GetComponent<ScoreController>().onCollected.AddListener(gameManager.PlusScore);
	}

	private void SpawnObstacles()
	{
		float angle = Random.Range(0f, Mathf.PI * 2);
		Debug.Log("angle: " + angle);
		Debug.Log("Mathf.PI: " + Mathf.Rad2Deg * angle);
		float x = Mathf.Cos(angle) * spawnDistance;
		Debug.Log("x C"+ Mathf.Cos(angle));
		Debug.Log("x "+ x);
		float z = Mathf.Sin(angle) * spawnDistance;
		Debug.Log("z S	"+ Mathf.Sin(angle));
		Debug.Log("z "+ z);

		Vector3 spawnPosition = new Vector3(playerTransform.position.x + x, playerTransform.position.y, playerTransform.position.z + z);
		spawnPosition.y = playerTransform.position.y;

		int spawnIndex = Random.Range(0, 2);

		if (spawnIndex == 0)
		{
			var obstacles = obstancleA.GetPooledObject();
			obstacles.transform.position = spawnPosition;
			var oA = obstacles.GetComponent<ObstacleAController>();
			oA.AttackPlayer(playerTransform);
			oA.OnTriggerAction += gameManager.MinusHealth;
			StartCoroutine(DestroyObstaclesA(oA));
		}
		else
		{
			var obstacles = obstancleB.GetPooledObject();
			obstacles.transform.position = spawnPosition;
			var oB = obstacles.GetComponent<ObstacleBController>();
			oB.AttackPlayer(playerTransform);
			oB.OnTriggerAction += gameManager.MinusHealth;
			StartCoroutine(DestroyObstaclesB(oB));
		}
	}

	private IEnumerator DestroyObstaclesA(ObstacleAController oA)
	{
		yield return new WaitForSeconds(10f);
		if (oA.gameObject.activeInHierarchy)
		{
			oA.Deactivate();
		}
	}

	private IEnumerator DestroyObstaclesB(ObstacleBController oA)
	{
		yield return new WaitForSeconds(10f);
		if (oA.gameObject.activeInHierarchy)
		{
			oA.Deactivate();
		}
	}
}
