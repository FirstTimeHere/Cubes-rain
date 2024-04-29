using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    private bool _isColorChanged;

    private void OnCollisionEnter(Collision collision)
    {
        //LifeTimer?.Invoke(RandomTime);
        if (_isColorChanged == false)
        {
            _isColorChanged = true;
            gameObject.GetComponent<MeshRenderer>().material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
    }
}
