using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Data/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [System.Serializable]
    public class LevelData
    {
        public int Level;
        public int Exp;
    }

    [SerializeField] private LevelData[] m_levelData;

    public LevelData GetLevelData(int level)
    {
        for(int i =0; i < m_levelData.Length; i++)
        {
            if(m_levelData[i].Level == level) { return m_levelData[i]; }
        }
        return null;
    }

}
