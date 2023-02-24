using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public ICollectable weaponUp;

    public GameObject[] poweUPs;


    private void Start()
    {
        StartCoroutine("powerUp");

    }
    public void FixedUpdate()
    {
        
    }

    IEnumerator powerUp()
    {
        GameObject obj = poweUPs[Random.Range(0, poweUPs.Length)];
        float x = Random.Range(-14, 14);
        float y = Random.Range(-25, 25);
        Vector3 pos = new Vector3(x,20,y);

        Instantiate(obj, pos, Quaternion.identity);
        yield return new WaitForSeconds(30);
    }

   

}
