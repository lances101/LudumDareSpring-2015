using UnityEngine;
using System.Collections;

public class DecorateCaramellController : MonoBehaviour
{
    public float myRotationSpeed = 100.0f;
    private int posOrNeg;

    void Start()
    {
        posOrNeg = Random.Range(-1,2);
        if (posOrNeg == 0)
        {
            posOrNeg = 1;
        }

    }

    void Update ()
    {
        transform.Rotate(0, 0, myRotationSpeed * Time.deltaTime * posOrNeg);
    }
}