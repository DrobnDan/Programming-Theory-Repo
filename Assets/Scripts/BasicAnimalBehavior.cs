using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BasicAnimalBehavior : MonoBehaviour
{
    protected bool collidedWithGround = false;
    private Rigidbody animalRB;
    protected float animalSpeed = 10.0f;
    Vector3 randomPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        animalRB = GetComponent<Rigidbody>();
        Destroy(gameObject, 25); 
    }

    // Update is called once per frame
    void Update()
    {
        if (collidedWithGround)
        {
            moveAnimalToRandomPos();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
      if (other.gameObject.CompareTag("Ground") && !collidedWithGround)
        {
            StabilizeAnimal(animalRB);
            Invoke("UnfreezeRotation", 5);
            collidedWithGround = true;
        }
    }

    public void UnfreezeRotation()
    {
        gameObject.GetComponent<Rigidbody>().freezeRotation = false;
    }

    public void StabilizeAnimal(Rigidbody rb)
    {
        rb.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        rb.freezeRotation = true;
    }

    public void moveAnimalToRandomPos()
    {
        if (Vector3.Distance(transform.position, randomPos) < 1)
        {
            randomPos = new Vector3(Random.Range(-12, 7), transform.position.y, Random.Range(-9, 12));
        }
        MoveAndRotateToTargetPos();
    }

    public void MoveAndRotateToTargetPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, randomPos, animalSpeed * Time.deltaTime);

        Vector3 targetDirection = randomPos - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, .15f, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public virtual void setAnimalSpeed()
    {
        animalSpeed = 10;
    }

    public virtual void setAnimalSpeed(float animSpeed)
    {
        animalSpeed *= 2;
    }

}
