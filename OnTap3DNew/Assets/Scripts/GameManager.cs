using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI healthText;
	[SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private DataManager dataManager;
	[SerializeField] private PlayerData playerData;

	private int maxHealth = 100;
	private int currentHealth = 0;

	private int currentScore = 0;

	private void Start()
	{
		playerData = dataManager.LoadData();
		if (playerData == null)
		{
			playerData = new PlayerData();
		}

		currentScore = playerData.score;
		currentHealth = maxHealth;
	}

	private void Update()
	{
		healthText.text = currentHealth.ToString();
		scoreText.text = currentScore.ToString();

		if (Input.GetKeyDown(KeyCode.V))
		{
			playerData.score = currentScore;
			dataManager.SaveData(playerData);
		}
	}

	public void MinusHealth(int health)
	{
		currentHealth -= health;
	}

	public void PlusScore(int score)
	{
		currentScore += score;
	}

	void OnApplicationQuit()
	{
		dataManager.SaveData(playerData);
	}

	void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			dataManager.SaveData(playerData);
		}
	}
}
