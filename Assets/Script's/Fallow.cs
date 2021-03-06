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
        //Bu script kameramın içerisinde. Dolayısıyla kameramın pozisyonu ile oynuyorum.
        //Lerp kullanarak kendi pozisyonundan kendi posizyonu ile inspector'den verdiğim vector3'ün toplamı olan vector3'e doğru bir hareket sağlıyoruz.
        //speed değişkeni ile çarparak bu hareketin yumuşaklığını belirliyoruz.
        //transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, speed * Time.deltaTime);

        //Bu kodu kullanırsak bir nevi parmak karakterle aynı anda hareket edecek gibi düşünün. Üstteki kodda karakter parmak konumuna gelip duruyordu.
        //Sonra parmağınızın konumunu değiştirmeniz gerekiyordu.
        //Bu kodda ise parmak kaldırmadan sınırsız hareket gerçekleştirebilirsiniz.
        transform.position = player.transform.position + offset;

    }
}
