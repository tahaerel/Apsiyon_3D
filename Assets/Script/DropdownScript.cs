using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class DropdownScript : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public GameObject elektrikkabloObject, sukabloObject, internetkabloObject;
    public Material elektrikMaterial, orangeMaterial, whiteMaterial, blackMaterial, darkBlueMaterial, lightBlueMaterial, greenMaterial, yellowMaterial;
    public GameObject[] filtreler;

    private Dictionary<string, Material[]> materialMappings;
    private Dictionary<string, GameObject> kabloMappings;

    void Start()
    {
        InitializeDropdown();
        InitializeMappings();
    }

    private void InitializeDropdown()
    {
        dropdown.options.Clear();
        List<string> options = new List<string> { "Filtre Yok", "Elektrik", "Su", "İnternet", "Borç Durumu", "Doluluk Durumu" };
        foreach (string option in options)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(option));
        }

        dropdown.value = 0;
        dropdown.captionText.text = "Filtrele";
        dropdown.onValueChanged.AddListener(delegate { OnDropdownChanged(); });
    }

    private void InitializeMappings()
    {
        kabloMappings = new Dictionary<string, GameObject>
        {
            {"Elektrik", elektrikkabloObject},
            {"Su", sukabloObject},
            {"İnternet", internetkabloObject}
        };

        materialMappings = new Dictionary<string, Material[]>
        {
            {"Elektrik", new Material[]{orangeMaterial, blackMaterial}},
            {"Su", new Material[]{lightBlueMaterial, lightBlueMaterial}},
            {"İnternet", new Material[]{darkBlueMaterial, blackMaterial}},
            {"Borç Durumu", new Material[]{greenMaterial, blackMaterial}},
            {"Doluluk Durumu", new Material[]{yellowMaterial, yellowMaterial}}
        };
    }

    public void OnDropdownChanged()
    {
        string selectedOption = dropdown.options[dropdown.value].text;
        dropdown.captionText.text = selectedOption != "Filtre Yok" ? selectedOption : "Filtrele";

        ResetApartmentMaterials();
        HandleKabloVisibility(selectedOption);
        ApplyMaterials(selectedOption);
        OpenFilter(selectedOption);
    }

    private void HandleKabloVisibility(string filterType)
    {
        foreach (var kablo in kabloMappings.Values)
        {
            kablo.SetActive(false);
        }

        if (kabloMappings.ContainsKey(filterType))
        {
            kabloMappings[filterType].SetActive(true);
        }
    }

    private void ApplyMaterials(string filterType)
    {
        if (materialMappings.ContainsKey(filterType))
        {
            Material[] materials = materialMappings[filterType];
            ApplyMaterialToTaggedObjects("apartment", materials[0]);
            ApplyMaterialToTaggedObjects("apartment-noelectric", materials[1]);

            if (filterType == "Elektrik")
            {
                ApplyMaterialToTaggedObjects("apartment-borc", orangeMaterial);
            }
            else if (filterType == "Borç Durumu")
            {
                ApplyMaterialToTaggedObjects("apartment-borc", blackMaterial);
                ApplyMaterialToTaggedObjects("apartment-noelectric", materials[0]);

            }
            else
            {
                ApplyMaterialToTaggedObjects("apartment-borc", materials[0]);
                ApplyMaterialToTaggedObjects("apartment-noelectric", materials[0]);

            }

            if (filterType == "İnternet")
            {
                ApplyMaterialToTaggedObjects("apartment-internet", blackMaterial);
                ApplyMaterialToTaggedObjects("apartment-noelectric", materials[0]);

            }
            else
            {
                ApplyMaterialToTaggedObjects("apartment-internet", materials[0]);
            }
        }
        else
        {
            ApplyMaterialToTaggedObjects("apartment", whiteMaterial);
            ApplyMaterialToTaggedObjects("apartment-noelectric", whiteMaterial);
            ApplyMaterialToTaggedObjects("apartment-borc", whiteMaterial);
            ApplyMaterialToTaggedObjects("apartment-internet", whiteMaterial);
        }
    }

    private void OpenFilter(string filterType)
    {
        int index = dropdown.options.FindIndex(option => option.text == filterType);
        for (int j = 0; j < filtreler.Length; j++)
        {
            filtreler[j].SetActive(j == index - 1);
        }
    }

    private void ApplyMaterial(GameObject obj, Material newMaterial)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = newMaterial;
        }
    }

    private void ApplyMaterialToTaggedObjects(string tag, Material newMaterial)
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in taggedObjects)
        {
            ApplyMaterial(obj, newMaterial);
        }
    }

    private void ResetApartmentMaterials()
    {
        ApplyMaterialToTaggedObjects("apartment", whiteMaterial);
        ApplyMaterialToTaggedObjects("apartment-noelectric", whiteMaterial);
        ApplyMaterialToTaggedObjects("apartment-borc", whiteMaterial);
    }
}
