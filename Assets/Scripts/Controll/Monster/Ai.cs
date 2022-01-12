using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : MonoBehaviour
{
	Animator myAnim;

	public Transform playerTrm;

	State curState;

	private void Start()
	{
		myAnim = this.GetComponent<Animator>();

		curState = new Idle(gameObject, myAnim, playerTrm);
	}
	private void Update()
	{
		curState = this.curState.Process();
	}
}
