using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextOjbect;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    public int collectables;

    public GameObject prefab;

    //public AudioSource taco;
    //public AudioSource yoquiero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextOjbect.SetActive(false);

        for(int i = 0; i < collectables - 1; i++)
        {
            Instantiate(prefab, new Vector3(Random.Range(-9.5f, 9.5f), 1, Random.Range(-9.5f, 9.5f)), Quaternion.identity, GameObject.FindGameObjectWithTag("PickUp Parent").transform);
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= collectables)
        {
            winTextOjbect.SetActive(true);
            //yoquiero.Play();
        }
    }

    /**
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            //taco.Play();

            SetCountText();
        }
    }
}
