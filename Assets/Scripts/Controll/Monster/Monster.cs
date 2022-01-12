using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : CONCharacter
{
	Animator myAnim;

	public Transform playerTrm;

	State curState;

	public override void Start()
	{
		//myAnim = this.GetComponent<Animator>();
		playerTrm = FindObjectOfType<ConCastle>().transform;
		curState = new Move(this,gameObject, myAnim, playerTrm);
	}

	public override void Update()
	{
		curState = this.curState.Process();
	}
}
