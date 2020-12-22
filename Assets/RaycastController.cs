using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastController : MonoBehaviour
{
    public float maxDistanceRay = 10f;
    public static RaycastController instance;
   
    public Transform gunFlashTarget;
    public float fireRate = 1.6f;
    private bool nextShot = true;
    private string objName = "";

    AudioSource audio;
    public AudioClip[] clips;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void playSound(int sound){
        audio.clip = clips[sound];
        audio.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine (spawnNewBird ());
    }

    void update(){

    }

    public void Fire(){
        if(nextShot){
            StartCoroutine (takeShot());        
            nextShot = false;
        }
    }

    private IEnumerator takeShot(){
        gunScript.instance.fireSound();

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        int layer_mask = LayerMask.GetMask("birdLayer");
        if(Physics.Raycast(ray, out hit, maxDistanceRay, layer_mask))
        {
            objName = hit.collider.gameObject.name;
            

            if(objName == "pesawat(Clone)")
            {
                Destroy(hit.collider.gameObject);
                StartCoroutine(spawnNewBird());
            }
        }

        GameObject gunFlash = Instantiate(Resources.Load("gunFlashSmoke", typeof(GameObject))) as GameObject;
        gunFlash.transform.position = gunFlashTarget.position;
        
        yield return new WaitForSeconds(fireRate);

        nextShot = true;

        GameObject[] gunSmokeGroup = GameObject.FindGameObjectsWithTag("GunSmoke");
        foreach(GameObject theSmoke in gunSmokeGroup)
        {
            Destroy(theSmoke.gameObject);
        }
    }

    private IEnumerator spawnNewBird(){
        yield return new WaitForSeconds (3f);

        //Spawn new bird
        GameObject newBird = Instantiate(Resources.Load("pesawat", typeof(GameObject))) as GameObject;
        // GameObject newBird2 = Instantiate(Resources.Load("Eagle_Elite", typeof(GameObject))) as GameObject;

        //Make bird child ImageTarget
        newBird.transform.parent = GameObject.Find("ImageTarget").transform;
        // newBird2.transform.parent = GameObject.Find("ImageTarget").transform;

        //Scale bird
        newBird.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        // newBird2.transform.localScale = new Vector3(1f, 1f, 1f);

        // newBird.transform.rotation = Quaternion.Euler(0, 0, 180);
        // var rotationVector = transform.rotation.eulerAngles;
        // rotationVector.z = 180;
        // transform.rotation = Quaternion.Euler(rotationVector);
        //Random Start Position
        Vector3 temp;
        temp.x = Random.Range(-2.5f, 2.5f);
        temp.y = Random.Range(0.4f, 1f);
        temp.z = Random.Range(-2.5f, 2.5f);
        newBird.transform.position = new Vector3 (temp.x, temp.y, temp.z);
        // newBird2.transform.position = new Vector3 (temp.x, temp.y-6.5f, temp.z);
    }
}