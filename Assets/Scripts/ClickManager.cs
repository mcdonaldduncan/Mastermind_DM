using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ClickManager : MonoBehaviour
{
    private Transform[] gameTiles;
    private Transform[] colorTiles;
    private Material[] colorTilesMaterials;
    private Material currentMaterial = null;

    // Start is called before the first frame update
    void Start()
    {
        gameTiles = 
        GameObject.FindGameObjectsWithTag("GameTile").
        Select(go => go.GetComponent<Transform>()).ToArray();
        gameTiles.ToList().ForEach(gt => Debug.Log($"Game Tile {gt.name}"));

        colorTiles =
            GameObject.FindGameObjectsWithTag("ColorTile").
        Select(go => go.GetComponent<Transform>()).ToArray();
        gameTiles.ToList().ForEach(gt => Debug.Log($"Game Tile {gt.name}"));

        colorTilesMaterials = 
            colorTiles.ToList().
            Select(t => t.GetComponent<MeshRenderer>().material).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mp = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mp);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (gameTiles.Contains(hit.transform))
                {
                    Debug.Log($"Clicked on Game Tile {hit.transform.name} at {hit.transform.position}");
                    if (currentMaterial != null)
                        hit.transform.GetComponent<MeshRenderer>().material = currentMaterial;
                }
                else if (colorTiles.Contains(hit.transform))
                {
                    Debug.Log($"Clicked on Color Tile {hit.transform.name} at {hit.transform.position}");
                    int pos = Array.IndexOf(colorTiles, hit.transform);
                    currentMaterial = colorTilesMaterials[pos];    
                }
            }
            else
                Debug.Log($"Clicked at {mp} without hitting anything");
        }
    }
}
