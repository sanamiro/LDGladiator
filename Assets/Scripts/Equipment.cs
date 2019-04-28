using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EquipmentInfo
{
    public int swordLevel;
    public int armorLevel;
    public int sandalLevel;
    public int capeLevel;

    public float SwordDamage { get => EquipmentStats.GetDamage(swordLevel); }
    public float Armor { get => EquipmentStats.GetArmor(armorLevel); }
    public float SpeedBonus { get => EquipmentStats.GetSandalSpeedBonus(sandalLevel) + EquipmentStats.GetCapeSpeedBonus(capeLevel); }
}
