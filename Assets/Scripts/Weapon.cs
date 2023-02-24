using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireTime;
    public float bulletSpeed;
    public Transform fireTrans;
    public GameObject fireBullet;

    public float minRadius;
    public float maxRadius;
    public float strength;
    public float hardness;

    float timer = 0;
    public void Fire()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit raycasthit;

        Vector3 target = Camera.main.transform.forward * 800;

        if (timer <= 0)
        {
            GameObject bullet = Instantiate(fireBullet, fireTrans.position, Quaternion.LookRotation(target));
            bullet.GetComponent<Bullet>().SetBullet(bulletSpeed, bullet.transform.forward ,minRadius,maxRadius,strength,hardness);
           timer = fireTime;
        }
        else
        {
            timer -= Time.deltaTime * 50;
        }
    }
}
