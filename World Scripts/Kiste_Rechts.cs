using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kiste_Rechts : MonoBehaviour
{
    public GameObject PlayerCheckobj;
    public GameObject Kisteobj;
    private Kiste_Check KisteCheck;
    public bool isopen;
    public GameObject Sniper, M4, Glock, Ak47, Small_Ammo, Mid_Ammo, Big_Ammo;
    // Start is called before the first frame update
    void Start()
    {
        KisteCheck =  PlayerCheckobj.GetComponent<Kiste_Check>();
    }

    // Update is called once per frame
    void Update()
    {
        if(KisteCheck.Contact && KisteCheck.open && !isopen){
            StartCoroutine(Open());
        }
    }

    IEnumerator Open(){
        isopen = true;
        //2 Waffen spawnen
        for(int i = 0 ;i != 2; i++){
            int weaponnum =  Random.Range(1, 100);
            if(weaponnum >= 1 && weaponnum <= 20){ //20% auf Ak
                Instantiate(Ak47,  new Vector3(Kisteobj.transform.position.x + Random.Range(1f, 2f), Kisteobj.transform.position.y - Random.Range(-1f, 1f), 120f), new Quaternion(0,0,0,0));
            }else if(weaponnum > 20 && weaponnum <= 50){ //30% auf M4
                Instantiate(M4,  new Vector3(Kisteobj.transform.position.x + Random.Range(1f, 2f), Kisteobj.transform.position.y - Random.Range(-1f, 1f), 120f), new Quaternion(0,0,0,0));
            }else if(weaponnum > 50 && weaponnum <= 60){ //10% auf Sniper
                Instantiate(Sniper,  new Vector3(Kisteobj.transform.position.x + Random.Range(1f, 2f), Kisteobj.transform.position.y - Random.Range(-1f, 1f), 120f), new Quaternion(0,0,0,0));
            }else if(weaponnum > 60 && weaponnum <= 100){ //40% auf Glock
                Instantiate(Glock, new Vector3(Kisteobj.transform.position.x + Random.Range(1f, 2f), Kisteobj.transform.position.y - Random.Range(-1f, 1f), 120f), new Quaternion(0,0,0,0));
            }else Debug.Log("Kiste hat keine Waffe ausgegeben" + weaponnum);
            yield return new WaitForSeconds(0.01f);
        }
        //2 Munitionsstapel Spawnen
        for(int i = 0 ;i != 2; i++){
            int Ammonum =  Random.Range(1, 3);
            if(Ammonum == 1){ //33% auf Small ammo
                Instantiate(Small_Ammo,  new Vector3(Kisteobj.transform.position.x + Random.Range(1f, 2f), Kisteobj.transform.position.y - Random.Range(-1f, 1f), 120f), new Quaternion(0,0,0,0));
            }else if(Ammonum == 2){ //33% auf Mid ammo
                Instantiate(Mid_Ammo, new Vector3(Kisteobj.transform.position.x + Random.Range(1f, 2f), Kisteobj.transform.position.y - Random.Range(-1f, 1f), 120f), new Quaternion(0,0,0,0));
            }else if(Ammonum == 3){ //33% auf Big ammo
                Instantiate(Big_Ammo,  new Vector3(Kisteobj.transform.position.x + Random.Range(1f, 2f), Kisteobj.transform.position.y - Random.Range(-1f, 1f), 120f), new Quaternion(0,0,0,0));
            }else Debug.Log("Kiste hat keine Ammo ausgegeben" + Ammonum);
            yield return new WaitForSeconds(0.01f);
        }
        Kisteobj.GetComponent<BoxCollider2D>().isTrigger = true;
    }
}
