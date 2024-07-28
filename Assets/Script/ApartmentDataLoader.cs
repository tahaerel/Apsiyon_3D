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
    public string BorçBilgisi;
    public string Aidat;
    public Bills Faturalar;
}

[Serializable]
public class Bills
{
    public string Elektrik;
    public string Su;
    public string Doðalgaz;
    public string Ýnternet;
}

public class ApartmentDataLoader : MonoBehaviour
{
    public string jsonUrl = "https://pushup.games/Apartment_Datas.json";
    public TextMeshProUGUI blok, daire, doluluk, dairesakinleri, borçbilgi, aidat, elektrik, su, doðalgaz, internet;

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
                    borçbilgi.text = "Borç Bilgisi: " + foundApartment.BorçBilgisi;
                    aidat.text = "Aidat: " + foundApartment.Aidat;
                    elektrik.text = "Elektrik: " + foundApartment.Faturalar.Elektrik;
                    su.text = "Su: " + foundApartment.Faturalar.Su;
                    doðalgaz.text = "Doðalgaz: " + foundApartment.Faturalar.Doðalgaz;
                    internet.text = "Ýnternet: " + foundApartment.Faturalar.Ýnternet;
                }
                else
                {
                    Debug.Log("Daire bulunamadý!");
                    blok.text = "Bulunamadý";
                    daire.text = "Bulunamadý";
                    doluluk.text = "Bulunamadý";
                    dairesakinleri.text = "Bulunamadý";
                    borçbilgi.text = "Bulunamadý";
                    aidat.text = "Bulunamadý";
                    elektrik.text = "Bulunamadý";
                    su.text = "Bulunamadý";
                    doðalgaz.text = "Bulunamadý";
                    internet.text = "Bulunamadý";
                }
            }
        }
    }
}
