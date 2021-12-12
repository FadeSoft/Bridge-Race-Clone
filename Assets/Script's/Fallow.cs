using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fallow : MonoBehaviour
{
    public Vector3 offset;
    public GameObject player;
    public float speed;
    void Update()
    {
        //Bu script kameram�n i�erisinde. Dolay�s�yla kameram�n pozisyonu ile oynuyorum.
        //Lerp kullanarak kendi pozisyonundan kendi posizyonu ile inspector'den verdi�im vector3'�n toplam� olan vector3'e do�ru bir hareket sa�l�yoruz.
        //speed de�i�keni ile �arparak bu hareketin yumu�akl���n� belirliyoruz.
        //transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, speed * Time.deltaTime);

        //Bu kodu kullan�rsak bir nevi parmak karakterle ayn� anda hareket edecek gibi d���n�n. �stteki kodda karakter parmak konumuna gelip duruyordu.
        //Sonra parma��n�z�n konumunu de�i�tirmeniz gerekiyordu.
        //Bu kodda ise parmak kald�rmadan s�n�rs�z hareket ger�ekle�tirebilirsiniz.
        transform.position = player.transform.position + offset;

    }
}
