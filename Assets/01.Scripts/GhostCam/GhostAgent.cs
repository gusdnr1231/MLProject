using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class GhostAgent : Agent
{
	private Rigidbody ghostRB;
	private Animator ghostAnim;
	private GhostCamMain gcMain;

	public float moveSpeed;
	public float turnSpeed;
	public float attackTime;
	public bool isEnemy;
	private bool CanAttack = true;

	public override void Initialize()
	{
		ghostRB = GetComponent<Rigidbody>();
		ghostAnim = transform.Find("Visual").GetComponent<Animator>();
		gcMain = transform.parent.GetComponent<GhostCamMain>();
	}

	public override void OnEpisodeBegin()
	{
		ghostRB.velocity = ghostRB.angularVelocity = Vector3.zero;
		CanAttack = true;
		transform.localRotation = Quaternion.identity;
	}

	public override void CollectObservations(VectorSensor sensor)
	{
		sensor.AddObservation(isEnemy ? gcMain.ECount : gcMain.PCount);
	}

	public override void OnActionReceived(ActionBuffers actions)
	{
		var DiscreteActions = actions.DiscreteActions;

		Vector3 direction = Vector3.zero;
		Vector3 rotationAxis = Vector3.zero;

		// DiscreteAction[0] : 정지(0), 전진(1), 후진(2)
		switch (DiscreteActions[0])
		{
			case 1: direction = transform.forward; break;
			case 2: direction = -transform.forward; break;
		}

		// DiscreteAction[1] : 정지(0), 좌회전(1), 우회전(2)
		switch (DiscreteActions[1])
		{
			case 1: rotationAxis = Vector3.down; break;
			case 2: rotationAxis = Vector3.up; break;
		}

		ghostRB.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
		transform.Rotate(rotationAxis, turnSpeed * Time.fixedDeltaTime);

		if((isEnemy ? gcMain.ECount : gcMain.PCount) > (isEnemy ? gcMain.PCount : gcMain.ECount))
		{
			//AddReward(0.5f);
		}

		//AddReward(-1f / MaxStep);
	}

	public override void Heuristic(in ActionBuffers actionsOut)
	{
		var DiscreteActionsOut = actionsOut.DiscreteActions;

		if (Input.GetKey(KeyCode.W))
		{
			DiscreteActionsOut[0] = 1;
		}
		if (Input.GetKey(KeyCode.S))
		{
			DiscreteActionsOut[0] = 2;
		}
		if (Input.GetKey(KeyCode.A))
		{
			DiscreteActionsOut[1] = 1;
		}
		if (Input.GetKey(KeyCode.D))
		{
			DiscreteActionsOut[1] = 2;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("STAGE"))
			Debug.Log("In Stage");
	}
	
	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.CompareTag("STAGE"))
			Debug.Log("Out Stage");
	}

}
