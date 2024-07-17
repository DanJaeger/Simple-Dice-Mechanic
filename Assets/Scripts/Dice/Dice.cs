using UnityEngine.Events;
using UnityEngine;
using System.Threading.Tasks;

[RequireComponent(typeof(Rigidbody))]
public class Dice : MonoBehaviour
{
    public Transform[] DiceFaces;
    public Rigidbody RB;

    private int _diceIndex = -1; //To use if we use multiple dices 

    private bool _hasStoppedRolling;
    private bool _isDelayFinished;

    public static UnityAction<int, int> OnDiceResult; //<Result of the dice, index>

    private void Awake()
    {
        if(RB == null)
        {
            RB = GetComponent<Rigidbody>();
            if (RB == null)
            {
                Debug.LogError("Component RB NOT set on " + this.name);
            }
        }
    }
    private void Update()
    {
        if (!_isDelayFinished) return;

        if (!_hasStoppedRolling && RB.velocity.sqrMagnitude == 0f)
        {
            _hasStoppedRolling = true;
            GetNumberOnTopFace();
        }
    }
    [ContextMenu("Get Top Face")] //To test inspector
    private int GetNumberOnTopFace()
    {
        if (DiceFaces == null) return 0;

        var topFaceValue = 0;
        var lastYPosition = DiceFaces[0].position.y;

        for (int i = 0; i < DiceFaces.Length; i++)
        {
            if (DiceFaces[i].position.y > lastYPosition)
            {
                lastYPosition = DiceFaces[i].position.y;
                topFaceValue = i; 
            }
        }

        Debug.Log($"Dice Result: {topFaceValue + 1} ");

        OnDiceResult?.Invoke(_diceIndex, topFaceValue + 1);//+ 1 because face 1 is on position 0, face 2 on position 1, etc...

        return topFaceValue;
    }

    public void RollDice(float throwForce, float rollForce, int i)
    {
        _diceIndex = i;
        float randomVariance = Random.Range(-1f, 1f);
        RB.AddForce(transform.forward * (throwForce + randomVariance), ForceMode.Impulse);

        var randX = Random.Range(0f, 1f);
        var randY = Random.Range(0f, 1f);
        var randZ = Random.Range(0f, 1f);

        RB.AddTorque(new Vector3(randX, randY, randZ) * (rollForce + randomVariance), ForceMode.Impulse);

        DelayResult();
    }

    private async void DelayResult()
    {
        await Task.Delay(1000);
        _isDelayFinished = true;
    }
}
