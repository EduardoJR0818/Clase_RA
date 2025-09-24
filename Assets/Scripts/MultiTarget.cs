using UnityEngine;

public class MultiTarget : MonoBehaviour
{
    [SerializeField] private GameObject startModel;
    private int modelsCount;
    private int currentModel;

    void Start()
    {
        modelsCount = transform.childCount;
        currentModel = startModel.transform.GetSiblingIndex();


        for (int i = 0; i < modelsCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == currentModel);
        }
    }

    public void ShowRandomModel()
    {
        transform.GetChild(currentModel).gameObject.SetActive(false);
        int newIndex;
        do
        {
            newIndex = Random.Range(0, modelsCount);
        }
        while (newIndex == currentModel);

        transform.GetChild(newIndex).gameObject.SetActive(true);

        currentModel = newIndex;
    }
}
