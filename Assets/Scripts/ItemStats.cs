﻿

using System;

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
            case 0: return 1;// epee de base équipé sur le joueur
            case 1: return 1.5f;//1ere epee du shop - degats
            case 2: return 2.8f;//2eme epee du shop - degats
            case 3: return 4; // pas de 3eme epee
            case 4: return 5;

            default: return 0;
        }
    }

    public static int GetSwordPrice(int sword)
    {
        switch (sword)
        {
            case 1: return 60;//1ere epee du shop - prix
            case 2: return 300;//2eme epee du shop - prix
            case 3: return 30;// pas de 3eme epee
            case 4: return 40;

            default: return 0;
        }
    }


    public static float GetArmor(int armor)
    {
        switch (armor)
        {
            case 0: return 1.0f;// armure de base équipé sur le joueur
            case 1: return 1.5f;//1ere armure du shop - armor
            case 2: return 2.0f;//2eme armure du shop - armor
            case 3: return 1.75f;// pas de 3eme 
            case 4: return 2.0f;  // Damage divisé par 2

            default: return 0;
        }
    }

    public static int GetArmorPrice(int armor)
    {
        switch (armor)
        {
            case 1: return 70;//1ere armure du shop - prix
            case 2: return 240;//2eme armure du shop - prix
            case 3: return 30;// pas de 3eme 
            case 4: return 40;

            default: return 0;
        }
    }


    public static float GetSandalSpeedBonus(int sandal)
    {
        switch (sandal)
        {
            case 1: return 0.10f; // +25% de speed //1ere sandale du shop - vitesse
            case 2: return 0.25f;// +50% de speed //2ere sandale du shop - vitesse

            default: return 0;
        }
    }

    public static int GetSandalPrice(int sandal)
    {
        switch (sandal)
        {
            case 1: return 30;//1ere sandale du shop -prix
            case 2: return 110; //2ere sandale du shop -prix

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

    public static int WoodenSwordPrice = 1000;
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
        return "Sharp as a paper sheet, whit it you will kill faster. Even vegans like swords like that.";
    }

    public static string GetArmorDesc(int level)
    {
        return "It will protect you from enemy's attack. At least a bit more than if you fight completely nude.";
    }

    public static string GetSandalDesc(int level)
    {
        return "A pair of sandal. You should buy it if you consider doing a marathon.";
    }

    public static string GetCapeDesc(int level)
    {
        return "Supergladiator used to wear this cape, but nobody was really sure since he was to fast.";
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
            case 2: return 30;
            case 3: return 50;
            case 4: return 70;

            default: return 0;
        }
    }

    public static int GetPrice(int level)
    {
        switch (level)
        {
            case 1: return 6;
            case 2: return 15;
            case 3: return 30;
            case 4: return 15;

            default: return 0;
        }
    }

    public static int GetAvailableCount(int level)
    {
        switch (level)
        {
            case 1: return 7;
            case 2: return 4;
            case 3: return 2;
            case 4: return 2;

            default: return 0;
        }
    }

    public static string GetName(int level, int availableCount)
    {
        switch (level)
        {
            case 1: return + availableCount + " x " + "Apple";
            case 2: return  + availableCount + " x " + "Chicken";
            case 3: return  + availableCount + " x " + "Potion ";

            default: return "";
        }
    }

    public static string GetDesc(int level/*, int availableCount*/)
    {
        switch (level)
        {
            case 1: return "A apple a day keeps the lion away.";
            case 2: return "Not that much proteins but really tasty.";
            case 3: return "Weird to see that in a realistic world. Who said ludonarative dissonance ? Anyway it's red liquid make you feel like a new born.";

            default: return "";
        }
    }
}