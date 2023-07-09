using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBounce : MonoBehaviour
{
    public float pushForce = 1f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SomeTag"))
        {
            // 충돌 점들의 정보를 담고 있는 배열
            ContactPoint[] contactPoints = collision.contacts;
            Vector3 impulse = collision.impulse;

            // Impulse의 크기 (magnitude)는 충격량의 절대적인 세기를 나타냅니다.
            float collisionStrength = impulse.magnitude;

            // 첫 번째 충돌 점의 정보를 가져옵니다.
            if (contactPoints.Length > 0)
            {
                ContactPoint contactPoint = contactPoints[0];

                // 충돌한 점에서의 충돌 벡터 (법선 벡터)
                Vector3 collisionVector = contactPoint.normal;
                // 물체를 반대 방향으로 밀어냅니다.
                Rigidbody rb = collision.rigidbody;
                rb.AddForce(-pushForce * collisionVector * collisionStrength);
            }
        }
    }
}
