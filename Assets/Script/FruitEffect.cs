using UnityEngine;

public class FruitEffect : MonoBehaviour, Ifruitboostable
{
    public enum FruitType { Apple, Banana }  // Meyve türü (Elma ya da Muz)
    public FruitType fruitType;  // Meyve türü seçimi

    //public float speedBoostDuration = 3f;  // Hýz artýþý süresi (5 saniye)

    private void OnTriggerEnter(Collider other)
    {
        // Eðer çarpan nesne bir oyuncu (Player) ise
        if (other.CompareTag("Player"))
        {
            // IFruitBoostable arayüzünü uygulayan sýnýf varsa, hýz artýþýný uygula
            IFruitBoostable boostable = other.GetComponent<IFruitBoostable>();
            if (boostable != null)
            {
                boostable.ApplySpeedBoost(speedBoostDuration);  // Hýz artýþýný uygula
                Destroy(gameObject);  // Meyveyi yok et (toplandý)
            }
        }
    }
}
