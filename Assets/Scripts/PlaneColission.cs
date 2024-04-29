using UnityEngine;

public class PlaneColission : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject, GetRandomTime());
    }

    private int GetRandomTime()
    {
        int minRandom = 2;
        int maxRandom = 6;
        int randomTime = UnityEngine.Random.Range(minRandom, maxRandom);

        return randomTime;
    }
}
