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
        //Bu script kameramýn içerisinde. Dolayýsýyla kameramýn pozisyonu ile oynuyorum.
        //Lerp kullanarak kendi pozisyonundan kendi posizyonu ile inspector'den verdiðim vector3'ün toplamý olan vector3'e doðru bir hareket saðlýyoruz.
        //speed deðiþkeni ile çarparak bu hareketin yumuþaklýðýný belirliyoruz.
        //transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, speed * Time.deltaTime);

        //Bu kodu kullanýrsak bir nevi parmak karakterle ayný anda hareket edecek gibi düþünün. Üstteki kodda karakter parmak konumuna gelip duruyordu.
        //Sonra parmaðýnýzýn konumunu deðiþtirmeniz gerekiyordu.
        //Bu kodda ise parmak kaldýrmadan sýnýrsýz hareket gerçekleþtirebilirsiniz.
        transform.position = player.transform.position + offset;

    }
}
