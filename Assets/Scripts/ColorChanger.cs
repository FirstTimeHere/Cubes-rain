using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void GetNewColorCube(Cube cube)
    {
        if (cube.TryGetComponent(out MeshRenderer mesh))
        {
            if (mesh.material.color == Color.white)
            {
                mesh.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            }
        }
    }

    public void GetDefaultColor(Cube cube)
    {
        if (cube.TryGetComponent(out MeshRenderer mesh))
        {
            mesh.material.color = Color.white;
        }
    }
}
