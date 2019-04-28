using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct EquipmentInfo
{
    public int swordLevel;
    public int armorLevel;
    public int sandalLevel;
    public int capeLevel;

    public float SwordDamage { get => EquipmentStats.GetDamage(swordLevel); }
    public float Armor { get => EquipmentStats.GetArmor(armorLevel); }
    public float SpeedBonus { get => EquipmentStats.GetSandalSpeedBonus(sandalLevel) + EquipmentStats.GetCapeSpeedBonus(capeLevel); }
}

public static class EquipmentStats
{
    public static float GetDamage(int sword)
    {
        switch (sword)
        {
            case 0: return 1;
            case 1: return 2;
            case 2: return 3;
            case 3: return 4;
            case 4: return 5;

            default: return 0;
        }
    }

    public static int GetSwordPrice(int sword)
    {
        switch (sword)
        {
            case 1: return 10;
            case 2: return 20;
            case 3: return 30;
            case 4: return 40;

            default: return 0;
        }
    }


    public static float GetArmor(int armor)
    {
        switch (armor)
        {
            case 0: return 1.0f;
            case 1: return 1.25f;
            case 2: return 1.5f;
            case 3: return 1.75f;
            case 4: return 2.0f;  // Damage divisé par 2

            default: return 0;
        }
    }

    public static int GetArmorPrice(int armor)
    {
        switch (armor)
        {
            case 1: return 10;
            case 2: return 20;
            case 3: return 30;
            case 4: return 40;

            default: return 0;
        }
    }


    public static float GetSandalSpeedBonus(int sandal)
    {
        switch (sandal)
        {
            case 1: return 0.25f; // +25% de speed
            case 2: return 0.5f;

            default: return 0;
        }
    }

    public static int GetSandalPrice(int sandal)
    {
        switch (sandal)
        {
            case 1: return 10;
            case 2: return 30;

            default: return 0;
        }
    }


    public static float GetCapeSpeedBonus(int cape)
    {
        switch (cape)
        {
            case 1: return 0.25f;
            case 2: return 0.5f;

            default: return 0;
        }
    }

    public static int GetCapePrice(int cape)
    {
        switch (cape)
        {
            case 1: return 20;
            case 2: return 40;

            default: return 0;
        }
    }
}