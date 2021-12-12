using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [Range(1, 10)]
    public int widht, height;
    Vector3 origin;

    public GameObject[] cubes;
    void Start()
    {
        for (int i = -5; i < widht; i++) //BU DÖNGÜ X
        {
            for (int j = -4; j < height; j++)// BU DÖNGÜ Z EKSENÝ GÝBÝ DÜÞÜNÜLEBÝLÝR.
            {
                //Ýki adet iç içe for ile bir nevi matris oluþturuyorum.
                //Bu algoritma oyun baþladýðýnda etrafda rastgele toplanabilecek engeller oluþturmamýza yarýyor.               
                int randomValue = Random.Range(0, 3);
                //Gameobject dizime üç farklý küp objesi vermiþtim. Ýlk olarak rastgele sayý üretip dizideki karþýlýðýna gelen objeyi origin adlý pozisyonda üretiyoruz.
                GameObject obj = Instantiate(cubes[randomValue], origin, transform.rotation,transform);
                //Her bir iterasyon bitmeden önce objeleri spawnlattýðým origin'e pozisyon deðiþikliði iþlemlerini uyguluyorum.
                //Böylece objeler sonraki iterasyonda öncekinden farklý noktada spawnlanmýþ oluyor. Üst üste spawnlanmanýn önüne 25. satýr ile geçtim.
                origin = new Vector3(transform.position.x + i, 0.1f, transform.position.z + j);
            }
        }
    }
}
