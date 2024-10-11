using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Weapon 1 Slot")]
    public GameObject Weapon1;//°Ë1
   // public bool isWeapon1Picked = false;
    public bool isWeapon1Active = false;
    public SingleMeleeAttack SMAS;

    public bool fistFightMode = false;

    [Header("Weapon 2 Slot")]
    public GameObject Weapon2;//°Ë2
    //public bool isWeapon2Picked = false;
    public bool isWeapon2Active = false;
    public SingleMeleeAttack2 SMAS2;

    [Header("Scripts")]
    public FistFight fistFight;
    public PlayerScript playerScript;
    public GameManager GM;
    public Animator anim;

    [Header("Current Weapons UI")]
    public GameObject NoWeapon;
    public GameObject CurrentWeapon1;
    public GameObject CurrentWeapon2;

    public WeaponMenu weaponmenu;
    private void Awake() {
        GM = FindObjectOfType<GameManager>();

        weaponmenu = FindObjectOfType<WeaponMenu>();
        weaponmenu.SetData(this);

        NoWeapon = weaponmenu.transform.GetChild(0).gameObject;
        CurrentWeapon1 = weaponmenu.transform.GetChild(1).gameObject;
        CurrentWeapon2 = weaponmenu.transform.GetChild(2).gameObject;      
    }

    private void Update()
    {
        if (isWeapon1Active == false && isWeapon2Active == false && fistFightMode == false)
        {
            NoWeapon.SetActive(true);
            fistFightMode = true;
            isRifleActive();
        }

        if (Input.GetMouseButtonDown(0) && isWeapon1Active == false && isWeapon2Active==false && fistFightMode == false)
        {
            fistFightMode = true;
            isRifleActive();
        }

        if(Input.GetKeyDown("1") && isWeapon1Active == false && isWeapon2Active == false)
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

        if(Input.GetKeyDown("2") && isWeapon1Active == false && isWeapon2Active == false)
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
            SMAS.GetComponent<SingleMeleeAttack2>().enabled = true;
            fistFight.GetComponent<FistFight>().enabled = false;
            anim.SetBool("SingleHandAttackActive", true);
            anim.SetBool("FistFightActive", false);
        }
        if(isWeapon2Active == false)
        {
            StartCoroutine(Weapon2GO());
            SMAS.GetComponent<SingleMeleeAttack2>().enabled = false;
            anim.SetBool("SingleHandAttackActive", false);
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
}
