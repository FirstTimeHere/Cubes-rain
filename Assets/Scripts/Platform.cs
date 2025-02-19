using UnityEngine;

public class Platform : MonoBehaviour
{
    public void GetNewColorCube(Cube cube)
    {
        if (cube.TryGetComponent(out MeshRenderer mesh))
        {
            mesh.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
    }
}
