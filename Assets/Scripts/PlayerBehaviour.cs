using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] GameObject PlayerExplosion;
    [SerializeField] GameObject BombObject;
    [SerializeField] Transform target;
    private TheSceneManager SceneManager;
    private bool canDropBomb = true;
    private GameObject Explosion;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<TheSceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDropBomb && !SceneManager.IsGameOver)
        {
            canDropBomb = false;
            StartCoroutine(BombCanDrop());
            DropBomb();
        }
    }

    private IEnumerator BombCanDrop()
    {
        yield return new WaitForSeconds(3f);// after 3 seconds, it is guaranteed that the other bomb would have exploded allowing you to drop another
        canDropBomb = true;
    }
    private void DropBomb()
    {
        GameObject Bomb = Instantiate(BombObject) as GameObject;
        Bomb.transform.position = new Vector3(target.position.x, .3f, target.position.z);
    }

    public void Splatter()
    {
        if (PlayerExplosion != null)
        {
            Explosion = Instantiate(PlayerExplosion) as GameObject;
            Explosion.transform.position = transform.position;
            Destroy(Explosion, 1f);
        }

        SceneManager.DeclareGameLost();

        Destroy(this.gameObject);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name.StartsWith("Enemy"))
            Splatter();
    }
}
