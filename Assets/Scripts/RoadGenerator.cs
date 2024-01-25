using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> roadPieces = new List<GameObject>();
    const float CELL_SIZE = 127.4f;
    const int GRID_SIZE_X = 10;
    const int GRID_SIZE_Z = 10;
    GameObject[,] gridBuffer = new GameObject[GRID_SIZE_X, GRID_SIZE_Z];
    Vector2Int bufferPosition;
    Vector2Int currentGridPosition;

    // Start is called before the first frame update
    void Start()
    {
        IntializeGrid();
    }

    private void IntializeGrid()
    {
        currentGridPosition = new Vector2Int((int)((Camera.main.transform.position.x / CELL_SIZE) - GRID_SIZE_X/2), (int)(Camera.main.transform.position.z / CELL_SIZE));
        if (Camera.main.transform.position.x < 0)
        {
            currentGridPosition.x--;      // -1..0 is seen as zero by /CELL_SIZE, but should be seen as -1, because 0..1 is already seen as 0
        }
        if (Camera.main.transform.position.z < 0)
        {
            currentGridPosition.y--;      // -1..0 is seen as zero by /CELL_SIZE, but should be seen as -1, because 0..1 is already seen as 0
        }
        bufferPosition = new Vector2Int(0, 0);

        for (int x = 0; x < GRID_SIZE_X; x++)
        {
            for (int z = 0; z <GRID_SIZE_Z; z++)
            {
                CreateGridPiece(x, z);
            }
        }
    }

    private void CreateGridPiece(int x, int z)
    {
        GameObject newPiece = Instantiate(roadPieces[Random.Range(0, roadPieces.Count)], 
            new Vector3((currentGridPosition.x + x) * CELL_SIZE, 
                        0, 
                        (currentGridPosition.y + z) * CELL_SIZE), Quaternion.identity);

        int buffer_x = bufferPosition.x + x;
        if (buffer_x >= GRID_SIZE_X)
        {
            buffer_x -= GRID_SIZE_X; 
        }
        int buffer_z = bufferPosition.y + z;
        if (buffer_z >= GRID_SIZE_Z)
        {
            buffer_z -= GRID_SIZE_Z;
        }
        gridBuffer[buffer_x, buffer_z] = newPiece;

        if (Random.value < 0.5)
        {
            newPiece.transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int cameraGrid = new Vector2Int((int)((Camera.main.transform.position.x / CELL_SIZE) - GRID_SIZE_X/2), (int)(Camera.main.transform.position.z / CELL_SIZE));
        if (Camera.main.transform.position.x < 0)
        {
            cameraGrid.x--;      // -1..0 is seen as zero by /CELL_SIZE, but should be seen as -1, because 0..1 is already seen as 0
        }
        if (Camera.main.transform.position.z < 0)
        {
            cameraGrid.y--;      // -1..0 is seen as zero by /CELL_SIZE, but should be seen as -1, because 0..1 is already seen as 0
        }
        while (cameraGrid.y > currentGridPosition.y)
        {
            GenerateTerrainForward();
        }
    }

    private void GenerateTerrainForward()
    {
        for (int x = 0; x < GRID_SIZE_X; x++)
        {
            Destroy(gridBuffer[x, bufferPosition.y]);
            CreateGridPiece(x, GRID_SIZE_Z-1);
        }

        currentGridPosition.y++;
        bufferPosition.y++;
        if (bufferPosition.y >= GRID_SIZE_Z)
        {
            bufferPosition.y = 0;
        }
    }
}
