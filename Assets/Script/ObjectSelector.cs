using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ObjectSelector : MonoBehaviour
{
    public Camera mainCamera;
    public TextMeshProUGUI selectedObjectNameText;
    public GameObject infopanel,goruntulebutton, bildirbutton;
    public GameObject prefabToInstantiate; 
    private GameObject selectedObject;
    private Color originalColor;
    public ApartmentDataLoader apartmentDataLoader; 

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    infopanel.SetActive(true);

                    if (selectedObject != null)
                    {
                        selectedObject.GetComponent<MeshRenderer>().material.color = originalColor;
                    }

                    selectedObject = hit.collider.gameObject;
                    var meshRenderer = selectedObject.GetComponent<MeshRenderer>();

                    if (meshRenderer != null)
                    {
                        originalColor = meshRenderer.material.color;

                        if (selectedObject.CompareTag("agac"))
                        {
                            meshRenderer.material.color = Color.white;
                            bildirbutton.SetActive(true);
                            goruntulebutton.SetActive(false);
                        }

                        else if (selectedObject.CompareTag("apartment") ||
                                     selectedObject.CompareTag("apartment-noelectric") ||
                                     selectedObject.CompareTag("apartment-borc") ||
                                     selectedObject.CompareTag("apartment-internet"))
                        {
                            meshRenderer.material.color = Color.green;
                            bildirbutton.SetActive(false);
                            goruntulebutton.SetActive(true);
                            StartCoroutine(apartmentDataLoader.GetApartmentData(selectedObject.name));
                        }

                        else
                        {
                            meshRenderer.material.color = Color.green;
                            bildirbutton.SetActive(true);
                            goruntulebutton.SetActive(false);
                        }

                        selectedObjectNameText.text = selectedObject.name;
                    }
                }
                else
                {
                    infopanel.SetActive(false);
                    if (selectedObject != null)
                    {
                        selectedObject.GetComponent<MeshRenderer>().material.color = originalColor;
                        selectedObject = null;
                    }
                }
            }
        }
    }

    public void InstantiatePrefabAtSelectedObject()
    {
        if (prefabToInstantiate != null && selectedObject != null)
        {
            Vector3 position = selectedObject.transform.position;
            if (selectedObject.name == "Çeþme" || selectedObject.name == "Basketbol Potasý" || selectedObject.name == "Güvenlik" || selectedObject.name == "Aðaç")
            {
                position.y += 3.7f; 
            }
            else
            {
                Debug.Log(selectedObject.name);
                position.y += 1.8f; 
            }
            Quaternion rotation = selectedObject.transform.rotation;
            Instantiate(prefabToInstantiate, position, rotation);
        }
    }
}
