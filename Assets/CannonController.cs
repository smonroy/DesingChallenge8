using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour {
    public float rotationSpeed;
    public GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
        this.transform.Rotate(new Vector3(0, rotationSpeed, 0));
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.UpArrow) && transform.rotation.eulerAngles.x > 270) {
            this.transform.Rotate(new Vector3(0, rotationSpeed, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow) && transform.rotation.eulerAngles.x < 350) {
            this.transform.Rotate(new Vector3(0, -rotationSpeed, 0));
        }
        if(Input.GetKeyDown(KeyCode.Space)) {
            var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.AngleAxis(90, new Vector3(1, 0, 0)));
            bullet.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            bullet.GetComponent<Rigidbody2D>().velocity = transform.forward * 10;
            Destroy(bullet, 3);
        }
    }
}
