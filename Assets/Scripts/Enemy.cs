using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    private Animator an;
    private void Start()
    {
        an = this.GetComponent<Animator>();
    }
    public void GetDamage()
    {
        an.SetTrigger("Die");
    }
}
