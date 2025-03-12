using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void GetNewColorCube(Cube cube)
    {
        if (cube.Mesh.material.color == Color.white)
        {
            cube.Mesh.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
    }

    public void GetDefaultColor(Cube cube)
    {
        cube.Mesh.material.color = Color.white;
    }

    public void GetDefaultAlpha(Bomb bomb)
    {
        Color color = bomb.Mesh.material.color;
        color.a = 1f;
        bomb.Mesh.material.color = color;
        bomb.Released -= GetDefaultAlpha;
    }
}
