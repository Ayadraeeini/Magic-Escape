//Eyad Al Raeeini - 02/17/2026
//lever puzzle manager 

using UnityEngine;

public class LeverPuzzleManager : MonoBehaviour
{
    public LeverSequenceInteractable[] levers;
    public int[] correctOrder;
    public DoorController gate;

    private int currentIndex = 0;

    public void LeverPulled(int leverID)
    {
        Debug.Log("Expected: " + correctOrder[currentIndex] + " Got: " + leverID);

        if (correctOrder[currentIndex] == leverID)
        {
            currentIndex++;

            if (currentIndex >= correctOrder.Length)
            {
                Debug.Log("Correct sequence!");

                if (gate != null)
                    gate.OpenDoor();
            }
        }
        else
        {
            Debug.Log("Wrong order - RESET");

            ResetPuzzle();
        }
    }

    void ResetPuzzle()
    {
        currentIndex = 0;

        for (int i = 0; i < levers.Length; i++)
        {
            if (levers[i] != null)
                levers[i].ResetLever();
        }
    }
}