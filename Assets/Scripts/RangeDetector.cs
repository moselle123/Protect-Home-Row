using UnityEngine;

public class RangeDetector : MonoBehaviour
{
    public float detectionRadius = 2f;
    public LayerMask enemyLayer;

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);

        foreach (Collider2D collider in colliders)
        {
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}