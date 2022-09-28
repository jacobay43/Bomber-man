using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class BombBehaviour : MonoBehaviour
{
    [SerializeField] GameObject ExplosionEffect;
    [SerializeField] LayerMask LMask;
    
    private float SphereRadius;
    // Start is called before the first frame update
    void Start()
    {
        SphereRadius = GetComponent<SphereCollider>().radius * Mathf.Max(transform.lossyScale.x,transform.lossyScale.y,transform.lossyScale.z);
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(3f);

        //destroy surrounding objects within trigger
        DestroySurroundingObjects();
        
        //destroy bomb object
        Destroy(this.gameObject);
    }

    private Collider[] FilterOutDuplicates(Collider [] colliders)
    {
        List <Collider> Uniques = new List<Collider>();
        bool IsDuplicate;
        for (int i = 0; i < colliders.Length; i++)
        {
            IsDuplicate = false;
            for (int j = 0; j < i; j++)
            {
                if (colliders[i].transform.position == colliders[j].transform.position)
                {
                    IsDuplicate = true;
                    break;
                }
            }
            if (!IsDuplicate)
                Uniques.Add(colliders[i]);
        }

        return Uniques.ToArray();
    }
    private void DestroySurroundingObjects()
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, SphereRadius,LMask,QueryTriggerInteraction.Collide); //
        Collider[] uniqueObjects = FilterOutDuplicates(colliders);

        if (colliders.Length != uniqueObjects.Length)
            Debug.Log($"Filtered out {colliders.Length - uniqueObjects.Length} duplicates");
        //Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, LMask);
        foreach (var collider in uniqueObjects)
        {
            if (collider.gameObject.name.StartsWith("Destructible"))
            {
                collider.GetComponent<DestructibleObjectBehaviour>().Splatter();
            }
            else if (collider.gameObject.name.StartsWith("Enemy"))
            {
                collider.GetComponent<EnemyBehaviour>().Splatter();
            }
          
            else if (collider.gameObject.name.ToLower().StartsWith("player"))
            {
                collider.GetComponent<PlayerBehaviour>().Splatter();
            }
        }
    }

    private void OnDestroy()
    {
        
    }
}
