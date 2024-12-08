using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    PlayerController pc;
    DialogueScript heldScript;

    void Awake()
    {
        pc = transform.parent.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetAxis("Horizontal") < 0f) {transform.eulerAngles = new Vector3(0f, 0f, 90f);}
        if (Input.GetAxis("Horizontal") > 0f) {transform.eulerAngles = new Vector3(0f, 0f, -90f);}
        if (Input.GetAxis("Vertical") < 0f) {transform.eulerAngles = new Vector3(0f, 0f, 180f);}
        if (Input.GetAxis("Vertical") > 0f) {transform.eulerAngles = new Vector3(0f, 0f, 0f);}

        if (pc.canInput)
        {
            if (Input.GetKeyDown(KeyCode.Z) && heldScript != null)
            {
                GameManager.Instance.LoadNewDialogue(heldScript);
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.GetComponent<Interactable>())
        {
            heldScript = col.GetComponent<Interactable>().heldScript;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<Interactable>())
        {
            heldScript = null;
        }
    }
}
