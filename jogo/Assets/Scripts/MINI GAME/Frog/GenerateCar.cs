using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCar : MonoBehaviour
{
    [SerializeField] private GameObject carLeft;
    [SerializeField] private GameObject carRight;
    private GameObject[] pointsA;
    private GameObject[] pointsB;
    private float timeCarLeft;
    private float timeCarRight;
    
    // Start is called before the first frame update
    void Start()
    {
        pointsA = GameObject.FindGameObjectsWithTag("PontoA");
        pointsB = GameObject.FindGameObjectsWithTag("PontoB");

        Instantiate(carLeft, pointsA[Random.Range(0, pointsA.Length)].transform.position, Quaternion.identity);
        Instantiate(carRight, pointsB[Random.Range(0, pointsB.Length)].transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        CarLeft();
        CarRight();
    }

    private void CarLeft()
    {
        timeCarLeft += Time.deltaTime;

        if (timeCarLeft > Random.Range(1.5f, 2.5f))
        {
            Instantiate(carLeft, pointsA[Random.Range(0, pointsA.Length)].transform.position, Quaternion.identity);
            timeCarLeft = 0;
        }
    }
    private void CarRight()
    {
        timeCarRight += Time.deltaTime;

        if (timeCarRight > Random.Range(1.5f, 2.5f))
        {
            Instantiate(carRight, pointsB[Random.Range(0, pointsB.Length)].transform.position, Quaternion.identity);
            timeCarRight = 0;
        }
    }
}
