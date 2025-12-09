using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [Header("Fruit Prefabs")]
    public GameObject[] fruits;  // Elma ve Muz prefabs'ý

    [Header("Spawn Area")]
    public Collider spawnAreaCollider;  // Spawn alaný collider'ý

    [Header("Spawn Settings")]
    public float spawnInterval = 2f;  // Meyve spawn aralýðý
    public int maxFruits = 10;  // Toplam meyve sayýsý
    private int currentFruits = 0;  // Mevcut meyve sayýsý

    void Start()
    {
        InvokeRepeating(nameof(SpawnFruit), 0f, spawnInterval);  // Baþlangýçta sürekli spawn etme
    }

    void SpawnFruit()
    {
        // Eðer meyve sayýsý 10'a ulaþtýysa yeni meyve spawn etmeyelim
        if (currentFruits >= maxFruits) return;

        if (fruits == null || fruits.Length == 0)
        {
            Debug.LogWarning("Fruit prefab listesi boþ.");
            return;
        }

        // Spawn alanýndan rastgele bir nokta seç
        Vector3 center = spawnAreaCollider.bounds.center;
        Vector3 size = spawnAreaCollider.bounds.size;

        float randomX = Random.Range(-size.x / 2f, size.x / 2f);
        float randomZ = Random.Range(-size.z / 2f, size.z / 2f);
        Vector3 spawnFrom = new Vector3(center.x + randomX, center.y + 5f, center.z + randomZ);

        // Yere yerleþmesini saðlamak için Raycast kullanýyoruz
        if (Physics.Raycast(spawnFrom, Vector3.down, out RaycastHit hit, 10f))
        {
            GameObject fruit = fruits[Random.Range(0, fruits.Length)];
            GameObject spawnedFruit = Instantiate(fruit, hit.point + Vector3.up * 0.1f, Quaternion.identity);

            currentFruits++;  // Yeni meyve spawn edildikçe sayýyý artýr

            // Meyve alýndýðýnda yeni meyve spawn et
            Fruit fruitScript = spawnedFruit.GetComponent<Fruit>();
            if (fruitScript != null)
            {
                fruitScript.OnFruitCollected += HandleFruitCollected;  // Meyve alýndýðýnda sayýyý azaltacak event
            }
        }
    }

    // Meyve alýndýðýnda meyve sayýsýný güncelle
    void HandleFruitCollected()
    {
        currentFruits--;  // Meyve alýndý, sayýyý azalt
        SpawnFruit();  // Yeni bir meyve spawn et
    }

    // Gizmos ile spawn alanýný çiz
    private void OnDrawGizmos()
    {
        if (spawnAreaCollider != null)
        {
            Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
            Gizmos.DrawCube(spawnAreaCollider.bounds.center, spawnAreaCollider.bounds.size);
        }
    }
}
