using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
	public int P_GhostCount { get; set; } = 0;
	public int E_GhostCount { get; set; } = 0;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("P_GHOST"))
		{
			P_GhostCount += 1;
		}
		if (collision.gameObject.CompareTag("E_GHOST"))
		{
			E_GhostCount += 1;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.CompareTag("P_GHOST"))
		{
			P_GhostCount -= 1;
		}
		if (collision.gameObject.CompareTag("E_GHOST"))
		{
			E_GhostCount -= 1;
		}
	}
}
