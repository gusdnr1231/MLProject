using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
	public int GhostCount { get; set; } = 0;
	private MeshRenderer mr;

	public Material DefaultMat;
	public Material WinMat;

	private void Start()
	{
		mr = GetComponent<MeshRenderer>();
	}

	private void Update()
	{
		if(GhostCount > 0)
		{
			mr.material = WinMat;
		}
		else
		{
			mr.material = DefaultMat;
		}
	}

	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.CompareTag("GHOST"))
		{
			GhostCount += 1;
		}
	}

	private void OnTriggerExit(Collider collision)
	{
		if (collision.gameObject.CompareTag("GHOST"))
		{
			GhostCount -= 1;
		}
	}
}
