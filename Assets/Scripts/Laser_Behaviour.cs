using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Behaviour : MonoBehaviour
{
    [SerializeField]
    private float _speedVar = 7.5f;

    
    void Update()
    {
        transform.Translate(Vector3.up * _speedVar * Time.deltaTime);

        if (transform.position.y > 7.5f)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject); 
        }
       
    }
}
