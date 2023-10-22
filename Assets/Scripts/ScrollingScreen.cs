
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScrollingScreen : MonoBehaviour
{
    //References
    GameController gc;
    [SerializeField] Transform section1;
    [SerializeField] Transform section2;

    [SerializeField] List<GameObject> grassPrefabs = new List<GameObject>();
    [SerializeField] List<GameObject> windPrefabs = new List<GameObject>();

    [SerializeField] GameObject sludgeBeginning;
    [SerializeField] List<GameObject> sludgePrefabs = new List<GameObject>();

    [SerializeField] List<GameObject> waterPrefabs = new List<GameObject>();
    [SerializeField] List<GameObject> dirtPrefabs = new List<GameObject>();
    [SerializeField] List<GameObject> rocksPrefabs = new List<GameObject>();

    private void Start()
    {
        gc = GameController.Instance;
    }
    void Update()
    {
        // Get the camera's orthographic size (half of the camera's height)
        float cameraOrthoSize = Camera.main.orthographicSize;

        // Get the camera's aspect ratio to calculate the width
        float cameraAspect = Camera.main.aspect;
        float cameraWidth = cameraOrthoSize * cameraAspect;

        // Get the camera's position
        float cameraX = Camera.main.transform.position.x;   

        // Check if the camera has moved past the first section
        if (cameraX + (cameraWidth/2) > section1.position.x + GetSectionWidth())
        {
            // Move section1 to the right of section2
            section1.position = new Vector3(section2.position.x + GetSectionWidth(), section1.position.y, section1.position.z);
            RemovePrefabsFromSection(section1.transform);
            CreateWindPrefabs(section1.transform);
        }
        // Check if the camera has moved past the second section
        else if (cameraX + (cameraWidth / 2) > section2.position.x + GetSectionWidth())
        {
            // Move section2 to the right of section1
            section2.position = new Vector3(section1.position.x + GetSectionWidth(), section2.position.y, section2.position.z);
            RemovePrefabsFromSection(section2.transform);
            CreateWindPrefabs(section2.transform);
        }
    }

    float GetSectionWidth()
    {
        // Calculate the width of the background section
        return Mathf.Abs(section1.position.x - section2.position.x);
    }

    private void RemovePrefabsFromSection(Transform parentTransform)
    {
        foreach(Transform gameObject in parentTransform)
        {
            if (gameObject.tag != "Ground")
            {
                Destroy(gameObject.gameObject);
            }
            
        }
    }

    private void CreateWindPrefabs(Transform parentTransform)
    {
        
        int randAmount = Random.Range(gc.GetWindSpawnTimeMin(), gc.GetWindSpawnTimeMax());
        float positionIntervalX = section1.position.x + GetSectionWidth() + (gc.GetWindSpawnSpacing() * randAmount);
        float positionIntervalY = section1.position.y + (gc.GetWindSpawnSpacing() * randAmount);

        for(int i = 0;  i < randAmount; i++) 
        {
            int randIndex = Random.Range(0, windPrefabs.Count);
            GameObject newWind = Instantiate(windPrefabs[randIndex], parentTransform);
            newWind.transform.position = new Vector2(positionIntervalX, positionIntervalY);
        }
        
    }

}

