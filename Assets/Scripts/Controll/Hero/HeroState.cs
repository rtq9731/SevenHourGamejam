using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hero
{
	public partial class HeroState
	{
		public enum eState
		{
			IDLE, MOVE, ATTACK, DEAD
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
		protected float attackRange;
		protected float speed;
		bool facingRight;


		protected HeroState nextState;


		public HeroState(CONCharacter charactor, GameObject obj, Animator anim, Transform targetTrm, float attackRange)
		{
			myChar = charactor;
			myObj = obj;
			myAnim = anim;
			playerTrm = targetTrm;
			this.attackRange = attackRange;

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

		public HeroState Process()
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
			CONEntity nearObj = GameSceneClass.gMGPool.poolTotalDic[ePrefabs.Monster].Find(x => x.gameObject.activeSelf);
			if (nearObj != null)
			{
				float dir = Mathf.Abs(nearObj.gameObject.transform.position.x - myObj.transform.position.x);
				if (dir < attackRange)
				{
					return true;
				}
			}

			return false;
		}

	}
	public class Idle : HeroState
	{
		public float attackDelay = 0.5f;
		public float time;
		public Idle(CONCharacter charactor, GameObject obj, Animator anim, Transform targetTrm, float attackRange) : base(charactor, obj, anim, targetTrm, attackRange)
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
			if (CanAttackPlayer() && time < 0)
			{
				nextState = new Attack(myChar, myObj, myAnim, playerTrm, attackRange);
				curEvent = eEvent.EXIT;
			}
			if (!CanAttackPlayer())
			{
				nextState = new Move(myChar, myObj, myAnim, playerTrm, attackRange);
				curEvent = eEvent.EXIT;
			}
		}
		public override void Exit()
		{
			if (myAnim != null)
				myAnim.ResetTrigger("Idle");
			base.Exit();
		}
	}
	public class Move : HeroState
	{
		public Move(CONCharacter charactor, GameObject obj, Animator anim, Transform targetTrm, float attackRange) : base(charactor, obj, anim, targetTrm, attackRange)
		{
			stateName = eState.MOVE;
		}
		public override void Enter()
		{
			if (myAnim != null)
				myAnim.SetTrigger("Move");
			base.Enter();
		}
		public override void Update()
		{

			if (!CanAttackPlayer())
			{
				myObj.transform.position += new Vector3(myChar.myVelocity.x * Time.deltaTime, 0, 0);
			}
			else
			{
				nextState = new Idle(myChar, myObj, myAnim, playerTrm, attackRange);
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
	public class Attack : HeroState
	{
		public float time;
		public float delay = 0.5f;
		public Attack(CONCharacter charactor, GameObject obj, Animator anim, Transform targetTrm, float attackRange) : base(charactor, obj, anim, targetTrm, attackRange)
		{
			stateName = eState.ATTACK;
		}
		public override void Enter()
		{
			time = delay;
			if (myAnim != null)
			{
				myAnim.SetTrigger("Attack");
				Debug.Log("╬Нец");
			}
			CONEntity nearObj = GameSceneClass.gMGPool.poolTotalDic[ePrefabs.Monster].Find(x => x.gameObject.activeSelf);
			if(nearObj!=null)
			nearObj.gameObject.GetComponent<CONCharacter>().Hit(myChar.attackPower);

			base.Enter();
		}
		public override void Update()
		{
			time -= Time.deltaTime;
			if (!CanAttackPlayer() && time < 0)
			{
				nextState = new Move(myChar, myObj, myAnim, playerTrm, attackRange);
				curEvent = eEvent.EXIT;
			}
			else if (CanAttackPlayer() && time < 0)
			{
				nextState = new Idle(myChar, myObj, myAnim, playerTrm, attackRange);
				curEvent = eEvent.EXIT;
			}
		}
		public override void Exit()
		{
			if (myAnim != null)
			{
				myAnim.ResetTrigger("Attack");
			}

			base.Exit();
		}
	}
}
