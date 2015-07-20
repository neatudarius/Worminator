using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public float moveSpeed = 5;
    public bool useTorque = true;
    public float maxAngularVelocity = 25;
    public float jumpPower = 2;
    public const float groundRayLength = 1f;
    private Rigidbody thisRigidbody;

    private Transform mainCamera;
    private Vector3 mainCameraForward;
    private bool jump;
    private Vector3 move;
    private Transform defaultParant;

    public GameObject messageText;
    public Text coinsText;
    private int coinTotal;
    private int coinCount;
    public bool lastLevel;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mainCamera = Camera.main.transform;
        thisRigidbody = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().maxAngularVelocity = maxAngularVelocity;

        coinTotal = GameObject.FindGameObjectsWithTag("Coin").Length;
        coinsText.text = "Coins: " + coinCount.ToString() + "/" + coinTotal.ToString();
        defaultParant = transform.parent;
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        jump = Input.GetButton("Jump");

        if (mainCamera != null)
        {
            mainCameraForward = Vector3.Scale(mainCamera.forward, new Vector3(1, 0, 1)).normalized;
            move = (v * mainCameraForward + h * mainCamera.right).normalized;
        }
        else
        {
            move = (v * Vector3.forward + h * Vector3.right).normalized;
        }

        if (transform.position.y < -5)
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("Menu");
        }
    }

    private void FixedUpdate()
    {
        if (useTorque)
        {
            thisRigidbody.AddTorque(new Vector3(move.z, 0, -move.x) * moveSpeed);
        }
        else
        {
            thisRigidbody.AddForce(move * moveSpeed);
        }

        if (Physics.Raycast(transform.position, -Vector3.up, groundRayLength) && jump)
        {
            thisRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider _hit)
    {
        if (_hit.tag == "Coin")
        {
            Destroy(_hit.gameObject);
            coinCount++;
            coinsText.text = "Coins: " + coinCount.ToString() + "/" + coinTotal.ToString();
        }
        else if (_hit.tag == "Next")
        {
            if (coinCount == coinTotal)
            {
                if (lastLevel)
                {
                    Application.LoadLevel("Menu");
                }
                else
                {
                    Application.LoadLevel(Application.loadedLevel + 1);
                }
            }
            else
            {
                StartCoroutine(ShowMessage("You need to find all the coins"));
            }
        }
        else if (_hit.tag == "Platform")
        {
            Vector3 tmp = transform.localScale;

            transform.parent = _hit.transform;

            transform.localScale = tmp;
        }
    }
    void OnTriggerExit(Collider _hit)
    {
        if (_hit.tag == "Platform")
        {
            transform.parent = defaultParant;
        }
    }

    IEnumerator ShowMessage(string _msg)
    {
        messageText.SetActive(true);
        messageText.transform.FindChild("Text").GetComponent<Text>().text = _msg;

        yield return new WaitForSeconds(2);
        messageText.SetActive(false);
    }
}