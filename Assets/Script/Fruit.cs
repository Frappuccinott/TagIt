using UnityEngine;

public class Fruit : MonoBehaviour
{
    public delegate void FruitCollected();
    public event FruitCollected OnFruitCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnFruitCollected?.Invoke();  // Meyve alýndýðýnda event tetiklenir
            Destroy(gameObject);  // Meyveyi yok et (toplandýðýnda)
        }
    }
}