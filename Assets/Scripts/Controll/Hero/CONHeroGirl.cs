using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CONHeroGirl : CONHero
{
	public Transform playerTrm;

	hero.HeroState curState;
	private Vector2 startPos;

	public override void Start()
	{
		startPos = transform.position;
		Spawner.instance.onWaveFinsh += () => transform.position = startPos;
		//myAnim = this.GetComponent<Animator>();
		CONEntity nearObj = GameSceneClass.gMGPool.poolTotalDic[ePrefabs.Monster].Find(x => x.gameObject.activeSelf);
		curState = new hero.Move(this, gameObject, _anim, playerTrm, attackRange);
	}

	public override void Update()
	{
		curState = this.curState.Process();
	}
}
