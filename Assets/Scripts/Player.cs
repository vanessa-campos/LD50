using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private float SpeedConst, Speed;
    [SerializeField] Slider staminaBar, lifeBar;
    [SerializeField] TextMeshProUGUI staminaText, lifeText;

    private float horizontalInput, verticalInput;
    private Rigidbody rgb;
    float stamina;
    int life;
    public float Stamina
    {
        get { return stamina; }
        set { stamina = value; if (stamina < 0){ stamina = 0;} if (stamina > 100){ stamina = 100; staminaText.text = stamina.ToString(); } }
    }
    public int Life
    {
        get { return life; }
        set { life = value; if (life < 0) { life = 0; } if (life > 100){ life = 100;  lifeText.text = life.ToString(); } }
    }

    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody>();
        Life = 4;
        Stamina = 100;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LifeSytem();
    }

    void Move()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        rgb.velocity = new Vector3(horizontalInput * Speed, verticalInput * Speed, SpeedConst);
    }
    void LifeSytem()
    {
        if (horizontalInput != 0 | verticalInput != 0)
        {
            Stamina -= 1f * Time.deltaTime;
        }
    }

    // void OnCollisionEnter(Collision col)
    // {
    //     if (col.gameObject.CompareTag("Wall"))
    //     {
    //         Life -= 1;
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        // rgb.AddForce(other.GetComponent<Wind>().Direction * other.GetComponent<Wind>().Strength, ForceMode.Impulse);
        if (other.gameObject.CompareTag("Wall"))
        {
            Life -= 1;
        }
        if (other.gameObject.CompareTag("Wind"))
        {
            rgb.AddForce(other.GetComponent<Wind>().Direction * other.GetComponent<Wind>().Strength, ForceMode.Impulse);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wind"))
        {
            rgb.AddForce(other.GetComponent<Wind>().Direction * other.GetComponent<Wind>().Strength, ForceMode.Impulse);
        }
    }
}
