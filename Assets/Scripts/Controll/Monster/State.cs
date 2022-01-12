using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class State
{

	public enum eState
	{
		IDLE,MOVE, ATTACK, DEAD
	};

	public enum eEvent
	{
		ENTER, UPDATE, EXIT
	};

	public eState stateName;
	protected eEvent curEvent;

	protected CONCharacter myChar;
	protected GameObject myObj;
	protected Animator myAnim;
	protected Transform playerTrm;


	protected State nextState;

	float detectDist = 10.0f;
	float detectAngle = 30.0f;
	float shotDist = 7.0f;

	public State(CONCharacter charactor,GameObject obj, Animator anim, Transform targetTrm)
	{
		myChar = charactor;
		myObj = obj;
		myAnim = anim;
		playerTrm = targetTrm;

		curEvent = eEvent.ENTER;
	}

	public virtual void Enter()
	{
		curEvent = eEvent.UPDATE;
	}
	public virtual void Update()
	{
		curEvent = eEvent.UPDATE;
	}
	public virtual void Exit()
	{
		curEvent = eEvent.EXIT;
	}

	public State Process()
	{
		if (curEvent == eEvent.ENTER) Enter();
		if (curEvent == eEvent.UPDATE) Update();
		if (curEvent == eEvent.EXIT)
		{
			Exit();
			return nextState;
		}
		return this;
	}
	public bool CanAttackPlayer()
	{
		Vector3 dir = playerTrm.position - myObj.transform.position;
		if (dir.magnitude < shotDist)
		{
			return true;
		}
		return false;
	}

}
public class Idle : State
{
	public float attackDelay = 2f;
	public float time;
	public Idle(CONCharacter charactor, GameObject obj, Animator anim, Transform targetTrm) : base(charactor, obj, anim, targetTrm)
	{
		stateName = eState.IDLE;
		time = attackDelay;
	}
	public override void Enter()
	{
		if (myAnim != null)
			myAnim.SetTrigger("Idle");
		base.Enter();
	}
	public override void Update()
	{
		time -= Time.deltaTime;
		if (CanAttackPlayer()&&time<0)
		{
			nextState = new Attack(myChar,myObj, myAnim, playerTrm);
			curEvent = eEvent.EXIT;
		}
		if (!CanAttackPlayer())
		{
			nextState = new Move(myChar,myObj, myAnim, playerTrm);
			curEvent = eEvent.EXIT;
		}
	}
	public override void Exit()
	{
		if (myAnim != null)
			myAnim.ResetTrigger("isIdle");
		base.Exit();
	}
}
public class Move : State
{
	public Move(CONCharacter charactor, GameObject obj, Animator anim, Transform targetTrm) : base(charactor, obj, anim, targetTrm)
	{
		stateName = eState.MOVE;
	}
	public override void Enter()
	{
		if(myAnim!=null)
		myAnim.SetTrigger("Move");
		base.Enter();
	}
	public override void Update()
	{
		if (!CanAttackPlayer())
		{
			myObj.transform.position += new Vector3(-1*myChar.myVelocity.x*Time.deltaTime, 0, 0);
		}
		else
		{
			nextState = new Idle(myChar, myObj, myAnim, playerTrm);
			curEvent = eEvent.EXIT;
		}
	}
	public override void Exit()
	{
		if (myAnim != null)
			myAnim.ResetTrigger("Move");
		base.Exit();
	}
}
public class Attack : State
{
	public Attack(CONCharacter charactor, GameObject obj, Animator anim, Transform targetTrm) : base(charactor, obj, anim, targetTrm)
	{
		stateName = eState.ATTACK;
	}
	public override void Enter()
	{
		if (myAnim != null)
			myAnim.SetTrigger("Attack");
		Debug.Log("╬Нец");
		playerTrm.gameObject.GetComponent<CONCharacter>().Hit(myChar.attackPower);
		base.Enter();
		if (!CanAttackPlayer())
		{
			nextState = new Move(myChar, myObj, myAnim, playerTrm);
			curEvent = eEvent.EXIT;
		}
		else
		{
			nextState = new Idle(myChar, myObj, myAnim, playerTrm);
			curEvent = eEvent.EXIT;
		}
	}
	public override void Exit()
	{
		if (myAnim != null)
			myAnim.ResetTrigger("Attack");
		base.Exit();
	}
}