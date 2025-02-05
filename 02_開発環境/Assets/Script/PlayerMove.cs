using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Text ammotext;
    [SerializeField] GameObject SettingUI;
    Score GameMane;
    Rigidbody rd;
    public InputField inputField;
    float x = 0;
    float y = 0;
    public float speed = 5;
    public float mouseSensitivity = 100f;
    float rotationY = 0f;
    float rotationX = 0f;
    public bool isAStop = false;
    public int shootCount = 0;
    public Transform cameraTransform; // Reference to the camera
    public GameObject bulletPrefab; // Bullet prefab
    public Transform bulletSpawnPoint; // Where bullets are spawned
    public float bulletForce = 700f; // Force applied to bullets
    public AudioClip Shot;
    public AudioClip Reroad;
    public AudioClip Enemy;
    AudioSource audioSource;

    public void Start()
    {
        rd = GetComponent<Rigidbody>();
        inputField = inputField.GetComponent<InputField>();
        GameMane = GameObject.Find("GameMane").GetComponent<Score>();
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the game window
        shootCount = 30;
        inputField.text = mouseSensitivity.ToString();
        isAStop = false;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            inputField.text = mouseSensitivity.ToString();
            if (isAStop)
            {
                isAStop = false;
                SettingUI.SetActive(false);
                GameMane.GetComponent<Score>().isOpen = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                isAStop = true;
                SettingUI.SetActive(true);
                GameMane.GetComponent<Score>().isOpen = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
        if (isAStop)
        {
            if (float.TryParse(inputField.text, out float a))
            {
                if(a > 1000 || a < 0)
                {
                    a = 100;
                }
                mouseSensitivity = a;
            }
            else
            {
                a = 100;
                mouseSensitivity = a;
            }
        }
        if (!isAStop)
        {
            // Get keyboard input
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");

            // Get mouse input
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Rotate the player around the Y-axis
            rotationY += mouseX;
            transform.rotation = Quaternion.Euler(0, rotationY, 0);

            // Rotate the camera around the X-axis
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Clamp vertical rotation to prevent over-rotation
            cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            // Shoot bullet on left mouse click
            if (Input.GetButtonDown("Fire1") && shootCount > 0)
            {
                Shoot();
            }
            if (Input.GetButtonDown("Fire1") && shootCount == 0)
            {
                audioSource.PlayOneShot(Enemy);
            }
            if (Input.GetKeyDown("r"))
            {
                shootCount = 30;
                audioSource.PlayOneShot(Reroad);
            }
            ammotext.GetComponent<Text>().text = $"ammo:{shootCount}";
        }
    }

    void FixedUpdate()
    {
        if (!isAStop)
        {
            // 現在のY座標を保持
            float currentYPosition = rd.position.y;

            // プレイヤーを入力に基づいて移動
            Vector3 move = transform.right * x + transform.forward * y;

            // Y座標を固定して移動
            rd.velocity = new Vector3(move.x * speed, rd.velocity.y, move.z * speed);

            // Y軸の位置を保持
            rd.position = new Vector3(rd.position.x, currentYPosition, rd.position.z);
        }
    }

    void Shoot()
    {
        // Instantiate the bullet at the spawn point
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Get the Rigidbody of the bullet and apply force
        Rigidbody bulletRd = bullet.GetComponent<Rigidbody>();
        bulletRd.AddForce(bulletSpawnPoint.forward * bulletForce);
        rotationX += -3;
        shootCount--;
        audioSource.PlayOneShot(Shot);
    }
}