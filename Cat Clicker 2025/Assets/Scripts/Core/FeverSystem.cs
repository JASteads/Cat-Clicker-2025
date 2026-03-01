using System;
using UnityEngine;

public class FeverSystem
{
    const float CHARGE_RATE = 4;
    const float SLOW_DRAIN_TIME = 0.5f; // Should be long enough not to notice during active clicking
    const float SLOW_DRAIN_AMOUNT = 1; // To give the feel that the Fever Bar is volatile even when charged

    [SerializeField] public float charge;
    [SerializeField] public float slowDrainTimeLeft; // To not drain the meter while actively charging
    [SerializeField] public float holdTimeLeft;
    [SerializeField] public bool isFeverTime;
    [SerializeField] public FeverData data;

    public FeverSystem()
    {
        holdTimeLeft = 0;
        slowDrainTimeLeft = 0;
        isFeverTime = false;

        // Test values -- Migrate to GameData later
        data = new FeverData
        {
            maxCharge = 100,
            drainAmount = 75,
            holdTime = 3
        };
    }

    public void AddCharge()
    {
        float newCharge = charge + CHARGE_RATE;
        slowDrainTimeLeft = SLOW_DRAIN_TIME;

        // Charge up to the maximum capacity. Enter Fever Time if maxed while not in Fever Time
        if (newCharge > data.maxCharge)
        {
            charge = data.maxCharge;

            if (!isFeverTime)
            {
                isFeverTime = true;
                EventBus.GoFeverTimeStart();
            }
        }
        else
        {
            charge = newCharge;
        }

        // Reset drain timers
        float halfHoldTime = data.holdTime / 3f;

        if (halfHoldTime > SLOW_DRAIN_TIME)
        {
            holdTimeLeft = isFeverTime ? data.holdTime : halfHoldTime;
        }
        else
        {
            // Apply fallback in case the reset hold timer is somehow shorter than the slow drain timer
            holdTimeLeft = SLOW_DRAIN_TIME;
        }
        slowDrainTimeLeft = SLOW_DRAIN_TIME;
    }

    public void UpdateSystem()
    {
        // Do nothing if Fever Bar is not active
        if (charge == 0) return;

        float newHoldTime = holdTimeLeft - Time.deltaTime;
        float newSlowDrainTime = slowDrainTimeLeft - Time.deltaTime;
        float drainAmount = 0;

        // Update timers
        holdTimeLeft = Mathf.Max(0, newHoldTime);
        slowDrainTimeLeft = Mathf.Max(0, newSlowDrainTime);

        // Slow drain logic
        if (slowDrainTimeLeft == 0 && holdTimeLeft > 0)
        {
            drainAmount = SLOW_DRAIN_AMOUNT;
        }

        // Fast drain logic
        if (holdTimeLeft == 0)
        {
            drainAmount = data.drainAmount;

            // Fever Time ends when hold time expires
            if (isFeverTime)
            {
                isFeverTime = false;
                EventBus.GoFeverTimeEnd();
            }
        }

        if (drainAmount != 0)
        {
            ReduceCharge(drainAmount);
        }
    }

    void ReduceCharge(float amountPerSec)
    {
        float newCharge = charge - (amountPerSec * Time.deltaTime);

        charge = Mathf.Max(0f, newCharge);
    }
}

[Serializable]
public struct FeverData
{
    [SerializeField] public float maxCharge;
    [SerializeField] public float drainAmount;
    [SerializeField] public float holdTime;
}
