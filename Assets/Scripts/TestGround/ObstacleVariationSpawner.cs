using System.Collections;
using System.Collections.Generic;
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
        Instantiate(variations[sectionNumber], new Vector3(0, 0, endZPosOfFirstSection), Quaternion.identity);
        endZPosOfFirstSection += 40;
        yield return new WaitForSeconds(delayBetweenSections);
        creatingSection = false;
    }
}
