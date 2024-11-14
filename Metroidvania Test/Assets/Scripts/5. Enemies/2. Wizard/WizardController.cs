using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : EnemyController
{
    public override void LoseLife(int damage)
    {
        base.LoseLife(damage);
        StartCoroutine(ForceResetTrigger());
    }

    private IEnumerator ForceResetTrigger()
    {
        yield return new WaitForSeconds(_enemyAnimator.GetCurrentAnimatorStateInfo(0).length);
        _enemyAnimator.CrossFade("Wizard_Idle", 0.01f);
    }
}
