using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MummyGoAgent : Agent
{
    public Material goodMaterial;
    public Material badMaterial;
    public Material originMaterial;
    private Renderer floorRenderer;

    public Transform targetTransform;
    private new Rigidbody rigidbody;
    public override void Initialize()
    {
        rigidbody = GetComponent<Rigidbody>();
        floorRenderer = transform.parent.Find("Floor").GetComponent<Renderer>();
        originMaterial = floorRenderer.material;
    }

    public override void OnEpisodeBegin()
    {
        rigidbody.velocity = Vector3.zero;

        transform.localPosition = new Vector3(Random.Range(-4f, 4f), 0.05f, Random.Range(-4f, 4f));
        targetTransform.localPosition = new Vector3(Random.Range(-4f, 4f), 0.55f, Random.Range(-4f, 4f));
        StartCoroutine(RecoverFloor());
    }

    private IEnumerator RecoverFloor()
    {
        yield return new WaitForSeconds(0.2f);
        floorRenderer.material = originMaterial;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(targetTransform.position);
        sensor.AddObservation(transform.position);
        sensor.AddObservation(rigidbody.velocity.x);
        sensor.AddObservation(rigidbody.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var continuousActions = actions.ContinuousActions;
        Vector3 direction = (Vector3.forward * continuousActions[0]) + (Vector3.right * continuousActions[1]);
        direction.Normalize();
        rigidbody.AddForce(direction * 50f);

        SetReward(-0.01f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var ContinuosActionsOut = actionsOut.ContinuousActions;
        ContinuosActionsOut[0] = Input.GetAxis("Vertical"); 
        ContinuosActionsOut[1] = Input.GetAxis("Horizontal");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("WALL"))
        {
            floorRenderer.material = badMaterial;
            SetReward(-1f);
            EndEpisode();
        }
        if (collision.collider.CompareTag("TARGET"))
        {
            floorRenderer.material = goodMaterial;
            SetReward(1f);
            EndEpisode();
        }
    }
}
