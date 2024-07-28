using UnityEngine;
using UnityEngine.UI;

public class AdminPanel : MonoBehaviour
{
    public GameObject cubePrefab;  // Sahnede oluþturulacak küp prefabý
    public GameObject panel;       // UI paneli
    private GameObject currentCube;
    private bool isDragging = false;

    void Start()
    {
        // Paneli baþlangýçta kapalý yap
        panel.SetActive(false);
    }

    public void OnButtonClick()
    {
        if (currentCube == null)
        {
            // Butona týklandýðýnda bir küp oluþtur ve fareyi takip etmesini saðla
            currentCube = Instantiate(cubePrefab, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Quaternion.identity);
            isDragging = true;
        }
    }

    void Update()
    {
        if (isDragging && currentCube != null)
        {
            // Küpü fare pozisyonuna güncelle
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10; // Kameranýn önünde 10 birim uzaklýkta
            currentCube.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                // Sol týklama yapýldýðýnda paneli aç ve sürüklemeyi durdur
                panel.SetActive(true);
              //  Destroy(cubePrefab);
                Destroy(currentCube);
                isDragging = false;
            }
        }
    }
}
