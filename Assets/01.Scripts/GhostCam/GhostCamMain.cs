using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCamMain : MonoBehaviour
{
	private Stage stage;

	private void Start()
	{
		stage = transform.Find("Stage").GetComponent<Stage>();
	}


}
