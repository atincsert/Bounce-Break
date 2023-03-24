using System.Collections;
using UnityEngine;

public class ObstacleVariationSpawner : MonoBehaviour
{
    [SerializeField] private float delayBetweenSections;
    [SerializeField] private GameObject[] variations;
    [SerializeField] private float endZPosOfFirstSection;
    [SerializeField] private int totalNumberOfSections;

    private bool creatingSection;
    private int sectionNumber;

    private void Update()
    {
        if (!GameManager.IsGameRunning)
        {
            return;
        }
        else
        {
            if (!creatingSection)
            {
                creatingSection = true;

                StartCoroutine(GenerateSection());
            }
        }
    }

    private IEnumerator GenerateSection()
    {
        sectionNumber = Random.Range(0, totalNumberOfSections);
        var go = Instantiate(variations[sectionNumber], new Vector3(0, 0, endZPosOfFirstSection), Quaternion.identity);

        if (TryGetPickupInsideSection(go, out Pickup pickup))
        {
            HandlePickupGameObject(pickup);
        }

        endZPosOfFirstSection += 30;
        yield return new WaitForSeconds(delayBetweenSections);
        creatingSection = false;
    }

    private bool TryGetPickupInsideSection(GameObject searchingGameObject, out Pickup pickup)
    {
        pickup = null;

        foreach (Transform child in searchingGameObject.transform)
        {
            if (child.TryGetComponent(out pickup))
            {
                return true;
            }
        }

        return false;
    }

    private void HandlePickupGameObject(Pickup pickup)
    {
        if (FindObjectOfType<HammerMovement>().gameObject.activeInHierarchy)
        {
            pickup.gameObject.SetActive(true);
        }
        else
        {
            pickup.gameObject.SetActive(false);
        }
    }
}