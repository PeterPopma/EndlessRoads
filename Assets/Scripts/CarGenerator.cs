using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> cars = new List<GameObject>();
    [SerializeField] float spawnLikeliness = 0.5f;

    public void AddCars(GameObject gridSquare)
    {
        if (gridSquare.GetComponent<RoadPiece>().hasVerticalCars)
        {
            for (int i = 0; i < 4; i++)
            {
                for (float z = -60; z < 60; z += 15)
                {
                    AddRandomCar(gridSquare, -3.78f + 2.43877f * i, 0.42f, z + i * 4);
                }
            }
        }
        if (gridSquare.GetComponent<RoadPiece>().hasHorizontalCars)
        {
            for (int i = 0; i < 4; i++)
            {
                for (float x = -60; x < 60; x += 15)
                {
                    AddRandomCar(gridSquare, x + i * 4, 8.026f, -3.78f + 2.43877f * i, true);
                }
            }
        }
    }

    private void AddRandomCar(GameObject gridSquare, float x, float y, float z, bool rotate=false)
    {
        if (Random.value < spawnLikeliness)
        {
            GameObject newObject = Instantiate(cars[Random.Range(0, cars.Count)]);
            newObject.transform.parent = gridSquare.transform;
            newObject.transform.transform.position = new Vector3(gridSquare.transform.position.x + x, gridSquare.transform.position.y + y, gridSquare.transform.position.z + z);
            if (rotate)
            {
                newObject.transform.Rotate(0, 90, 0);
            }
        }
    }
}
