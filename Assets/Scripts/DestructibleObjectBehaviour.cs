using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjectBehaviour : MonoBehaviour
{
    [SerializeField] GameObject ObjectExplosion;
    private GameObject Explosion;
    public void Splatter()
    {
        Debug.Log("Splatter called for object");
        if (ObjectExplosion != null)
        {
            Explosion = Instantiate(ObjectExplosion) as GameObject;
            Explosion.transform.position = transform.position;
            Destroy(Explosion, 1f);
        }

        Destroy(this.gameObject);
    }
}
