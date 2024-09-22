using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private DamageReceiver damageReceiver;

    public virtual void SendToTarget(Transform obj)
    {
        if (damageReceiver == null) return;
        this.Send(damageReceiver);
    }

    public virtual void Send(DamageReceiver damageReceiver)
    {
        damageReceiver.Deduct(this.damage);
        damageReceiver.HurtAnimation(true);
        StartCoroutine(ShowHurtAnim(damageReceiver));

    }

    public virtual void SetColliderEnable(bool colStatus)
    {
        col.enabled = colStatus;
        damageReceiver.Col.enabled = colStatus;
    }

    private IEnumerator ShowHurtAnim(DamageReceiver damageReceiver)
    {
        yield return new WaitForSeconds(0.2f);
        damageReceiver.HurtAnimation(false);
    }
    
    private void OnTriggerEnter2D (Collider2D collider)
    {
        Debug.Log(collider.name, collider.gameObject);
        this.SendToTarget(collider.transform);
    }
}
