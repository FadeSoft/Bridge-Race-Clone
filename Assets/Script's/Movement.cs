using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Range(1, 200)]
    public float rayDistance;
    [Range(1, 50)]
    public int moveSpeed, rotSpeed;
    public LayerMask layer;
    public Vector3 currentPos;
    public Animator anim;
    public Transform backPos;

    private float yPos=1f;

    void Update()
    {
        RaycastHit hit;
        Vector3 mousePos = Input.mousePosition;
        //Klasik raycast kullan�yorum.
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Vector3 direction = (currentPos - transform.position).normalized;

        if (Physics.Raycast(ray, out hit, rayDistance, layer))
        {
            currentPos = new Vector3(hit.point.x, 0.1f, hit.point.z);
            if (Input.GetMouseButton(0))
            {
                //Ko�ma animasyonunu �al��t�r�yorum.
                anim.SetBool("Running", true);
                //Karaterin pozisyonunu MoveTowards ile de�i�tirmemim sebebi Lerp gibi en ba�ta yava� sona do�ru h�zl� olmas�n� istemiyor olmam.
                //MoveTowards'da bu durumun aksine pozisyonumuz girilen paremetreler aras�nda sabit h�zla de�i�iyor.
                transform.position = Vector3.MoveTowards(transform.position, currentPos, moveSpeed * Time.deltaTime);
                //Karakterin mouse veya parma��n y�n�ne bakmas� i�in basit bir kod yazd�m.
                //Burada d�zg�n �al��mas� i�in bkz. 24. sat�rda ���n�n de�di�i yerden karakterin pozisyonunu ��kar�p bir y�n elde ediyorum.
                //Bu y�n'� verdi�imizde daha do�ru ve net hesaplamalar elde ediyoruz. Alt sat�rda direction yerine currentPos verip denerseniz dedi�imi anlayacaks�n�z.
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            }
            if (Input.GetMouseButtonUp(0))
            {
                anim.SetBool("Running", false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            AddBack(other.gameObject);
        }
        if (other.gameObject.CompareTag("Bridge"))
        {
            Bridge(other.gameObject);
        }
    }

    private void Bridge(GameObject bridgeObj)
    {
        if (backPos.transform.childCount > 0)
        {
            //Buarada merdiven k�sm�na geldi�inde merdivenin mesh renderine eri�ip �e�itli i�lemler yap�yorum
            MeshRenderer mesh = bridgeObj.GetComponent<MeshRenderer>();
            mesh.enabled = true;
            mesh.material.color = Color.red;
            //Yerden toplad���m�z objeleri karakterin s�rt�nda bir objenin cocu�u haline getiriyordum. Burda da ka� adet cocuk obje olduguna bak�yorum.
            int obstacleNumber = backPos.transform.childCount - 1;
            //�rne�in 28 tane cocuk obje olsun. 28. cocuk objeye eri�ip onu destroy ediyorum.
            Destroy(backPos.GetChild(obstacleNumber).gameObject);
            //Bir daha yerden obje toplarsa spawnlanaca�� konum �a�mas�n diye yPos de�erini azalt�yorum.
            yPos -= 0.2f;
            //Boxcollider'ini kapat�yorum ��nk� bir daha �zerinden ge�erse onu alg�lay�p tekrar bu i�lemleri yapmas�n.
            bridgeObj.GetComponent<BoxCollider>().enabled = false;
        }
  
    }
    private void AddBack(GameObject obj)
    {
        //Yerden toplad�g�m�z objeleri bir gameobject'in parant'i haline getiriyorum.
        obj.transform.SetParent(backPos.transform);
        //Rotasyonunu cocugu oldugu objeninkine e�itliyorum.
        obj.transform.rotation = backPos.rotation;
        //Objenin pozisyonunu karakterin s�rt k�sm�ndaki konuma g�re ayarl�yorum.
        obj.transform.position = new Vector3(backPos.position.x,yPos,backPos.position.z);
        //yPos toplanan engelin yeni Y eksenindeki pozisyonunu belirtiyor. Her toplamada bu fonksiyon bir kere �al��t���ndan yPos de�erini bir miktar art�r�yorum.
        //Bir sonraki �al��mada yPos de�eri �ncekinden daha b�y�k olaca�� i�in istedi�imiz gibi daha yukar�da konumlanacak.
        yPos += 0.2f;
    }
}
