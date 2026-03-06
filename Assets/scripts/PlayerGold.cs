//Eyad Al Raeeini - 02/17/2026
//player gold system
using UnityEngine;
public class PlayerGold : MonoBehaviour
{
    public int startingGold = 100;
    public int maxGold = 9999;
    public int CurrentGold;
    void Awake()
    {
        CurrentGold = startingGold;
    }

    public bool HasEnoughGold(int amount)
    {
        if (CurrentGold >= amount)
            return true;
        else
            return false;
    }

    public bool SpendGold(int amount)
    {
        if (amount <= 0) return false;

        if (!HasEnoughGold(amount))
            return false;

        CurrentGold -= amount;
        return true;
    }

    public void AddGold(int amount)
    {
        if (amount <= 0) return;

        CurrentGold += amount;
        if (CurrentGold > maxGold)
            CurrentGold = maxGold;
    }

    public void SetGold(int amount)
    {
        CurrentGold = amount;
        if (CurrentGold < 0)
            CurrentGold = 0;
        if (CurrentGold > maxGold)
             CurrentGold = maxGold;
    }
}