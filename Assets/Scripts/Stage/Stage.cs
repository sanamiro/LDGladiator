using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class EnemyData
{
    public EnemyController.EnemyType EnemyType;
    public int Count;
}

[Serializable]
public class Wave
{
    public List<EnemyData> Enemies;
}

[CreateAssetMenu(fileName = "Level x")]
public class Stage : ScriptableObject
{
    public List<Wave> Waves = new List<Wave>() { new Wave() };
}
