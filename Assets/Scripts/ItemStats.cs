

public enum ItemType
{
    Sword,
    Armor,
    Sandal,
    Cape,
    TheWoodenSword,
    Heal
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

    public static int WoodenSwordPrice = 100;
    public static string WoodenSwordName = "The Ultimate Wooden Sword";
    public static string WoodenSwordDesc = "The Ultimate Wooden Sword";

    public static string GetSwordName(int level)
    {
        return "Sword " + level;
    }

    public static string GetArmorName(int level)
    {
        return "Armor " + level;
    }

    public static string GetSandalName(int level)
    {
        return "Sandal " + level;
    }

    public static string GetCapeName(int level)
    {
        return "Cape " + level;
    }



    public static string GetSwordDesc(int level)
    {
        return "Sword " + level;
    }

    public static string GetArmorDesc(int level)
    {
        return "Armor " + level;
    }

    public static string GetSandalDesc(int level)
    {
        return "Sandal " + level;
    }

    public static string GetCapeDesc(int level)
    {
        return "Cape " + level;
    }
}

public static class HealingItemStats
{
    public static readonly int MaxLevel = 4;

    public static float GetLifeRegeneration(int level)
    {
        switch (level)
        {
            case 1: return 10;
            case 2: return 10;
            case 3: return 20;
            case 4: return 30;

            default: return 0;
        }
    }

    public static int GetPrice(int level)
    {
        switch (level)
        {
            case 1: return 6;
            case 2: return 6;
            case 3: return 11;
            case 4: return 15;

            default: return 0;
        }
    }

    public static string GetName(int level)
    {
        return "Potion " + level;
    }

    public static string GetDesc(int level)
    {
        return "Potion " + level;
    }
}