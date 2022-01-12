using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public bool waveStart;
	public int curWave = 0;


	public void WaveStart()
	{
		curWave++;
		waveStart = true;
		StartCoroutine(MonsterSpawn(curWave*5, transform.position));
	}
	public IEnumerator MonsterSpawn(int monsterCount,Vector2 spawnPos)
	{
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < monsterCount; i++)
		{
			CONEntity monsterCon = GameSceneClass.gMGPool.CreateObj(ePrefabs.Monster, spawnPos+Random.insideUnitCircle);
			//Random
			yield return new WaitForSeconds(0.1f);
		}

		//heroCon.GetComponent<CONCharacter>()
	}
}
