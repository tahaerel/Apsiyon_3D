using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

[Serializable]
public class ApartmentList
{
    public List<Apartment> apartments;
}

[Serializable]
public class Apartment
{
    public string Blok;
    public string Daire;
    public string Doluluk;
    public string DaireSakinleri;
    public string Bor�Bilgisi;
    public string Aidat;
    public Bills Faturalar;
}

[Serializable]
public class Bills
{
    public string Elektrik;
    public string Su;
    public string Do�algaz;
    public string �nternet;
}

public class ApartmentDataLoader : MonoBehaviour
{
    public string jsonUrl = "https://pushup.games/Apartment_Datas.json";
    public TextMeshProUGUI blok, daire, doluluk, dairesakinleri, bor�bilgi, aidat, elektrik, su, do�algaz, internet;

    public IEnumerator GetApartmentData(string apartmentName)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(jsonUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string jsonData = webRequest.downloadHandler.text;
                ApartmentList apartmentList = JsonUtility.FromJson<ApartmentList>("{\"apartments\":" + jsonData + "}");
                Apartment foundApartment = apartmentList.apartments.Find(apartment => apartment.Daire == apartmentName);

                if (foundApartment != null)
                {
                    blok.text = "Blok: " + foundApartment.Blok;
                    daire.text = "Daire: " + foundApartment.Daire;
                    doluluk.text = "Doluluk: " + foundApartment.Doluluk;
                    dairesakinleri.text = "Daire Sakinleri: " + foundApartment.DaireSakinleri;
                    bor�bilgi.text = "Bor� Bilgisi: " + foundApartment.Bor�Bilgisi;
                    aidat.text = "Aidat: " + foundApartment.Aidat;
                    elektrik.text = "Elektrik: " + foundApartment.Faturalar.Elektrik;
                    su.text = "Su: " + foundApartment.Faturalar.Su;
                    do�algaz.text = "Do�algaz: " + foundApartment.Faturalar.Do�algaz;
                    internet.text = "�nternet: " + foundApartment.Faturalar.�nternet;
                }
                else
                {
                    Debug.Log("Daire bulunamad�!");
                    blok.text = "Bulunamad�";
                    daire.text = "Bulunamad�";
                    doluluk.text = "Bulunamad�";
                    dairesakinleri.text = "Bulunamad�";
                    bor�bilgi.text = "Bulunamad�";
                    aidat.text = "Bulunamad�";
                    elektrik.text = "Bulunamad�";
                    su.text = "Bulunamad�";
                    do�algaz.text = "Bulunamad�";
                    internet.text = "Bulunamad�";
                }
            }
        }
    }
}
