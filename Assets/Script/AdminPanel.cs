using UnityEngine;
using UnityEngine.UI;

public class AdminPanel : MonoBehaviour
{
    public GameObject cubePrefab;  // Sahnede olu�turulacak k�p prefab�
    public GameObject panel;       // UI paneli
    private GameObject currentCube;
    private bool isDragging = false;

    void Start()
    {
        // Paneli ba�lang��ta kapal� yap
        panel.SetActive(false);
    }

    public void OnButtonClick()
    {
        if (currentCube == null)
        {
            // Butona t�kland���nda bir k�p olu�tur ve fareyi takip etmesini sa�la
            currentCube = Instantiate(cubePrefab, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Quaternion.identity);
            isDragging = true;
        }
    }

    void Update()
    {
        if (isDragging && currentCube != null)
        {
            // K�p� fare pozisyonuna g�ncelle
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10; // Kameran�n �n�nde 10 birim uzakl�kta
            currentCube.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                // Sol t�klama yap�ld���nda paneli a� ve s�r�klemeyi durdur
                panel.SetActive(true);
              //  Destroy(cubePrefab);
                Destroy(currentCube);
                isDragging = false;
            }
        }
    }
}
