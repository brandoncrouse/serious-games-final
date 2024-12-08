using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float maxSpeed;
    public bool canInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        currentInput = currentInput.normalized;
        if (!canInput) 
        {
            currentInput = Vector2.zero;
        } 
        rb.linearVelocity = currentInput * maxSpeed;
    }

    public void TakeInputs() => canInput = false;

    public void ReturnInputs() => canInput = true;

    public void ReturnInputsInTime()
    {
        Invoke("ReturnInputs", 0.3f);
    }
}
