//using UnityEngine;

//public class CameraFollow : MonoBehaviour
//{
//    [Header("Target Settings")]
//    public Transform target; // Karakterin transform bileþeni
//    public float cameraHeight = 1.5f; // Kameranýn karaktere göre yüksekliði

//    [Header("Distance Settings")]
//    public float normalDistance = 5f; // Normal kamera mesafesi
//    public float minDistance = 1f; // Minimum yakýnlaþma mesafesi
//    public float distanceSmoothTime = 0.2f; // Mesafe deðiþim yumuþatma süresi

//    [Header("Collision Settings")]
//    public float cameraRadius = 0.3f; // Kamera çarpýþma kontrolü için yarýçap
//    public LayerMask obstacleMask; // Engel olarak algýlanacak katmanlar

//    private Vector3 cameraDirection; // Kameranýn hedefe göre yönü
//    private float currentDistance; // Þu anki kamera mesafesi
//    private float distanceVelocity; // SmoothDamp için hýz referansý

//    void Start()
//    {
//        // Baþlangýçta kamerayý hedefin arkasýna yerleþtir
//        cameraDirection = -transform.forward.normalized;
//        currentDistance = normalDistance;
//    }

//    void LateUpdate()
//    {
//        if (target == null)
//        {
//            Debug.LogWarning("Kamera hedefi atanmamýþ!");
//            return;
//        }

//        // Kameranýn olmasý gereken pozisyonu hesapla (karakterin baþýnýn üstünde)
//        Vector3 targetPosition = target.position + Vector3.up * cameraHeight;

//        // SphereCast ile engel kontrolü
//        RaycastHit hit;
//        float desiredDistance = normalDistance;
//        if (Physics.SphereCast(
//            targetPosition,
//            cameraRadius,
//            cameraDirection,
//            out hit,
//            normalDistance,
//            obstacleMask,
//            QueryTriggerInteraction.Ignore))
//        {
//            desiredDistance = Mathf.Clamp(hit.distance - 0.2f, minDistance, normalDistance);
//        }

//        // Yumuþak geçiþ için SmoothDamp kullan
//        currentDistance = Mathf.SmoothDamp(
//            currentDistance,
//            desiredDistance,
//            ref distanceVelocity,
//            distanceSmoothTime);

//        // Kameranýn pozisyonunu güncelle
//        transform.position = targetPosition + cameraDirection * currentDistance;

//        // Kameranýn karaktere bakmasýný saðla
//        transform.LookAt(targetPosition);
//    }

//    // Debug için SphereCast görselleþtirme (Sadece Editor'de çalýþýr)
//    private void OnDrawGizmosSelected()
//    {
//        if (target != null)
//        {
//            Gizmos.color = Color.red;
//            Vector3 targetPos = target.position + Vector3.up * cameraHeight;
//            Gizmos.DrawWireSphere(targetPos + cameraDirection * currentDistance, cameraRadius);
//            Gizmos.DrawLine(targetPos, targetPos + cameraDirection * normalDistance);
//        }
//    }
//}

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // Karakterin transform bileþeni
    public float cameraHeight = 1.5f; // Kameranýn karaktere göre yüksekliði

    [Header("Distance Settings")]
    public float normalDistance = 5f; // Normal kamera mesafesi
    public float minDistance = 1f; // Minimum yakýnlaþma mesafesi
    public float distanceSmoothTime = 0.2f; // Mesafe deðiþim yumuþatma süresi

    [Header("Rotation Settings")]
    public float rotationSmoothTime = 0.1f; // Dönüþ yumuþatma süresi
    public float mouseSensitivity = 3f; // Fare hassasiyeti (opsiyonel)

    [Header("Collision Settings")]
    public float cameraRadius = 0.3f; // Kamera çarpýþma kontrolü için yarýçap
    public LayerMask obstacleMask; // Engel olarak algýlanacak katmanlar

    private Vector3 cameraDirection; // Kameranýn hedefe göre yönü
    private float currentDistance; // Þu anki kamera mesafesi
    private float distanceVelocity; // SmoothDamp için hýz referansý
    private float rotationVelocity; // Dönüþ için SmoothDamp hýz referansý

    void Start()
    {
        // Baþlangýçta kamerayý hedefin arkasýna yerleþtir
        cameraDirection = -transform.forward.normalized;
        currentDistance = normalDistance;
    }

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Kamera hedefi atanmamýþ!");
            return;
        }

        // Kameranýn olmasý gereken pozisyonu hesapla (karakterin baþýnýn üstünde)
        Vector3 targetPosition = target.position + Vector3.up * cameraHeight;

        // Mouse ile kamera döndürme (opsiyonel)
        //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        //cameraDirection = Quaternion.Euler(0, mouseX, 0) * cameraDirection;

        // Karakterin yönüne göre kamerayý yumuþakça döndür
        Vector3 targetForward = target.forward;
        targetForward.y = 0; // Yükseklik etkisini kaldýr
        targetForward.Normalize();

        float targetAngle = Mathf.Atan2(targetForward.x, targetForward.z) * Mathf.Rad2Deg;
        float currentAngle = transform.eulerAngles.y;
        float smoothedAngle = Mathf.SmoothDampAngle(
            currentAngle,
            targetAngle,
            ref rotationVelocity,
            rotationSmoothTime
        );

        // Kamera yönünü güncelle (karakterin arkasýnda kalacak þekilde)
        cameraDirection = Quaternion.Euler(0, smoothedAngle, 0) * Vector3.back;

        // SphereCast ile engel kontrolü
        RaycastHit hit;
        float desiredDistance = normalDistance;
        if (Physics.SphereCast(
            targetPosition,
            cameraRadius,
            cameraDirection,
            out hit,
            normalDistance,
            obstacleMask,
            QueryTriggerInteraction.Ignore))
        {
            desiredDistance = Mathf.Clamp(hit.distance - 0.2f, minDistance, normalDistance);
        }

        // Yumuþak geçiþ için SmoothDamp kullan
        currentDistance = Mathf.SmoothDamp(
            currentDistance,
            desiredDistance,
            ref distanceVelocity,
            distanceSmoothTime
        );

        // Kameranýn pozisyonunu güncelle
        transform.position = targetPosition + cameraDirection * currentDistance;

        // Kameranýn karaktere bakmasýný saðla
        transform.LookAt(targetPosition);
    }

    // Debug için SphereCast görselleþtirme (Sadece Editor'de çalýþýr)
    private void OnDrawGizmosSelected()
    {
        if (target != null)
        {
            Gizmos.color = Color.red;
            Vector3 targetPos = target.position + Vector3.up * cameraHeight;
            Gizmos.DrawWireSphere(targetPos + cameraDirection * currentDistance, cameraRadius);
            Gizmos.DrawLine(targetPos, targetPos + cameraDirection * normalDistance);
        }
    }
}