using System;
using UnityEngine;

public class PlaneColission : MonoBehaviour
{
    public event Action OnCollisionEnterEvent;

    private void OnCollisionEnter(Collision collision)
    {
        OnCollisionEnterEvent?.Invoke();
    }
}
