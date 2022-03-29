using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public GameObject target;
    public int bulletDamage;
    
    // Update is called once per frame
    void Update()
    {
        
        if (target != null)
        {
            transform.LookAt(target.transform);
            transform.Translate(0, 0, bulletSpeed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
