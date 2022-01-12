using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttackSword : CONEntity
{
    [SerializeField] float disappearTime = 3f;

    Animator _anim = null;

    float _damage = 0f;
    float _speed = 0f;
    float _timer = 0f;
    float _speedtimer = 0f;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void SetAttack(Vector3 target, float speed, float damage)
    {
        _damage = damage;
        _speed = speed;

        _timer = 0f;
        _speedtimer = 0f;
        _anim.speed = 1;

        StartCoroutine(MoveToTarget(target));
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= disappearTime)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.GetComponent<Monster>()?.Hit(_damage);
        gameObject.SetActive(false);
    }

    public IEnumerator MoveToTarget(Vector3 target)
    {
        Vector3 p1, p2, midPoint;

        p1 = transform.position;
        p2 = target;
        midPoint = new Vector2(target.x - transform.position.x, target.y + transform.position.y);

        while (Vector3.Distance(target, transform.position) >= 0.01f)
        {
            Vector3 tmp1 = Vector2.Lerp(p1, midPoint, _speedtimer);
            Vector3 tmp2 = Vector2.Lerp(midPoint, p2, _speedtimer);
            transform.position = Vector2.Lerp(tmp1, tmp2, _speedtimer);

            _speedtimer += Time.deltaTime * _speed;
            yield return null;
        }
        _anim.speed = 0;

    }
}
