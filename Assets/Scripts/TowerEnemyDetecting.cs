using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEnemyDetecting : MonoBehaviour
{
    public List<GameObject> enemies;
    public TowerControll towerControll;
    // Start is called before the first frame update
    void Start()
    {
        towerControll = transform.parent.GetComponent<TowerControll>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count > 0 && enemies[0].activeSelf == false)
        {
            enemies.RemoveAt(0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (towerControll.targetEnemy == null)
            {
                towerControll.targetEnemy = other.gameObject;
                towerControll.towerState = TowerControll.TOWERSTATE.ATTACK;
            }
            enemies.Add(other.gameObject);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        towerControll.targetEnemy = null;
        enemies.Remove(other.gameObject);
    }
}
