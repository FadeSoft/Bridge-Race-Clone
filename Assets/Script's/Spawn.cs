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
        for (int i = -5; i < widht; i++) //BU D�NG� X
        {
            for (int j = -4; j < height; j++)// BU D�NG� Z EKSEN� G�B� D���N�LEB�L�R.
            {
                //�ki adet i� i�e for ile bir nevi matris olu�turuyorum.
                //Bu algoritma oyun ba�lad���nda etrafda rastgele toplanabilecek engeller olu�turmam�za yar�yor.               
                int randomValue = Random.Range(0, 3);
                //Gameobject dizime �� farkl� k�p objesi vermi�tim. �lk olarak rastgele say� �retip dizideki kar��l���na gelen objeyi origin adl� pozisyonda �retiyoruz.
                GameObject obj = Instantiate(cubes[randomValue], origin, transform.rotation,transform);
                //Her bir iterasyon bitmeden �nce objeleri spawnlatt���m origin'e pozisyon de�i�ikli�i i�lemlerini uyguluyorum.
                //B�ylece objeler sonraki iterasyonda �ncekinden farkl� noktada spawnlanm�� oluyor. �st �ste spawnlanman�n �n�ne 25. sat�r ile ge�tim.
                origin = new Vector3(transform.position.x + i, 0.1f, transform.position.z + j);
            }
        }
    }
}
