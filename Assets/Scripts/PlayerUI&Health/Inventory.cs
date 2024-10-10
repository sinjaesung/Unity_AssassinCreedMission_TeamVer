using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Weapon 1 Slot")]
    public GameObject Weapon1;
   // public bool isWeapon1Picked = false;
    public bool isWeapon1Active = false;
    public SingleMeleeAttack SMAS;

    public bool fistFightMode = false;

    [Header("Weapon 2 Slot")]
    public GameObject Weapon2;
    //public bool isWeapon2Picked = false;
    public bool isWeapon2Active = false;
    public PlayerShooter playershooterScript;//�⺻����

    [Header("Weapon 3 Slot")]
    public GameObject Weapon3;
   // public bool isWeapon3Picked = false;
    public bool isWeapon3Active = false;
    public PlayerShooter3 playershooter3Script;//����ī����3

    [Header("Weapon 4 Slot")]
    public GameObject Weapon4;
    //public bool isWeapon4Picked = false;
    public bool isWeapon4Active = false;
    public WandShooter1 wandshooter1Script;//�����ϵ�

    [Header("Weapon 5 Slot")]
    public GameObject Weapon5;
   // public bool isWeapon5Picked = false;
    public bool isWeapon5Active = false;
    public WandShooter2 wandshooter2Script;//���̾����

    [Header("Weapon 6 Slot")]
    public GameObject Weapon6;
    // public bool isWeapon6Picked = false;
    public bool isWeapon6Active = false;
    public PlayerShooter2 playershooter2Script;//������

    [Header("Scripts")]
    public FistFight fistFight;
    public PlayerScript playerScript;
    public GameManager GM;
    public Animator anim;

    [Header("Current Weapons UI")]
    public GameObject NoWeapon;
    public GameObject CurrentWeapon1;
    public GameObject CurrentWeapon2;
    public GameObject CurrentWeapon3;
    public GameObject CurrentWeapon4;
    public GameObject CurrentWeapon5;
    public GameObject CurrentWeapon6;

    public WeaponMenu weaponmenu;
    private void Awake() {
        GM = FindObjectOfType<GameManager>();

        weaponmenu = FindObjectOfType<WeaponMenu>();
        weaponmenu.SetData(this);

        NoWeapon = weaponmenu.transform.GetChild(0).gameObject;
        CurrentWeapon1 = weaponmenu.transform.GetChild(1).gameObject;
        CurrentWeapon2 = weaponmenu.transform.GetChild(2).gameObject;
        CurrentWeapon3 = weaponmenu.transform.GetChild(3).gameObject;
        CurrentWeapon4 = weaponmenu.transform.GetChild(4).gameObject;
        CurrentWeapon5 = weaponmenu.transform.GetChild(5).gameObject;
        CurrentWeapon6 = weaponmenu.transform.GetChild(6).gameObject;
    }

    private void Update()
    {
        //�ѹ������ ��� �����߿��� ���⺯�� ����
        if (playershooterScript.gun.state == Gun.State.Reloading ||
        playershooter2Script.gun.state == Gun.State.Reloading ||
        playershooter3Script.gun.state == Gun.State.Reloading
        )
        {
            return;
        }

        if (isWeapon1Active == false && isWeapon2Active == false && isWeapon3Active == false && isWeapon4Active == false && isWeapon5Active == false && isWeapon6Active==false && fistFightMode == false)
        {
            NoWeapon.SetActive(true);
            fistFightMode = true;
            isRifleActive();
        }

        if (Input.GetMouseButtonDown(0) && isWeapon1Active == false && isWeapon2Active==false && isWeapon3Active == false && isWeapon4Active == false && isWeapon5Active == false && isWeapon6Active == false && fistFightMode == false)
        {
            fistFightMode = true;
            isRifleActive();
        }

        if(Input.GetKeyDown("1") && isWeapon1Active == false && isWeapon2Active == false && isWeapon3Active == false && isWeapon4Active == false && isWeapon5Active == false && isWeapon6Active == false)
        {
            isWeapon1Active = true;
            isRifleActive();fistFightMode = false;
            CurrentWeapon1.SetActive(true);
            NoWeapon.SetActive(false);
        }
        else if(Input.GetKeyDown("1") && isWeapon1Active==true)
        {
            isWeapon1Active = false;
            isRifleActive(); 
            CurrentWeapon1.SetActive(false);
        }

        if(Input.GetKeyDown("2") && isWeapon1Active == false && isWeapon2Active == false && isWeapon3Active == false && isWeapon4Active == false && isWeapon5Active == false && isWeapon6Active == false)
        {
            isWeapon2Active = true;
            isRifleActive(); fistFightMode = false;
            CurrentWeapon2.SetActive(true);
            NoWeapon.SetActive(false);
        }
        else if(Input.GetKeyDown("2") && isWeapon2Active == true)
        {
            isWeapon2Active = false;
            isRifleActive();
            CurrentWeapon2.SetActive(false);
        }

        if (Input.GetKeyDown("3") && isWeapon1Active == false && isWeapon2Active == false && isWeapon3Active == false && isWeapon4Active == false && isWeapon5Active == false && isWeapon6Active == false)
        {
            isWeapon3Active = true;
            isRifleActive(); fistFightMode = false;
            CurrentWeapon3.SetActive(true);
            NoWeapon.SetActive(false);
        }
        else if (Input.GetKeyDown("3") && isWeapon3Active == true)
        {
            isWeapon3Active = false;
            isRifleActive();
            CurrentWeapon3.SetActive(false);
        }


        if (Input.GetKeyDown("4") && isWeapon1Active == false && isWeapon2Active == false && isWeapon3Active == false && isWeapon4Active == false && isWeapon5Active == false && isWeapon6Active == false)
        {
            isWeapon4Active = true;
            isRifleActive(); fistFightMode = false;
            CurrentWeapon4.SetActive(true);
            NoWeapon.SetActive(false);
        }
        else if (Input.GetKeyDown("4") && isWeapon4Active == true)
        {
            isWeapon4Active = false;
            isRifleActive();
            CurrentWeapon4.SetActive(false);
        }

        if (Input.GetKeyDown("5") && isWeapon1Active == false && isWeapon2Active == false && isWeapon3Active == false && isWeapon4Active == false && isWeapon5Active == false && isWeapon6Active == false)
        {
            isWeapon5Active = true;
            isRifleActive(); fistFightMode = false;
            CurrentWeapon5.SetActive(true);
            NoWeapon.SetActive(false);
        }
        else if (Input.GetKeyDown("5") && isWeapon5Active == true)
        {
            isWeapon5Active = false;
            isRifleActive();
            CurrentWeapon5.SetActive(false);
        }

        if (Input.GetKeyDown("6") && isWeapon1Active == false && isWeapon2Active == false && isWeapon3Active == false && isWeapon4Active == false && isWeapon5Active == false && isWeapon6Active == false)
        {
            isWeapon6Active = true;
            isRifleActive(); fistFightMode = false;
            CurrentWeapon6.SetActive(true);
            NoWeapon.SetActive(false);
        }
        else if (Input.GetKeyDown("6") && isWeapon6Active == true)
        {
            isWeapon6Active = false;
            isRifleActive();
            CurrentWeapon6.SetActive(false);
        }
        /* if(GM.numberofGrenades <=0 && isWeapon4Active == true)
         {
             Weapon4.SetActive(false);
             isWeapon4Active = false;
             CurrentWeapon4.SetActive(false);
             isRifleActive();
         }*/

        /* if(Input.GetKeyDown("5") && isWeapon1Active == false && isWeapon2Active == false && isWeapon3Active == false && isWeapon4Active == false && GM.numberofHealth > 0 && playerScript.presentHealth < 95)
         {
             StartCoroutine(IncreaseHealth()); 
         }

         if (Input.GetKeyDown("6") && isWeapon1Active == false && isWeapon2Active == false && isWeapon3Active == false && isWeapon4Active == false && GM.numberofEnergy > 0 && playerScript.presentEnergy < 95)
         {
             StartCoroutine(IncreaseEnergy()); 
         }*/
    }

    void isRifleActive()
    {
        if(fistFightMode == true)
        {
            fistFight.GetComponent<FistFight>().enabled = true;
        }
        

        if(isWeapon1Active == true)
        {
            StartCoroutine(Weapon1GO());
            SMAS.GetComponent<SingleMeleeAttack>().enabled = true;
            fistFight.GetComponent<FistFight>().enabled = false;
            anim.SetBool("SingleHandAttackActive", true);
            anim.SetBool("FistFightActive", false);
        }
        if (isWeapon1Active == false)
        {
            StartCoroutine(Weapon1GO());
            SMAS.GetComponent<SingleMeleeAttack>().enabled = false;
            anim.SetBool("SingleHandAttackActive", false);
        }

        if(isWeapon2Active == true)
        {
            StartCoroutine(Weapon2GO());//Rifle
            //rifle.GetComponent<Rifle>().enabled = true;
            playershooterScript.enabled = true;
            fistFight.GetComponent<FistFight>().enabled = false;
            anim.SetBool("RifleActive", true);
            anim.SetBool("FistFightActive", false);
        }
        if(isWeapon2Active == false)
        {
            StartCoroutine(Weapon2GO());
            playershooterScript.enabled = false;
            anim.SetBool("RifleActive", false);
        }

        if (isWeapon3Active == true)
        {
            //����ī
            StartCoroutine(Weapon3GO());
            //bazooka.GetComponent<Bazooka>().enabled = true;
            playershooter3Script.enabled = true;
            fistFight.GetComponent<FistFight>().enabled = false;
            anim.SetBool("FistFightActive", false);
            anim.SetBool("BazookaActive", true);
        }
        if (isWeapon3Active == false)
        {
            StartCoroutine(Weapon3GO());
            playershooter3Script.enabled = false;
            anim.SetBool("BazookaActive", false);
        }

        //��������
        if (isWeapon4Active == true)
        {
            StartCoroutine(Weapon4GO());
            wandshooter1Script.enabled = true;
            fistFight.enabled = false;
            anim.SetBool("BazookaActive", true);
            anim.SetBool("FistFightActive", false);
        }
        if (isWeapon4Active == false)
        {
            StartCoroutine(Weapon4GO());
            wandshooter1Script.enabled = false;
            anim.SetBool("BazookaActive", false);
        }

        //�ҹ���
        if (isWeapon5Active == true)
        {
            StartCoroutine(Weapon5GO());
            wandshooter2Script.enabled = true;
            fistFight.enabled = false;
            anim.SetBool("BazookaActive", true);
            anim.SetBool("FistFightActive", false);
        }
        if (isWeapon5Active == false)
        {
            StartCoroutine(Weapon5GO());
            wandshooter2Script.enabled = false;
            anim.SetBool("BazookaActive", false);
        }

        if (isWeapon6Active == true)
        {
            //������
            StartCoroutine(Weapon6GO());
            //bazooka.GetComponent<Bazooka>().enabled = true;
            playershooter2Script.enabled = true;
            fistFight.GetComponent<FistFight>().enabled = false;
            anim.SetBool("FistFightActive", false);
            anim.SetBool("RifleActive", true);
        }
        if (isWeapon6Active == false)
        {
            StartCoroutine(Weapon6GO());
            playershooter2Script.enabled = false;
            anim.SetBool("RifleActive", false);
        }
    }

    IEnumerator Weapon1GO()
    {
        if (!isWeapon1Active)
        {
            Weapon1.SetActive(false);
        }
        yield return new WaitForSeconds(0.1f);
        if (isWeapon1Active)
        {
            Weapon1.SetActive(true);
        }
    }
    IEnumerator Weapon2GO()
    {
        if (!isWeapon2Active)
        {
            Weapon2.SetActive(false);
        }
        yield return new WaitForSeconds(0.1f);
        if (isWeapon2Active)
        {
            Weapon2.SetActive(true);
        }
    }
    IEnumerator Weapon3GO()
    {
        if (!isWeapon3Active)
        {
            Weapon3.SetActive(false);
        }
        yield return new WaitForSeconds(0.1f);
        if (isWeapon3Active)
        {
            Weapon3.SetActive(true);
        }
    }
    IEnumerator Weapon4GO()
    {
        if (!isWeapon4Active)
        {
            Weapon4.SetActive(false);
        }
        yield return new WaitForSeconds(0.1f);
        if (isWeapon4Active)
        {
            Weapon4.SetActive(true);
        }
    }
    IEnumerator Weapon5GO()
    {
        if (!isWeapon5Active)
        {
            Weapon5.SetActive(false);
        }
        yield return new WaitForSeconds(0.1f);
        if (isWeapon5Active)
        {
            Weapon5.SetActive(true);
        }
    }
    IEnumerator Weapon6GO()
    {
        if (!isWeapon6Active)
        {
            Weapon6.SetActive(false);
        }
        yield return new WaitForSeconds(0.1f);
        if (isWeapon6Active)
        {
            Weapon6.SetActive(true);
        }
    }
    /*IEnumerator IncreaseHealth()
    {
        anim.SetBool("Drink", true);
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("Drink", false);
        GM.numberofHealth -= 1;
        playerScript.presentHealth = 200f;
        playerScript.healthbar.GiveFullHealth(200f);
    }
    IEnumerator IncreaseEnergy()
    {
        anim.SetBool("Drink", true);
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("Drink", false);
        GM.numberofEnergy -= 1;
        playerScript.presentEnergy = 100f;
        playerScript.energybar.GiveFullEnergy(100f);
    }*/
}
