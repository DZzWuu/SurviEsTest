public struct UpgradeLevelDataSignal
{
    public int Level;
    public int Exp;
    public int MinExp;
    public int MaxExp;

    public UpgradeLevelDataSignal(int level, int exp,int minExp, int maxExp)
    {
        Level = level;
        Exp = exp;
        MinExp = minExp;
        MaxExp = maxExp;
    }
}
