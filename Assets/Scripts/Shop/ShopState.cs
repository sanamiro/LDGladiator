using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ShopState
{
    /// <summary>
    /// The number of stages to wait before the healing item is available
    /// </summary>
    private readonly int[] healingItemStates = new int[HealingItemStats.MaxLevel];

    public void ConsumeHealingItem(int level)
    {
        healingItemStates[level - 1] = 2;
    }

    public bool IsHealingItemAvailable(int level)
    {
        return healingItemStates[level - 1] <= 0;
    }

    public void OnStagePassed()
    {
        for (int i = 0; i < HealingItemStats.MaxLevel; i++)
        {
            if (healingItemStates[i] > 0)
                healingItemStates[i]--;
        }
    }
}
