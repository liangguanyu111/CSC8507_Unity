using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    float speed;
    Vector3 dir;
    float minRadius;
    float maxRadius;
    float strength;
    float hardness;
    void Update()
    {
        this.transform.position = this.transform.position + dir * speed * Time.deltaTime;
    }

    public void SetBullet(float speed, Vector3 dir,float minRadius,float maxRadius,float strength,float hardness)
    {
        this.minRadius = minRadius;
        this.maxRadius = maxRadius;
        this.speed = speed;
        this.dir = dir;
        this.strength = strength;
        this.hardness = hardness;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable;
        Paintable p = collision.gameObject.GetComponent<Paintable>();

       
        if (p != null)
        {
            Vector3 location = this.transform.position;
            Vector3 closestPoint = collision.collider.ClosestPoint(location);

   
             float radius = Random.Range(minRadius, maxRadius);
             Color paintColor = new Color(255, 0, 255); //Load from manager
             PaintManager.instance.paint(p, closestPoint, radius, hardness, strength, paintColor);

        }
        if(collision.gameObject.TryGetComponent<IDamageable>(out damageable))
        {
            damageable.GetDamage();
        }

        Destroy(this.gameObject);
    }
}
