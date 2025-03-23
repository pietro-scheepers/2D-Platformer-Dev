using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class WeaponWheelController : MonoBehaviour
{
    private Animator anim;
    private bool weaponWheelSelected = false;
    public static bool isWeaponWheelOpen = false;
    [SerializeField] WeaponWheelButtonController[] buttons;
    public static WeaponWheelController instance;
    public Image selectedItem;
    public static int weaponID = 1;
    [SerializeField]private int lastSelectedWeaponID = 1;
    [SerializeField]private Sprite arrowIconSprite;
    [SerializeField]private Sprite fireIconSprite;
    [SerializeField]private Sprite iceIconSprite;
    [SerializeField]private Sprite gravityIconSprite;
    public GameObject player;

    private void Awake()
    {   
        instance = this;
        anim = GetComponent<Animator>();   
    }
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
            case 1:            
                player.GetComponent<Animator>().SetBool("power",false);
                lastSelectedWeaponID = 1;
                break;
            case 2:
                player.GetComponent<Animator>().SetBool("power",true);
                lastSelectedWeaponID = 2;
                break;
            case 3:
                player.GetComponent<Animator>().SetBool("power",true);
                lastSelectedWeaponID = 3;
                break;
            case 4:
                player.GetComponent<Animator>().SetBool("power",true);
                lastSelectedWeaponID = 4;
                break;
        }
        UpdateSelectedItem();
    }
    private void UpdateSelectedItem()
    {
        switch (lastSelectedWeaponID)
        {
            case 1:
                selectedItem.sprite = arrowIconSprite;
                break;
            case 2:
                selectedItem.sprite = fireIconSprite;
                break;
            case 3:
                selectedItem.sprite = iceIconSprite;
                break;
            case 4:
                selectedItem.sprite = gravityIconSprite;
                break;
            default:
                selectedItem.sprite = null;
                break;
        }
    }
    public void UnlockButton(string attackType){
        foreach (var btn in buttons)
        {
            if ((attackType=="F" && btn.ID==2) || (attackType=="I" && btn.ID==3) || (attackType=="G" && btn.ID==4))
            {
                btn.Unlock();
            }
        }
    }
}
