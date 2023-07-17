using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CSV_Reader : MonoBehaviour
{
    [SerializeField] private TextAsset excelData;

    private void Start()
    {
        ReadSCVFile();
    }

    private void Update()
    {
        
    }

    private void ReadSCVFile()
    {
        //string[] allExcelData = excelData.text.Split(new string[] { ";", "\n" }, StringSplitOptions.None);
        string[] allExcelData = excelData.text.Split("\n", StringSplitOptions.None);
        //Debug.Log(allExcelData[allExcelData.Length - 2]); // ostatni index allExcelData.Length - 2
        Debug.Log(allExcelData.Length); // ostatni index allExcelData.Length - 2
        int tableSize = allExcelData.Length / 4 - 1; // dzielimy przez 4 bo cztery kolumny w excel
        //Debug.Log(tableSize);
        for (int i = 0; i < tableSize; i++)
        {
            //Debug.Log(allExcelData[4 * (i + 1)]); // kolumna pierwsza, dlatego za nawiasem okr¹g³ym nic nie ma, mozna dodaæ 0
            //Debug.Log(allExcelData[4 * (i + 1) + 2]); // + 2 bo trzecia kolumna, iterujemy je od 0
        }
    }
}
