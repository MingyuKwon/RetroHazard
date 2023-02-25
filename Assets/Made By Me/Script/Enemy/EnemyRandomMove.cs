using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomMove : MonoBehaviour
{
    private EnemyManager enemyManager;
    public float RandomRange = 4;

    const float RandomMoveSlow = 0.6f;
    const float timeElapsedLimit = 3f;

    private bool First = true;

    private void Awake() {
        
        enemyManager = GetComponent<EnemyManager>();
    }
    private void OnEnable() {
        if(First)
        {
            enemyManager.RandomStartPosition = transform.position;
            First = false;
        }
    
        StartCoroutine(MoveRandomCircle());
    }

    IEnumerator MoveRandomCircle()
    {
        Vector2 previousRandomUnitCircle = new Vector2(-1,0);

        while(true)
        {
            Vector2 randomCircle;

            do
            {
                randomCircle = Random.insideUnitCircle;
            }while(Vector3.Magnitude(previousRandomUnitCircle - randomCircle) < 1.5);

            previousRandomUnitCircle = randomCircle;

            Vector3 RandomPositionCircle;
            RandomPositionCircle = enemyManager.RandomStartPosition + new Vector3(randomCircle.x, randomCircle.y, 0) * RandomRange;
                
            float timeElapsed = 0f;
            while( Vector3.Magnitude(RandomPositionCircle - transform.position) > 1 && timeElapsed < timeElapsedLimit)
            {
                Vector3 towardPlayerDirection =  new Vector3(RandomPositionCircle.x - transform.position.x, RandomPositionCircle.y - transform.position.y, RandomPositionCircle.z - transform.position.z).normalized;
                SetXYAnimation(towardPlayerDirection);

                float towardPlayerMagnitude = enemyManager.enemySpeed * Time.fixedDeltaTime * RandomMoveSlow;
                transform.Translate(towardPlayerDirection * towardPlayerMagnitude);
                timeElapsed += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            
        }

    }

    private void SetXYAnimation(Vector3 towardPlayerDirection)
    {
        bool isXBig = Mathf.Abs(towardPlayerDirection.x) >= Mathf.Abs(towardPlayerDirection.y);

        if(isXBig)
        {
            if(towardPlayerDirection.x > 0)
            {
                enemyManager.animationX = 1;
            }else
            {
                enemyManager.animationX = -1;
            }
            enemyManager.animationY = 0;
        }else
        {
            if(towardPlayerDirection.y > 0)
            {
                enemyManager.animationY = 1;
            }else
            {
                enemyManager.animationY = -1;
            }
            enemyManager.animationX = 0;
        }
    }

    


}
