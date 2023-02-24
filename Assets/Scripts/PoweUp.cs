using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweUp : MonoBehaviour, ICollectable
{
    public virtual void Interct(GameObject obj)
    {
      
    }

    private void Update()
    {
        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y + 4f, this.transform.eulerAngles.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Interct(other.gameObject);
        }
    }
}
