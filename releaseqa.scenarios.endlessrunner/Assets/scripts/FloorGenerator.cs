using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    //Public fields used in the Inspector
    [Header("Floor Generation")]
    [SerializeField]
    [Tooltip("The prefab that should be used as a base for each floor chunk")]
    private GameObject floorChunkPrefab = null;
    [SerializeField]
    [Tooltip("How big is the pool of floor chunks that can be used? A bigger pool will take up more memory but will be more performant")]
    private int floorChunkPoolSize = 0;

    //Member fields and variables
    private GameObject[] floorChunkPool = null; //Storage for the floor chunk objects
    private GameObject lastChunkPlaced = null; //Tracking the last chunk that was placed down, needed when adding new chunks
    private Vector3 floorChunkBounds = Vector3.zero; //Storing the size of the chunk prefab bounds. Needed to get positioning correct

    private void Start()
    {
        //init the chunk pool array
        floorChunkPool = new GameObject[floorChunkPoolSize];

        //get the bounds of the FloorChunkPrefab for later use
        floorChunkBounds = floorChunkPrefab.GetComponent<Renderer>().bounds.size;

        //Create all the floor chunks we will use, add them to the array
        for (int i = 0; i < floorChunkPoolSize; i++)
        {
            GameObject newChunk = Instantiate(floorChunkPrefab, transform);
            newChunk.name = "FloorChunk" + i;
            newChunk.SetActive(false);
            floorChunkPool[i] = newChunk;
        }

        //Set up the chunks directly in front of the player
        InitializeFloorChunks();
    }

    //Lay down the first chunk and then append all the others to that
    private void InitializeFloorChunks()
    {
        //Will only run if there are no chunks placed down
        if (lastChunkPlaced == null)
        {
            GameObject firstChunk = GetFreeFloorChunk();
            firstChunk.transform.position = Vector3.zero; //Currently plonks the first chunk at world origin. Might not be the best.
            firstChunk.transform.position = new Vector3 (0,-10,0);
            firstChunk.SetActive(true);
            lastChunkPlaced = firstChunk;

            for (int i = 1; i < floorChunkPool.Length; i++)
            {
                GameObject newChunk = GetFreeFloorChunk();
                AppendFloorChunk(newChunk);
            }
        }
    }

    //Append a new chunk to the end of the floor. Use the position of the last chunk placed and the size of the chunks to know where the new one goes
    private void AppendFloorChunk(GameObject chunk)
    {
        Vector3 lastChunkPos = lastChunkPlaced.transform.position;
        Vector3 newChunkPosition = new Vector3(lastChunkPos.x, lastChunkPos.y, lastChunkPos.z + floorChunkBounds.z); //Assumes the level moves forward in Z
        GameObject newChunk = GetFreeFloorChunk();
        newChunk.transform.position = newChunkPosition;
        newChunk.SetActive(true);
        lastChunkPlaced = newChunk;
    }

    //Retrieve a free chunk from the pool. If we can't find one, return null
    private GameObject GetFreeFloorChunk()
    {

        for (int i = 0; i < floorChunkPool.Length; i++)
        {
            GameObject currentFloorChunk = floorChunkPool[i];

            if (currentFloorChunk.activeInHierarchy == false)
            {
                return currentFloorChunk;
            }
        }

        return null;
    }

    //When chunks hit a box collider behind the Floor Generator object, disable them and add a new chunk to the end of the level
    private void OnTriggerEnter(Collider other) // currently does no validaiton on the other collider to make sure it's a floor chunk
    {
        if (other.GetComponent<FloorChunk>())
        {
            other.gameObject.SetActive(false);
            GameObject newChunk = GetFreeFloorChunk();
            AppendFloorChunk(newChunk);
        }
    }
}
