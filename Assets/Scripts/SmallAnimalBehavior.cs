using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE Child method inherets a lot of functionality from a parent method.
public class SmallAnimalBehavior : BasicAnimalBehavior
{
    private bool collidedWithGround = false;
    private Rigidbody smallAnimalRB;
    // Start is called before the first frame update
    void Start()
    {
        setAnimalSpeed();
        smallAnimalRB = GetComponent<Rigidbody>();
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
            StabilizeAnimal(smallAnimalRB);
            Invoke("UnfreezeRotation", 5);
            collidedWithGround = true;
        }
    }

    //POLYMORPHISM Overriden method for different functionality in child class.
    public override void setAnimalSpeed()
    {
        animalSpeed = 20;
    }
}
