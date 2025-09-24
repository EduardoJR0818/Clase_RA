using UnityEngine;

public class TargetPrueba : MonoBehaviour
{
    [SerializeField] private GameObject startModel;
    private int modelsCount; private int CurrentModel;
    void Start()
    { modelsCount = transform.childCount; CurrentModel = startModel.transform.GetSiblingIndex(); }
    public void ChangeARModel(int Index)
    {
        transform.GetChild(CurrentModel).gameObject.SetActive(false);
        int newIndex = CurrentModel + Index;
        if (newIndex < 0) { newIndex = modelsCount - 1; }
        else if (newIndex > modelsCount - 1) { newIndex = 0; }
        GameObject newModel = transform.GetChild(newIndex).gameObject;
        newModel.SetActive(true); CurrentModel = -newModel.transform.GetSiblingIndex();
    }
}