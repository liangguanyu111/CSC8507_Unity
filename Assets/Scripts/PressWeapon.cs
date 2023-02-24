using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressWeapon : Weapon
{
    public bool fire = false;

    public ParticleSystem bullet;

    private void Update()
    {
        if(fire)
        {
            bullet.gameObject.SetActive(true);
            bullet.Play();
        }
        else
        {
            bullet.gameObject.SetActive(false);
            bullet.Pause();
        }
    }

}
