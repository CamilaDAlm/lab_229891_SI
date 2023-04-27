using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed;
    public float gotHayDestroyDelay;
    private bool hitByHay;

    public float dropDestroyDelay;
    private Collider myCollider;
    private Rigidbody myRigidbody;

    private SheepSpawner sheepSpawner;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }
    
    private void HitByHay()
    {
        sheepSpawner.RemoveSheepFromList(gameObject);
        hitByHay = true;
        runSpeed = 0;
        SoundManager.Instance.PlaySheepHitClip();
        if (gameObject.CompareTag("MagSheep"))
        {
            GameStateManager.Instance.MagSavedSheep();
        }
        else if (gameObject.CompareTag("CSheep"))
        {
            GameStateManager.Instance.CyanSavedSheep();
        }
        else if (gameObject.CompareTag("WSheep"))
        {
            GameStateManager.Instance.WoodHitSheep();
        }
        else
        {
            GameStateManager.Instance.SavedSheep();
        }
        Destroy(gameObject, gotHayDestroyDelay);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hay")&& !hitByHay)
        {
            Destroy(other.gameObject);
            HitByHay();
        }else if(other.CompareTag("DropSheep")){
            Drop();
        }
    }

    private void Drop() // sheep falls 
    {
        sheepSpawner.RemoveSheepFromList(gameObject);
        if (gameObject.CompareTag("MagSheep"))
        {
            GameStateManager.Instance.MagDroppedSheep();
        }else if (gameObject.CompareTag("NormalSheep"))
        {
            GameStateManager.Instance.DroppedSheep();
        }
        myRigidbody.isKinematic = false;
        myCollider.isTrigger = false;
        Destroy(gameObject, dropDestroyDelay);
        SoundManager.Instance.PlaySheepDroppedClip();

    }

    public void SetSpawner(SheepSpawner spawner)//spawn sheeps
    {
        sheepSpawner = spawner;
    }

   
}
