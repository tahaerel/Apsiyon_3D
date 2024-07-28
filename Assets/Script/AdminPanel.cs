using UnityEngine;
using UnityEngine.UI;

public class AdminPanel : MonoBehaviour
{
    public GameObject cubePrefab;  
    public GameObject panel;       
    private GameObject currentCube;
    private bool isDragging = false;

    void Start()
    {
        panel.SetActive(false);
    }

    public void OnButtonClick()
    {
        if (currentCube == null)
        {
            currentCube = Instantiate(cubePrefab, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Quaternion.identity);
            isDragging = true;
        }
    }

    void Update()
    {
        if (isDragging && currentCube != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10; // Kameranýn önünde 10 birim uzaklýkta
            currentCube.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                panel.SetActive(true);
                Destroy(currentCube);
                isDragging = false;
            }
        }
    }
}
