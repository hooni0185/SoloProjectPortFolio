using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float deadTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, deadTime);
    }

}
