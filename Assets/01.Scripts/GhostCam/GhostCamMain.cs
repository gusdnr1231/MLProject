using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCamMain : MonoBehaviour
{
	private Stage stage;
	public int PCount
	{
		get
		{
			return stage.P_GhostCount;
		}
		private set
		{
			stage.P_GhostCount = value;
		}
	}
	public int ECount
	{
		get
		{
			return stage.E_GhostCount;
		}
		private set
		{
			stage.E_GhostCount = value;
		}
	}


	private void Start()
	{
		stage = transform.Find("Stage").GetComponent<Stage>();
		InvokeRepeating(nameof(DebugRepeat), 1f, 5f);
	}

	private void DebugRepeat()
	{
		Debug.Log($"COUNT : P [{PCount}] E [{ECount}]");
	}
}
