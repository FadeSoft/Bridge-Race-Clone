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
        //Klasik raycast kullanýyorum.
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Vector3 direction = (currentPos - transform.position).normalized;

        if (Physics.Raycast(ray, out hit, rayDistance, layer))
        {
            currentPos = new Vector3(hit.point.x, 0.1f, hit.point.z);
            if (Input.GetMouseButton(0))
            {
                //Koþma animasyonunu çalýþtýrýyorum.
                anim.SetBool("Running", true);
                //Karaterin pozisyonunu MoveTowards ile deðiþtirmemim sebebi Lerp gibi en baþta yavaþ sona doðru hýzlý olmasýný istemiyor olmam.
                //MoveTowards'da bu durumun aksine pozisyonumuz girilen paremetreler arasýnda sabit hýzla deðiþiyor.
                transform.position = Vector3.MoveTowards(transform.position, currentPos, moveSpeed * Time.deltaTime);
                //Karakterin mouse veya parmaðýn yönüne bakmasý için basit bir kod yazdým.
                //Burada düzgün çalýþmasý için bkz. 24. satýrda ýþýnýn deðdiði yerden karakterin pozisyonunu çýkarýp bir yön elde ediyorum.
                //Bu yön'ü verdiðimizde daha doðru ve net hesaplamalar elde ediyoruz. Alt satýrda direction yerine currentPos verip denerseniz dediðimi anlayacaksýnýz.
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
            //Buarada merdiven kýsmýna geldiðinde merdivenin mesh renderine eriþip çeþitli iþlemler yapýyorum
            MeshRenderer mesh = bridgeObj.GetComponent<MeshRenderer>();
            mesh.enabled = true;
            mesh.material.color = Color.red;
            //Yerden topladýðýmýz objeleri karakterin sýrtýnda bir objenin cocuðu haline getiriyordum. Burda da kaç adet cocuk obje olduguna bakýyorum.
            int obstacleNumber = backPos.transform.childCount - 1;
            //Örneðin 28 tane cocuk obje olsun. 28. cocuk objeye eriþip onu destroy ediyorum.
            Destroy(backPos.GetChild(obstacleNumber).gameObject);
            //Bir daha yerden obje toplarsa spawnlanacaðý konum þaþmasýn diye yPos deðerini azaltýyorum.
            yPos -= 0.2f;
            //Boxcollider'ini kapatýyorum çünkü bir daha üzerinden geçerse onu algýlayýp tekrar bu iþlemleri yapmasýn.
            bridgeObj.GetComponent<BoxCollider>().enabled = false;
        }
  
    }
    private void AddBack(GameObject obj)
    {
        //Yerden topladýgýmýz objeleri bir gameobject'in parant'i haline getiriyorum.
        obj.transform.SetParent(backPos.transform);
        //Rotasyonunu cocugu oldugu objeninkine eþitliyorum.
        obj.transform.rotation = backPos.rotation;
        //Objenin pozisyonunu karakterin sýrt kýsmýndaki konuma göre ayarlýyorum.
        obj.transform.position = new Vector3(backPos.position.x,yPos,backPos.position.z);
        //yPos toplanan engelin yeni Y eksenindeki pozisyonunu belirtiyor. Her toplamada bu fonksiyon bir kere çalýþtýðýndan yPos deðerini bir miktar artýrýyorum.
        //Bir sonraki çalýþmada yPos deðeri öncekinden daha büyük olacaðý için istediðimiz gibi daha yukarýda konumlanacak.
        yPos += 0.2f;
    }
}
