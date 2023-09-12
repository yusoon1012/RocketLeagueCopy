using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Rewired.ComponentControls.Data;

public class CSVReader_Choi : MonoBehaviour
{
    #region �̱��� ����
    private static CSVReader_Choi m_instance; // �̱����� �Ҵ�� static ����
    public static CSVReader_Choi instance
    {
        get
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ CSVReader ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<CSVReader_Choi>();
            }

            // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }
    #endregion

    public const char DELIMITER = ','; // CSV ���Ͽ��� ����ϴ� ������ (�⺻���� �޸�)

    // csv ������ ������ ��� ���� �����Ͽ� ������ ��ųʸ�
    // ���� Ű ���� �ǰ�, ���� Ű �� ������ ���� �ȴ�.
    public Dictionary<string, List<string>> dataDictionary;

    // CSV ������ �д� �Լ�
    // csvFileName�� "CSVFiles"�� ���� Resources �ȿ� �ִ� ���丮���
    // WheelList.csv"�� ���� csv ������ �̸��� �Է��Ѵ�.
    public Dictionary<string, List<string>> ReadCSVFile(string csvFileName)
    {
        dataDictionary = new Dictionary<string, List<string>>();
        TextAsset filePath = Resources.Load<TextAsset>(csvFileName);
        // ����׿� ����
        bool isCSVReadSuccessful = false;
        if (filePath != null)
        {
            {
                string[] lines = filePath.text.Split('\n'); // �� �ٲ����� �� ������ ���� �߰�

                // lines�� ���̰� 1 �̻��� ���
                if (lines.Length > 0)
                {
                    string[] headers = lines[0].Split(DELIMITER); // ���ڿ��� ',' �������� �ڸ�

                    foreach (string header in headers)
                    {
                        dataDictionary.Add(header, new List<string>()); // dataDictionary�� �� �̸��� Ű ������ ����Ʈ �߰�
                    }

                    // ù��° ��[0]�� ����� ����ϰ� �� ��°[1] ���� ������ ������ ����ϱ� ����
                    // index�� 1 ���� ����
                    for (int i = 1; i < lines.Length; i++)
                    {
                        string line = lines[i];
                        string[] values = line.Split(DELIMITER);

                        for (int j = 0; j < values.Length; j++) 
                        {
                            // ��� ����Ʈ�� �� �߰�
                            dataDictionary[headers[j]].Add(values[j]);
                        }
                    }
                }
            }

            isCSVReadSuccessful = true;
        }

        Debug.Log($"CSV ���� ã�� : {isCSVReadSuccessful}");
        return dataDictionary;
    }

    // ��ȯ�� csv ������ ������ ����� ��ųʸ� ������ ���� ����ϴ� �Լ�.
    // �Ű������� csv ���Ͽ��� ��ȯ�� ��ųʸ��� �־�� �Ѵ�.
    // "��:��1,��2,��3"�� ���� ��µȴ�.
    // �Ű� ������ ���� ��ųʸ��� ������ <string, List<string>> �̾�� �Ѵ�.
    public void PrintData(Dictionary<string, List<string>> dictionary)
    {
        // ��ųʸ��� �� �׸��� ���
        foreach (KeyValuePair<string, List<string>> entry in dictionary)
        {
            string category = entry.Key;
            List<string> values = entry.Value;

            Debug.Log(category + ": " + string.Join(", ", values));
        }
    }

}