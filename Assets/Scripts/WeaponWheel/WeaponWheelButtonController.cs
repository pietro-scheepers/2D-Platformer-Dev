using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WeaponWheelButtonController : MonoBehaviour
{
    public int ID;
    private Animator anim;
    public string itemName;
    public TextMeshProUGUI itemText;
    public Image selectedItem;
    public Sprite icon;
    private bool selected = false;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (selected)
        {
            selectedItem.sprite = icon;
            itemText.text = itemName;
        }
    }
    public void Selected(){
        selected = true;
        WeaponWheelController.weaponID=ID;
    }
    public void Deselected(){
        selected = false;
        WeaponWheelController.weaponID=0;
    }
    public void HoverEnter(){
        anim.SetBool("Hover",true);
        itemText.text = itemName;
    }
    public void HoverExit(){
        anim.SetBool("Hover",false);
        itemText.text ="";
    }
}
