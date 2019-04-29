using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ShopState
{
    /// <summary>
    /// The number of popo available healing item available
    /// </summary>
    private readonly int[] healingItemCounts = new int[HealingItemStats.MaxLevel];

    public ShopState()
    {
        for (int i = 0; i < HealingItemStats.MaxLevel; i++)
        {
            healingItemCounts[i] = HealingItemStats.GetAvailableCount(i + 1);
        }
    }

    public void ConsumeHealingItem(int level)
    {
        healingItemCounts[level - 1]--;
    }

    public bool IsHealingItemAvailable(int level)
    {
        return healingItemCounts[level - 1] > 0;
    }

    public int GetHealingItemCount(int level)
    {
        return healingItemCounts[level - 1];
    }
}
