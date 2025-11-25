using UnityEngine;

public class ARDeleteCubes : MonoBehaviour
{
    public void DeleteAllCubes()
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("ARCube");
        foreach (GameObject cube in cubes)
        {
            Destroy(cube);
        }
    }
}
