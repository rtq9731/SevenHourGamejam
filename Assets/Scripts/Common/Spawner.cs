using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public void MonsterSpawn(Vector3 spawnPos)
	{
		CONEntity heroCon = GameSceneClass.gMGPool.CreateObj(ePrefabs.Monster, spawnPos);
		//heroCon.GetComponent<CONCharacter>()
	}
}
