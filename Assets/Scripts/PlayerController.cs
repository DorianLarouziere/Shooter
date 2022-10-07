using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] float horizontalSensitivity = 3f;
    // vertical rotation speed
    [SerializeField] float verticalSensitivity = 3f;
    [SerializeField] uint playerSpeed = 5;
    private float mouseX;
    private float mouseY;
    private float xRotation;
    private float yRotation;
    private Vector2 input;
    private Vector3 camF;
    private Vector3 camR;
    private Camera camera;
    GameObject bullet;

    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SpawnBullet();
        }

        /*if (Input.GetButtonDown("Fire3"))
        {
            StartCoroutine(SpawnBullets());
        }*/

        camF = cameraTransform.forward;
        camR = cameraTransform.right;
        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;
        input = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        playerTransform.position +=(camF*input.y + camR*input.x) * Time.deltaTime * playerSpeed;

        mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity;
        mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity;
        yRotation += mouseX;
        xRotation -= mouseY;
        camera.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);

    }

    private void SpawnBullet()
    {
        //Instantiate(bullet, new Vector3(player.transform.position.x+1, player.transform.position.y, player.transform.position.z), Quaternion.identity);

        //J'instantie la balle
        bullet = Instantiate(bulletPrefab, cameraTransform.position, Quaternion.identity);

        //Je récupère son rigidbody
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        //J'applique une force initiale à la balle, AddForce prend en paramètre la direction de la force et son intensité
        bulletRigidbody.AddForce(cameraTransform.forward * 1000f);
    }

    private IEnumerator SpawnBullets() //On crée une coroutine
    {
        for(int i = 0; i<5; i++)
        {
            SpawnBullet();
            yield return new WaitForSeconds(.1f); //Programme attend 2secondes puis fait spawn la deuxième balle
        }
    }
}
