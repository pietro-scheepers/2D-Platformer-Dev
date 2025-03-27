using UnityEngine;

public class EnemyTag : MonoBehaviour
{
    [SerializeField] private string enemyName;

    public string GetEnemyName()
    {
        return enemyName;
    }
}