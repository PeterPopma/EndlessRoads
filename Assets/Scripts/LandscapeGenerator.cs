using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> objects = new List<GameObject>();
    [SerializeField] float spawnLikeliness = 0.5f;
    const float OBJECT_SPACE = 10;
    const float OBJECT_MARGIN = 2;

    public void AddRandomLandscape(GameObject gridSquare)
    {
        foreach (Vector4 landArea in gridSquare.GetComponent<RoadPiece>().landAreas)
        {
            for (float z = landArea.y; z < landArea.w - OBJECT_SPACE; z += OBJECT_SPACE)
            {
                for (float x = landArea.x; x < landArea.z - OBJECT_SPACE; x += OBJECT_SPACE)
                {
                    AddRandomObject(gridSquare, x, z);
                }
            }
        }
        
    }

    private void AddRandomObject(GameObject gridSquare, float x, float z)
    {
        if (Random.value < spawnLikeliness)
        {
            x += Random.value * (OBJECT_SPACE - OBJECT_MARGIN);
            z += Random.value * (OBJECT_SPACE - OBJECT_MARGIN);
            GameObject newObject = Instantiate(objects[Random.Range(0, objects.Count)]);
            newObject.transform.parent = gridSquare.transform;
            newObject.transform.transform.position = new Vector3(gridSquare.transform.position.x + x, gridSquare.transform.position.y, gridSquare.transform.position.z + z);
        }
    }
}
