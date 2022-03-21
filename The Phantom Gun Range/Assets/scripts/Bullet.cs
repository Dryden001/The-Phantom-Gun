using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //remove bullet on dissapear
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    //remove bullet on collision
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
