using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayeMovementController : MonoBehaviour {

	public Camera mainCamera;
	public NavMeshAgent agent;
	private Animator animator;
	private bool isIdle = true;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		animationTrigger ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = mainCamera.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {
				agent.SetDestination (hit.point);
				isIdle = false;
				animationTrigger ();
			}
		}

		if (!agent.pathPending)	{
			if (agent.remainingDistance <= agent.stoppingDistance){
				if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f){
					isIdle = true;
					animationTrigger ();
				}
			}
		}

	}

	private void animationTrigger() {
		if (isIdle == true) {
			animator.SetBool ("willRun", false);
			animator.SetBool ("willStop", true);
		} else {
			animator.SetBool ("willRun", true);
			animator.SetBool ("willStop", false);
		}
	}
}
