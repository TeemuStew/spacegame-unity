using UnityEngine;
/*
 * This script makes the enemy, mainly asteroid, to spin.
 */
public class EnemySpin : MonoBehaviour
{
    public float spinSpeed = 100f; 

    void Update()
    {
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
    }
}
