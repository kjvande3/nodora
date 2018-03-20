using UnityEngine;
using UnityEngine.UI;

public class chestController : MonoBehaviour
{
    public GameObject character = null;
    public GameObject chest = null;
    public double distanceToOpen = 5;
    public bool show = false;

    SpriteRenderer spriteRenderer;

    // Start of program
    void Start()
    {
        chest.SetActive(show);
    }

    // Update Function
    void Update()
    {
        var distance = Vector3.Distance(transform.position, character.transform.position);

        // Show/Hide Chest
        if (show == true) {
            chest.SetActive(true); }
        else {
            chest.SetActive(false); }

        // Open Chest
        if (Input.GetKeyDown(KeyCode.E) && distanceToOpen >= distance) {
            spriteRenderer.sprite = GameObject.Find("chestOpen").GetComponent<Sprite>();
        }
            
    }

    // Change state of chest
    public void showChest(bool show)
    {
        show = this.show;
    }

}