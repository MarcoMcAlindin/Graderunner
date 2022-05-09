using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Editor script that automatically places shadows for every tile on the tilemap this script is assigned to

public class MakeWalls3D : MonoBehaviour {
    // The prefab to use as the shadow box
    public GameObject shadowBox;

    // The prefab to use for the container
    public GameObject shadowBoxContainer;

    // Seconds elapsed since last update
    public float secondsElapsed = 0;

    public void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart() {
        yield return new WaitForSeconds(1f);
        foreach (GameObject wallMap in GameObject.FindGameObjectsWithTag("Walls")) {
            Tilemap tilemap = wallMap.GetComponent<Tilemap>();

            Vector3Int gridSize = tilemap.size;

            // Go through every existent tile on the map and place a shadowcube
            for (int x = tilemap.origin.x; x < gridSize.x; x++)
            {
                for (int y = tilemap.origin.y; y < gridSize.y; y++)
                {
                    TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                    if (tile) PlaceShadowBox(tilemap.CellToWorld(new Vector3Int(x, y, 0)));
                }
            }
        }
	}

    void PlaceShadowBox(Vector3 location)
    {
        // Instansiate a shadowcube at the tile location with an offset to center it
        location += new Vector3(0.65f, 0.65f, -0.5f);
        GameObject newShadowBox = Instantiate(shadowBox, shadowBoxContainer.transform);
        newShadowBox.transform.position = location;
    }
}
