using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Populate Grid with Blocks and Enemies
/// </summary>
public class Populator : MonoBehaviour
{
    [SerializeField] Transform Floor;
    private List<Transform> PositionsList;
    private Transform[] PositionsArray;

    [Tooltip("Preplaced positions on the grid that will be referenced by the random spawner")]
    [SerializeField] GameObject Placeholders;
    public int NumberOfEnemies = 5;
    [SerializeField] int NumberOfDestructibleBlocks = 40;
    [SerializeField] int NumberOfIndestructibleBlocks = 30;
    [SerializeField] GameObject IndestructibleBlockObject;
    [SerializeField] GameObject DestructibleBlockObject;
    [SerializeField] GameObject EnemyObject;
    private int allNeededPositions;
    /// <summary>
    /// The current position/index in the random collection of transforms
    /// </summary>
    private int currentPosition = 0;
    // Start is called before the first frame update
    void Start()
    {
        //UnityEngine.Random.InitState(11);
        allNeededPositions = NumberOfEnemies + NumberOfDestructibleBlocks + NumberOfIndestructibleBlocks;
        PositionsList = new List<Transform>();
        PositionsArray = Placeholders.GetComponentsInChildren<Transform>();
        for (int i = 0; i < PositionsArray.Length - 1; ++i)
        {
            PositionsList.Add(PositionsArray[i]);
        }
        
        PositionsList = Shuffle(PositionsList);

        SpawnIndestructibleBlocks();
        SpawnDestructibleBlocks();
        SpawnEnemies();
        
    }
    private void SpawnIndestructibleBlocks()
    {
        for (int i = 0; i < NumberOfIndestructibleBlocks; ++i)
        {
            GameObject indestructible = Instantiate(IndestructibleBlockObject) as GameObject;
            indestructible.transform.position = new Vector3(PositionsList[currentPosition].position.x, .7f, PositionsList[currentPosition].position.z);
            currentPosition += 1;
        }
    }

    private void SpawnDestructibleBlocks()
    {
        for (int i = 0; i < NumberOfDestructibleBlocks; ++i)
        {
            GameObject destructible = Instantiate(DestructibleBlockObject) as GameObject;
            destructible.transform.position = new Vector3(PositionsList[currentPosition].position.x, .7f, PositionsList[currentPosition].position.z);
            currentPosition += 1;
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < NumberOfEnemies; ++i)
        {
            GameObject enemy = Instantiate(EnemyObject) as GameObject;
            enemy.transform.position = new Vector3(PositionsList[currentPosition].position.x, .7f, PositionsList[currentPosition].position.z);
            currentPosition += 1;
        }
    }

    private List<Transform> Shuffle(List<Transform> theList)
    {
        int count = theList.Count;
        int last = count - 1;
        for (int i = 0; i < last; ++i)
        {
            int r = UnityEngine.Random.Range(i, count);
            Transform tmp = theList[i];
            theList[i] = theList[r];
            theList[r] = tmp;
        }
        return theList;
    }
}
