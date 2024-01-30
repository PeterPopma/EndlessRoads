using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    const float CELL_SIZE = 127.4f;
    const int GRID_SIZE_X = 10;
    const int GRID_SIZE_Z = 10;
    [SerializeField] LandscapeGenerator landscapeGenerator;
    [SerializeField] CarGenerator carGenerator;
    [SerializeField] List<GameObject> roadPieces = new List<GameObject>();
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
        currentGridPosition = new Vector2Int((int)((Camera.main.transform.position.x / CELL_SIZE) - GRID_SIZE_X/2), (int)(Camera.main.transform.position.z / CELL_SIZE - GRID_SIZE_Z / 2));
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

        Destroy(gridBuffer[buffer_x, buffer_z]);
        Vector2 gridPosition = new Vector2((currentGridPosition.x + x) * CELL_SIZE, (currentGridPosition.y + z) * CELL_SIZE);
        GameObject newPiece = Instantiate(roadPieces[Random.Range(0, roadPieces.Count)], 
            new Vector3(gridPosition.x, 0, gridPosition.y), Quaternion.identity);
        landscapeGenerator.AddRandomLandscape(newPiece);
        carGenerator.AddCars(newPiece);
        gridBuffer[buffer_x, buffer_z] = newPiece;

        if (Random.value < 0.5)
        {
            newPiece.transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int cameraGrid = new Vector2Int((int)((Camera.main.transform.position.x / CELL_SIZE) - GRID_SIZE_X/2), (int)(Camera.main.transform.position.z / CELL_SIZE - GRID_SIZE_Z/2));
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
        while (cameraGrid.y < currentGridPosition.y)
        {
            GenerateTerrainBackward();
        }
        while (cameraGrid.x < currentGridPosition.x)
        {
            GenerateTerrainLeft();
        }
        while (cameraGrid.x > currentGridPosition.x)
        {
            GenerateTerrainRight();
        }
    }

    private void GenerateTerrainForward()
    {
        for (int x = 0; x < GRID_SIZE_X; x++)
        {
            CreateGridPiece(x, GRID_SIZE_Z-1);
        }

        currentGridPosition.y++;
        bufferPosition.y++;
        if (bufferPosition.y >= GRID_SIZE_Z)
        {
            bufferPosition.y = 0;
        }
    }

    private void GenerateTerrainBackward()
    {
        for (int x = 0; x < GRID_SIZE_X; x++)
        {
            CreateGridPiece(x, 0);
        }

        currentGridPosition.y--;
        bufferPosition.y--;
        if (bufferPosition.y < 0)
        {
            bufferPosition.y = GRID_SIZE_Z - 1;
        }
    }

    private void GenerateTerrainLeft()
    {
        for (int z = 0; z < GRID_SIZE_Z; z++)
        {
            CreateGridPiece(0, z);
        }

        currentGridPosition.x--;
        bufferPosition.x--;
        if (bufferPosition.x < 0)
        {
            bufferPosition.x = GRID_SIZE_Z - 1;
        }
    }

    private void GenerateTerrainRight()
    {
        for (int z = 0; z < GRID_SIZE_Z; z++)
        {
            CreateGridPiece(GRID_SIZE_X-1, z);
        }

        currentGridPosition.x++;
        bufferPosition.x++;
        if (bufferPosition.x >= GRID_SIZE_X)
        {
            bufferPosition.x = 0;
        }
    }
}
