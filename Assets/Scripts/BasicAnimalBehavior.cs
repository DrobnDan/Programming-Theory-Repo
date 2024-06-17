using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BasicAnimalBehavior : MonoBehaviour
{
    [SerializeField] bool collidedWithGround = false;
    private Rigidbody animalRB;
    private float animalSpeed = 10.0f;
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
            StabilizeAnimal();
            Invoke("UnfreezeRotation", 5);
            collidedWithGround = true;
        }
    }

    private void UnfreezeRotation()
    {
        animalRB.freezeRotation = false;
    }

    private void StabilizeAnimal()
    {
        animalRB.rotation = Quaternion.identity;
        animalRB.velocity = Vector3.zero;
        animalRB.freezeRotation = true;
    }

    private void moveAnimalToRandomPos()
    {
        if (Vector3.Distance(transform.position, randomPos) < 1)
        {
            randomPos = new Vector3(Random.Range(-12, 7), transform.position.y, Random.Range(-9, 12));
        }
        MoveAndRotateToTargetPos();
    }

    private void MoveAndRotateToTargetPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, randomPos, animalSpeed * Time.deltaTime);

        Vector3 targetDirection = randomPos - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, .15f, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
