using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : Enemy
{

    void BossDefeated()
    {
        Invoke("LoadScene", 1f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(0);
    }

    public override void TookDamage(int damage)
    {

        if (!isDead)
        {
            damaged = true;
            currentHealth -= damage;
            anim.SetTrigger("HitDamage");
            FindObjectOfType<UIManager>().UpdateEnemyUI(maxHealth, currentHealth, enemyName, enemyImage);
            if (currentHealth <= 0)
            {
                isDead = true;
                rig.AddRelativeForce(new Vector3(3, 5, 0), ForceMode.Impulse);
                BossDefeated();
            }
        }

    }

}
