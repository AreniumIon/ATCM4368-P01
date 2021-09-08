using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slower : Enemy
{
    [SerializeField] float _slowAmount;
    [SerializeField] float _slowTime;

    [SerializeField] AudioClip _restoreSpeedSound;

    protected override bool PlayerImpact(Player player)
    {
        TankController controller = player.GetComponent<TankController>();
        if (controller != null)
        {
            controller.MoveSpeed -= _slowAmount;
            controller.StartCoroutine(RestoreSpeed(_slowTime, _slowAmount, controller));
        }
        return true;
    }

    public IEnumerator RestoreSpeed(float time, float slowAmount, TankController controller)
    {
        yield return new WaitForSeconds(time);
        controller.MoveSpeed += slowAmount;
        RestoreSpeedFeedback();
    }

    public void RestoreSpeedFeedback()
    {
        // Audio
        if (_restoreSpeedSound != null)
        {
            AudioHelper.PlayClip2D(_restoreSpeedSound, 1f);
        }
    }
}