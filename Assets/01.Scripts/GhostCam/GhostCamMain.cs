using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCamMain : MonoBehaviour
{
	private Stage stage;
	public int GCount
	{
		get
		{
			return stage.GhostCount;
		}
		private set
		{
			stage.GhostCount = value;
		}
	}

	public Transform ObstacleContainer;
	public GameObject ObstaclePrefab;
	[HideInInspector] public List<GameObject> Obstacles = new List<GameObject>();

	private void Start()
	{
		stage = transform.Find("Stage").GetComponent<Stage>();
	}

	public void SettingObstacle(int count)
	{
		if(Obstacles != null)
		{
			for(int obj = 0; obj < Obstacles.Count; obj++)
			{
				Destroy(Obstacles[obj]);
			}
			Obstacles.Clear();
			Obstacles = new List<GameObject>();
		}
		for(int c = 0; c  < count; c++)
		{
			GameObject obstacle = Instantiate(ObstaclePrefab, ObstacleContainer);
			obstacle.transform.localPosition = new Vector3(Random.Range(-11f, 11f), 1.5f, Random.Range(-11f, 11f));
			Obstacles.Add(obstacle);
		}
	}
}
