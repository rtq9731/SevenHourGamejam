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

	protected GameObject myObj;
	protected Animator myAnim;
	protected Transform playerTrm;


	protected State nextState;

	float detectDist = 10.0f;
	float detectAngle = 30.0f;
	float shotDist = 7.0f;

	public State(GameObject obj, Animator anim, Transform targetTrm)
	{
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
	public Idle(GameObject obj, Animator anim, Transform targetTrm) : base(obj, anim, targetTrm)
	{
		stateName = eState.IDLE;
		time = attackDelay;
	}
	public override void Enter()
	{
		myAnim.SetTrigger("Idle");
		base.Enter();
	}
	public override void Update()
	{
		time -= Time.deltaTime;
		if (CanAttackPlayer()&&time<0)
		{
			nextState = new Attack(myObj, myAnim, playerTrm);
			curEvent = eEvent.EXIT;
		}
	}
	public override void Exit()
	{
		myAnim.ResetTrigger("isIdle");
		base.Exit();
	}
}
public class Move : State
{
	public Move(GameObject obj, Animator anim, Transform targetTrm) : base(obj, anim, targetTrm)
	{
		stateName = eState.IDLE;
	}
	public override void Enter()
	{
		myAnim.SetTrigger("Move");
		base.Enter();
	}
	public override void Update()
	{
		if (!CanAttackPlayer())
		{
			myObj.transform.position += new Vector3(-1, 0, 0);
		}
	}
	public override void Exit()
	{
		myAnim.ResetTrigger("Move");
		base.Exit();
	}
}
public class Attack : State
{
	public Attack(GameObject obj, Animator anim, Transform targetTrm) : base(obj, anim, targetTrm)
	{
		stateName = eState.IDLE;
	}
	public override void Enter()
	{
		myAnim.SetTrigger("Attack");
		base.Enter();
		nextState = new Idle(myObj, myAnim, playerTrm);
		curEvent = eEvent.EXIT;
	}
	public override void Exit()
	{
		myAnim.ResetTrigger("Attack");
		base.Exit();
	}
}