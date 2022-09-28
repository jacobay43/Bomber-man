using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] GameObject EnemyExplosion;
    [SerializeField] Transform PatrolRoute;
    [SerializeField] Transform Player;
    private Populator populator;
    private bool ShouldRunAway;
    public UIController uiController;
    public List<Transform> Locations;
    private GameObject Explosion;
    private int LocationIndex;
    private NavMeshAgent Agent;

    public float Speed = 3.0f;
    public float ObstacleRange = .35f;
    private Rigidbody rb;
    private static readonly int[] AvailableAngles = new[] { 0, 90, 180, 270 };
    // Start is called before the first frame update
    void Start()
    {
        ShouldRunAway = false;
        populator = GameObject.Find("Grid Manager").GetComponent<Populator>();
        uiController = GameObject.Find("UI Controller").GetComponent<UIController>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    private void InitializePatrolRoute()
    {
        foreach(Transform child in PatrolRoute)
        {
            Locations.Add(child);
        }
    }
    private void MoveToNextPatrolLocation()
    {
        if (Locations.Count == 0)
            return;
        Agent.destination = Locations[LocationIndex].position;
        //choose next location randomly
        LocationIndex = (int)UnityEngine.Random.Range(0f,(float)Locations.Count);
        if (LocationIndex >= Locations.Count)
            LocationIndex = 0;
    }

    

    // Update is called once per frame
    void Update()
    {
        if (ShouldRunAway)
        {
            MoveAwayFromPlayer();
            return;
        }
        if (populator.NumberOfEnemies <= 2)
        { //Trigger Runaway Enemies
            ShouldRunAway = true;
        }
        if (Agent.remainingDistance < 0.2f && !Agent.pathPending)
        {
          MoveToNextPatrolLocation();
        }
        if ((int)UnityEngine.Random.Range(1, 20) == 15)
            MoveToNextPatrolLocation();
    }

    public void Splatter()
    {
        if (populator.NumberOfEnemies > 0)
            populator.NumberOfEnemies -= 1;
        

        uiController.EnemyCounterLabel.text = $"Enemies Left: {populator.NumberOfEnemies}";

        Debug.Log("Splatter called");
        if (EnemyExplosion != null)
        {
            Explosion = Instantiate(EnemyExplosion) as GameObject;
            Explosion.transform.position = transform.position;
            Destroy(Explosion, 1f);
        }
        Destroy(this.gameObject);
    }

    private void MoveAwayFromPlayer()
    {
        Agent.destination = transform.position + (transform.position - Player.transform.position);
    }
}
