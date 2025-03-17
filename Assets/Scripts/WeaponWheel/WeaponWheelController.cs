using UnityEngine;
using UnityEngine.UI;
public class WeaponWheelController : MonoBehaviour
{
    public Animator anim;
    private bool weaponWheelSelected = false;
    public static bool isWeaponWheelOpen = false;

    public Image selectedItem;
    public Sprite noImage;
    public static int weaponID;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            weaponWheelSelected = !weaponWheelSelected;
            isWeaponWheelOpen = weaponWheelSelected;
        }
        if (weaponWheelSelected)
        {
            anim.SetBool("OpenWeaponWheel",true);
        }else{
            
            anim.SetBool("OpenWeaponWheel",false);
        }
        switch (weaponID)
        {
            case 0:
            selectedItem.sprite = noImage;
            break;
            case 1:
            Debug.Log("Arrow");
            break;
            case 2:
            Debug.Log("Fire");
            break;
            case 3:
            Debug.Log("Ice");
            break;
            case 4:
            Debug.Log("Gravity");
            break;
        }
    }

}
