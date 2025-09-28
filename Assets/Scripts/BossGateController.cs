using UnityEngine;

public class BossGateController : MonoBehaviour
{
    [Header("Who is the boss?")]
    [SerializeField] Enemy boss;                 // drag your bear Enemy here

    [Header("Gate to show when boss dies")]
    [SerializeField] GameObject gatePrefab;      // prefab with LevitationGate + trigger collider
    [SerializeField] Transform spawnPoint;       // where to spawn the gate (optional)
    [SerializeField] GameObject preplacedGate;   // OR enable an already placed gate in scene

    bool opened;

    void Update()
    {
        if (opened) return;

        // boss removed OR health <= 0
        if (boss == null || boss.health <= 0)
        {
            opened = true;
            OpenGate();
        }
    }

    void OpenGate()
    {
        if (gatePrefab != null)
        {
            Vector3 pos = spawnPoint ? spawnPoint.position : transform.position;
            Instantiate(gatePrefab, pos, Quaternion.identity);
        }
        else if (preplacedGate != null)
        {
            preplacedGate.SetActive(true);
        }
        else
        {
            Debug.LogWarning("[BossGateController] No gatePrefab or preplacedGate assigned.");
        }
    }
}