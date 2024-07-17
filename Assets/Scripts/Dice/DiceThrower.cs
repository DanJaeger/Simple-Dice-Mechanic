using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DiceThrower : MonoBehaviour
{
    public Dice DiceToThrow;
    public int AmountOfDice = 1; //In case we want to throw more than 1 dice

    public float ThrowForce = 5f;
    public float RollForce = 5f;

    private List<GameObject> _spawnedDice = new List<GameObject>(); //To save dices in case we want to throw more than 1 dice

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) RollDice();

    }

    private async void RollDice()
    {
        if (DiceToThrow == null) return;

        foreach (var dice in _spawnedDice)
        {
            Destroy(dice);
        }

        for (int i = 0; i < AmountOfDice; i++)
        {
            Dice newDice = Instantiate(DiceToThrow, transform.position, transform.rotation);
            _spawnedDice.Add(newDice.gameObject);
            newDice.RollDice(ThrowForce, RollForce, i);
            await Task.Yield();
        }
    }
}
