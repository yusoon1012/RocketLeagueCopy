
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CustomizingManager_Choi : MonoBehaviour
{
    #region �̱��� ����
    private static CustomizingManager_Choi m_instance; // �̱����� �Ҵ�� static ����
    public static CustomizingManager_Choi instance
    {
        get
        {
            // ���� �̱��� ������Ʈ�� �Ҵ��� ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ ������Ʈ�� ã�� �Ҵ�
                m_instance  = FindObjectOfType<CustomizingManager_Choi>();
            }
            
            // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }
    #endregion

    private delegate void CustomizingFunc(); // ��������Ʈ ����

    [Header("CSVFileReader")]
    private string defaultDirectory = "CSVFiles/"; // �⺻ ���丮 ���
    private string[] csvFileList =
    {
        "CarFrameList", "ColorList", "FlagList", "MarkList", 
        "WheelList_FL", "WheelList_FR", "WheelList_BL", "WheelList_BR"
    }; // CSV ���ϸ� ����Ʈ
    private string[] categoryList =
    {
        "CarFrames", "Colors", "Flags", "Marks",
        "Wheels_FL", "Wheels_FR", "Wheels_BL", "Wheels_BR"
    };
    private Dictionary<string, List<string>> temp_DataDictionary; // CSV ���� �ӽ� ����� ����
    private Dictionary<string, Dictionary<string, List<string>>> dataDictionary; // CSV ���� �������� ����

    [Header("Delegate")]
    private static Dictionary<string, CustomizingFunc> customizingFuncs = 
        new Dictionary<string, CustomizingFunc>(); // Ű ������ �Լ��� �����ϱ� ���� ��ųʸ� ����

    [Header("Parents")]
    //public GameObject carFrames; // ī ������ �θ�
    //public GameObject colors; // �÷� �θ�
    //public GameObject flags; // �÷��� �θ�
    //public GameObject marks; // ��ũ �θ�
    //public GameObject[] wheels; // �� �θ�(�ν����Ϳ� �� �θ� 4���� �߰��ؾ� �Ѵ� csvFileList ������� �� �߰�)
    private List<GameObject> parents; // �θ���� �����ϴ� ����Ʈ
    
    private void Awake()
    {
        Debug.Log("����");
        // csvFileList�� �ִ� ��� CSV ���� �ε�
        ReadCSVFileAndSave();

        // parents ����Ʈ�� GameObject �θ���� �߰�
        InputParents();

        // ������Ʈ Ǯ���� ���� ������Ʈ ����
        CreateObjectPools();
    }

    // csvFileList�� �ִ� ��� CSV ������ �о dataDictionary�� �����ϴ� �Լ�
    private void ReadCSVFileAndSave()
    {
        // ������ ��ųʸ� �ʱ�ȭ
        dataDictionary = new Dictionary<string, Dictionary<string, List<string>>>();
        // csvFileList�� �ִ� ��� CSV ���ϵ��� dataDictionary�� ����
        for (int i = 0; i < csvFileList.Length; i++)
        {
            temp_DataDictionary = CSVReader_Choi.instance.ReadCSVFile(defaultDirectory + csvFileList[i]);
            dataDictionary.Add(csvFileList[i], temp_DataDictionary);
            Debug.Log($"Ű �� : {csvFileList[i]}");
            Debug.Log($"��� : {dataDictionary[csvFileList[i]]["Info"]}");
            Debug.Log($"���� : {dataDictionary.Count}");

        }
    }
    
    // Parents ����Ʈ�� �θ���� �߰��ϴ� �Լ�
    private void InputParents()
    {
        // ����Ʈ �ʱ�ȭ
        parents = new List<GameObject>();
        // �ӽ� ���� ����
        GameObject temp_Obj = new GameObject();
        // �� ī�װ��� �ִ� �θ� ������Ʈ���� ã�� parents�� �߰�
        for (int i = 0; i < categoryList.Length; i++)
        {
            temp_Obj = GameObject.Find(categoryList[i]);
            if (temp_Obj != null) 
            { 
                parents.Add(temp_Obj);
                Debug.Log($"�θ� �߰� : {temp_Obj.name}");
            }
        }
    }

    // ������Ʈ Ǯ���� ���� ������Ʈ ���� �Լ�
    private void CreateObjectPools()
    {
        // �ӽ� ���� ����
        GameObject temp_parent = new GameObject();
        string temp_Key = "";

        // CSV ���� ���� ��ŭ for�� �ݺ�
        for (int i = 0; i < csvFileList.Length; i++)
        {
            temp_parent = parents[i];
            temp_Key = csvFileList[i];
            // dataDictionary[Ű ��]�� �ִ� ������ �������� ������Ʈ ����
            for (int j = 0; j < dataDictionary[temp_Key].Count; j++)
            {
                // ���� ���� �ν��Ͻ� ���� �Լ� ȣ��
                CreateInstantiate(j, temp_Key, temp_parent);
            }
        }
    }

    // ������Ʈ ��� �Լ�
    private void ToggleObject(GameObject previousObj, GameObject currentObj)
    {
        previousObj.SetActive(false); // ���� ������Ʈ ��Ȱ��ȭ
        currentObj.SetActive(true); // ���� ������Ʈ Ȱ��ȭ
    }


    // ��������Ʈ ȣ�� �Լ�
    private void CallDelegateFunc(string funcName)
    {
        // ��������Ʈ �Լ� �ȿ� �ش��ϴ� �Լ����� �ִ��� Ȯ��
        if (customizingFuncs.ContainsKey(funcName))
        {
            CustomizingFunc func = customizingFuncs[funcName];
            func(); // �Լ� ȣ��
        }
    }

    // �θ� �����ϴ� �Լ�
    private void SetParent(GameObject parent, GameObject child)
    {
        Transform parentTransform = parent.transform;
        Transform childTransform = child.transform;

        // �θ� ����
        childTransform.SetParent(parentTransform);
    }

    // ���� ���� ������Ʈ Ǯ���� �ν��Ͻ� ���� �Լ�
    private void CreateInstantiate(int index, string category, GameObject parent)
    {
        // ������ �ν��Ͻ� ������Ʈ ����
        GameObject prefab = GetPrefab(index, category);
        GameObject obj = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation, parent.transform);

        // ������Ʈ ��Ȱ��ȭ
        obj.SetActive(false);
    }

    // ���ϴ� ī�װ��� �������� ��ȯ�ϴ� �Լ�
    private GameObject GetPrefab(int index, string category)
    {
        string temp_Name = "";
        GameObject temp_Prefab = new GameObject();

        // dataDictionary�� �����ϱ� ���� foreach���� ���
        foreach (KeyValuePair<string, List<string>> value in dataDictionary[category])
        {
            // Key ���� PrefabName�� ���
            if (value.Key == "PrefabName")
            {
                temp_Name = dataDictionary[category][value.Key][index]; // dataDictionary �ȿ� �ִ� ���� ȣ��
                temp_Prefab = Resources.Load<GameObject>(temp_Name); // ������ �ҷ��� ������ ��ġ�ϴ� �̸��� prefab�� ȣ��
                Debug.Log($"Ű��:{value.Key}");
                Debug.Log($"��: {dataDictionary[category][value.Key][index]}");

                // �� ���� �����ϰ� ����
                break;
            }

        }

        // ������ ��ȯ
        return temp_Prefab;
    }

}
