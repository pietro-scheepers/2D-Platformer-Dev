using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WeaponWheelButtonController : MonoBehaviour
{
    public int ID;
    private Animator anim;
    [SerializeField]private string itemName;
    public TextMeshProUGUI itemText;
    public Sprite icon;
    private bool selected = false;
    [SerializeField]private bool isUnlocked = false;
    public CanvasGroup canvasGroup; 
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Start()
    {
        UpdateButtonState();
    }

    private void Update()
    {
        if (selected && isUnlocked)
        {
            itemText.text = itemName;
        }
    }
    public void Selected(){
        if (!isUnlocked)return;
        selected = true;
        WeaponWheelController.weaponID=ID;
    }
    public void Deselected(){
        selected = false;
    }
    public void HoverEnter(){
        if(!isUnlocked)return;
        anim.SetBool("Hover",true);
        itemText.text = itemName;
    }
    public void HoverExit(){
        anim.SetBool("Hover",false);
        itemText.text ="";
    }
    public void  Unlock()
    {
        isUnlocked = true;
        Selected();
        UpdateButtonState();
    }
    private void UpdateButtonState()
    {
        if (!isUnlocked)
        {
            canvasGroup.alpha = 0.5f;
            GetComponent<Button>().interactable = false;
        }
        else
        {
            canvasGroup.alpha = 1f;
            GetComponent<Button>().interactable = true;
        }
    }
}
