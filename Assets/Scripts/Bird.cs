using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] GameManager gameManager; // Reference to the GameManager
    [SerializeField] float delay = 3.5f; // Delay before calling KillBird

    private bool isThrown = false; // Flag to indicate if the bird has been thrown
    private Vector3 initialPosition;
    private Rigidbody2D rb;

    public void Awake()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMouseUp()
    {
        
        Vector2 directionToInitialPosition = initialPosition - transform.position; // Get the direction from the current position to the initial position
        rb.gravityScale  = 1; // Enable gravity
        rb.AddForce(directionToInitialPosition * speed);
        isThrown = true;
    }

    public void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }

    private bool DestroyBird()
    {
        if (isThrown && rb.velocity.magnitude < 0.1f) // If the bird has been thrown and its velocity is less than 0.1f
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Update()
    {
        if (isThrown)
        {
            StartCoroutine(DestroyDelay()); // Call the DestroyDelay function when the bird is thrown 
        }
    }

    IEnumerator DestroyDelay()
    {
       
        yield return new WaitForSeconds(delay); 

        // Call KillBird function on the GameManager
        if (DestroyBird())
        {
            gameManager.KillBird();
        }
    }
}
