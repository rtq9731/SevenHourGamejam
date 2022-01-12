using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public static Spawner instance{ get; private set; }

	private HealthBar waveBar;
	List<CONEntity> monsterList = new List<CONEntity>();

	public event Action onWaveFinsh;

	public bool waveStart = true;
	public int curWave = 0;

	private void Awake()
	{
		instance = this;
	}

    private void Start()
    {
		waveBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
		WaveStart();
	}

    private void Update()
    {
		waveBar.UpdateHealthBar(monsterList.Count, monsterList.FindAll(x => x.gameObject.activeSelf).Count, $"Wave {curWave}");
		if(monsterList.FindAll(x => x.gameObject.activeSelf).Count <= 0 && waveStart)
        {
			onWaveFinsh?.Invoke();
			WaveStart();
		}
    }

    public void WaveStart()
	{
		curWave++;
		waveStart = false;
		StartCoroutine(MonsterSpawn(curWave*5, transform.position));
	}
	public IEnumerator MonsterSpawn(int monsterCount,Vector2 spawnPos)
	{
		yield return new WaitForSeconds(1f);
		waveStart = true;
		for (int i = 0; i < monsterCount; i++)
		{
			CONEntity monsterCon = GameSceneClass.gMGPool.CreateObj(ePrefabs.Monster, spawnPos+UnityEngine.Random.insideUnitCircle);
			monsterList.Add(monsterCon);
			//Random
			yield return new WaitForSeconds(0.1f);
		}

		//heroCon.GetComponent<CONCharacter>()
	}
}
