
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

    RectTransform section1RT;
    RectTransform section2RT;

    private void Start()
    {
        gc = GameController.Instance;
        section1RT = GetComponent<RectTransform>();
        section2RT = GetComponent<RectTransform>();
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
            CreateGroundPrefabs(section1.transform, grassPrefabs, 4);
            CreateGroundPrefabs(section1.transform, dirtPrefabs, 1);
            CreateGroundPrefabs(section1.transform, rocksPrefabs, 1);
        }
        // Check if the camera has moved past the second section
        else if (cameraX + (cameraWidth / 2) > section2.position.x + GetSectionWidth())
        {
            // Move section2 to the right of section1
            section2.position = new Vector3(section1.position.x + GetSectionWidth(), section2.position.y, section2.position.z);
            RemovePrefabsFromSection(section2.transform);
            CreateWindPrefabs(section2.transform);
            CreateGroundPrefabs(section2.transform, grassPrefabs, 4);
            CreateGroundPrefabs(section1.transform, dirtPrefabs, 1);
            CreateGroundPrefabs(section1.transform, rocksPrefabs, 1);
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
        RectTransform parentRect = parentTransform.GetComponent<RectTransform>();
        int randAmount = Random.Range(gc.GetWindSpawnTimeMin(), gc.GetWindSpawnTimeMax());

        
        for (int i = 0;  i < randAmount; i++) 
        {

            int randIndex = Random.Range(0, windPrefabs.Count);
            GameObject newWind = Instantiate(windPrefabs[randIndex], parentTransform);

            float positionIntervalX = parentRect.rect.xMin + (gc.GetWindSpawnSpacingX() * i);
            float positionIntervalY = parentRect.rect.yMin + (gc.GetWindSpawnSpacingY() * Random.Range(1, i));

            
            newWind.transform.localPosition = new Vector2(positionIntervalX, positionIntervalY);
        }
        
    }

    private void CreateGroundPrefabs(Transform parentTransform, List<GameObject> prefabGroup, float amountOfOjbects)
    {
        RectTransform parentRect = parentTransform.GetComponent<RectTransform>();  
        for (int i = 0; i < amountOfOjbects; i++)
        {

            int randIndex = Random.Range(0, prefabGroup.Count-1);
            GameObject newGroundObject = Instantiate(prefabGroup[randIndex], parentTransform);
            float positionIntervalX = (parentRect.rect.xMin + (prefabGroup[0].GetComponent<SpriteRenderer>().size.x * i));
            float positionIntervalY = 0;
            
            newGroundObject.transform.localPosition = new Vector2(positionIntervalX, positionIntervalY);
        }

    }

}

