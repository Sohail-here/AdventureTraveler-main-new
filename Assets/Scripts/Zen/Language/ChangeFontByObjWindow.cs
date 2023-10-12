#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ChangeFontByObjWindow : EditorWindow
{
    [MenuItem("MyTools/Change Font/Change Font By Select")]
    public static void ShowWindow()
    {
        // Pop up window
        EditorWindow.GetWindow(typeof(ChangeFontByObjWindow), false, "Change Font Window");
    }

    string showNotify;

    //Target font, class
    Font targetFont;

    List<GameObject> selectTextPrefabList = new List<GameObject>();
    GameObject newAddObj;

    void OnEnable()
    {
        GameObject[] tmpSelections = Selection.gameObjects;
        selectTextPrefabList.Clear();
        selectTextPrefabList.AddRange(tmpSelections);

        newAddObj = null;
        showNotify = "";
    }

    Vector2 scrollPosition = Vector2.zero;
    void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(position.width), GUILayout.Height(position.height));

        GUILayout.Space(10);
        GUILayout.Label("U Can Select GameObjects By Mouse In Hierachy And Project Then Open This Window");

        // Select target font
        GUILayout.Space(10);
        GUILayout.Label("Target Font");
        targetFont = (Font)EditorGUILayout.ObjectField(targetFont, typeof(Font), true);
        // Select target fontSize
        //GUILayout.Label("Target Font Size (if value < 0, will not change font size)");
        //targetFontSize = EditorGUILayout.IntField(targetFontSize);
        // Select target fontStyle
        //GUILayout.Label("Target FontStyle");
        //targetFontStyle = (FontStyle)EditorGUILayout.EnumPopup(targetFontStyle);

        // Selected gameobject list
        GUILayout.Space(10);
        GUILayout.Label("Selection");
        if (selectTextPrefabList != null && selectTextPrefabList.Count > 0)
        {
            for (int i = 0; i < selectTextPrefabList.Count; i++)
            {
                selectTextPrefabList[i] = EditorGUILayout.ObjectField(selectTextPrefabList[i], typeof(GameObject), true) as GameObject;
            }
        }
        else
            GUILayout.Label("None");

        // Add to new gameobject list
        GUILayout.Space(10);
        GUILayout.Label("Add To Selection");
        newAddObj = EditorGUILayout.ObjectField(newAddObj, typeof(GameObject), true) as GameObject;
        if (newAddObj != null && !selectTextPrefabList.Contains(newAddObj))
        {
            if (GUILayout.Button("Add Obj"))
            {
                for (int i = 0; i < selectTextPrefabList.Count; i++)
                {
                    if (selectTextPrefabList[i] == null)
                    {
                        selectTextPrefabList[i] = newAddObj;
                        showNotify = "Add Obj To Select List";
                        return;
                    }
                }
                selectTextPrefabList.Add(newAddObj);
                showNotify = "Add Obj To Select List";
            }
        }

        GUILayout.Space(10);
        if (GUILayout.Button("Change Selection Font"))
        {
            int count;
            ChangeFontTool.ChangeTextFontRecursion(selectTextPrefabList, targetFont, out count);
            ChangeFontTool.SaveAllPrefab(selectTextPrefabList);
            if (count < 0)
                showNotify = "Change text font failed";
            else
                showNotify = "Change text font success : " + count;
        }

        GUILayout.Space(10);
        // Close window
        if (GUILayout.Button("Close"))
        {
            this.Close();
        }

        GUILayout.Space(10);
        GUILayout.Label(showNotify);

        GUILayout.EndScrollView();
        //this.Repaint();
    }

    void OnDisable()
    {
        selectTextPrefabList.Clear();
        newAddObj = null;

        showNotify = "";
    }
}

public class ChangeFontByFontWindow : EditorWindow
{
    [MenuItem("MyTools/Change Font/Change Font By Font")]
    public static void ShowWindow()
    {
        // Pop up window
        EditorWindow.GetWindow(typeof(ChangeFontByFontWindow), false, "Change Font Window");
    }

    string showNotify;

    Font targetFont;
    Font searchFont;

    int uiPrefabCount;
    int allTextCount;

    List<GameObject> allPrefabRootList;
    List<GameObject> allPrefabTextList;
    List<GameObject> searchTextList = new List<GameObject>();

    private void OnEnable()
    {
        searchTextList.Clear();
        ChangeFontTool.GetAllTextPrefabs(out allPrefabRootList, out allPrefabTextList);
        uiPrefabCount = allPrefabRootList.Count;
        allTextCount = allPrefabTextList.Count;
        showNotify = "Find text component " + allTextCount + " from ui prefab " + uiPrefabCount;
    }

    Vector2 scrollPosition = Vector2.zero;
    private void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(position.width), GUILayout.Height(position.height));

        GUILayout.Space(10);
        GUILayout.Label("Find text component " + allTextCount + " from ui prefab " + uiPrefabCount);

        // Select search font
        GUILayout.Space(10);
        GUILayout.Label("Search Font");
        searchFont = (Font)EditorGUILayout.ObjectField(searchFont, typeof(Font), true);

        // Search font
        if (GUILayout.Button("Search"))
        {
            searchTextList.Clear();

            for (int i = 0; i < allPrefabTextList.Count; i++)
            {
                Text tempText = allPrefabTextList[i].GetComponent<Text>();
                if (tempText.font == searchFont)
                    searchTextList.Add(allPrefabTextList[i]);
            }
        }

        if (searchTextList.Count > 0)
        {
            for (int i = 0; i < searchTextList.Count; i++)
            {
                searchTextList[i] = EditorGUILayout.ObjectField(searchTextList[i].gameObject, typeof(GameObject), true) as GameObject;
            }
        }
        else
            GUILayout.Label("None");

        // Select target font
        GUILayout.Space(10);
        GUILayout.Label("Target Font");
        targetFont = (Font)EditorGUILayout.ObjectField(targetFont, typeof(Font), true);
        // Select target fontSize
        //GUILayout.Label("Target Font Size (if value < 0, will not change font size)");
        //targetFontSize = EditorGUILayout.IntField(targetFontSize);
        // Select target fontStyle
        //GUILayout.Label("Target FontStyle");
        //targetFontStyle = (FontStyle)EditorGUILayout.EnumPopup(targetFontStyle);

        GUILayout.Space(20);
        if (GUILayout.Button("Change Selection Font"))
        {
            int count;
            ChangeFontTool.ChangeTextFont(searchTextList, targetFont, out count);
            ChangeFontTool.SaveAllPrefab(allPrefabRootList);
            if (count < 0)
                showNotify = "Change text font failed";
            else
                showNotify = "Change text font success : " + count;
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Change All Prefab Font"))
        {
            int count;
            ChangeFontTool.ChangeTextFont(allPrefabTextList, targetFont, out count);
            ChangeFontTool.SaveAllPrefab(allPrefabRootList);
            if (count < 0)
                showNotify = "Change text font failed";
            else
                showNotify = "Change text font success : " + count;
        }

        GUILayout.Space(20);
        if (GUILayout.Button("Refresh"))
        {
            searchTextList.Clear();
            ChangeFontTool.GetAllTextPrefabs(out allPrefabRootList, out allPrefabTextList);
            uiPrefabCount = allPrefabRootList.Count;
            allTextCount = allPrefabTextList.Count;
            showNotify = "Find text component " + allTextCount + " from ui prefab " + uiPrefabCount;
        }

        GUILayout.Space(10);
        if (GUILayout.Button("Close"))
        {
            this.Close();
        }

        GUILayout.Space(10);
        GUILayout.Label(showNotify);

        GUILayout.EndScrollView();
        //this.Repaint();
    }

    private void OnDisable()
    {
        allPrefabRootList.Clear();
        allPrefabTextList.Clear();
        searchTextList.Clear();

        showNotify = "";
    }

}

public class ChangeFontTool
{
    private static string[] uiPrefabPaths = new string[] { "Assets/Resources/UIPrefab" };

    public static void GetAllTextPrefabs(out List<GameObject> allPrefabRootList, out List<GameObject> allPrefabTextList)
    {
        allPrefabRootList = new List<GameObject>();
        allPrefabTextList = new List<GameObject>();

        // Get all text component in Project
        // Get all prefabs's GUID in folder Asset
        string[] ids = AssetDatabase.FindAssets("t:Prefab", uiPrefabPaths);
        string tmpPath;
        for (int i = 0; i < ids.Length; i++)
        {
            // Get path according to GUID
            tmpPath = AssetDatabase.GUIDToAssetPath(ids[i]);
            if (!string.IsNullOrEmpty(tmpPath))
            {
                // Get prefab according to path(GameObject)
                GameObject prefab = AssetDatabase.LoadAssetAtPath(tmpPath, typeof(GameObject)) as GameObject;
                if (prefab != null)
                {
                    allPrefabRootList.Add(prefab);

                    GetAllTextFromChildren(prefab, ref allPrefabTextList);
                }
            }
        }
    }

    private static void GetAllTextFromChildren(GameObject target, ref List<GameObject> textList)
    {
        if (target == null)
            return;

        Text tmpText = target.GetComponent<Text>();
        if (tmpText != null)
            textList.Add(target);

        if (target.transform.childCount > 0)
        {
            for (int i = 0; i < target.transform.childCount; i++)
            {
                GetAllTextFromChildren(target.transform.GetChild(i).gameObject, ref textList);
            }
        }
    }

    public static void ChangeTextFontRecursion(List<GameObject> textPrefabList, Font font, out int successCount)
    {
        if (textPrefabList == null || textPrefabList.Count == 0 || font == null)
        {
            successCount = -1;
            return;
        }
        successCount = 0;
        for (int i = 0; i < textPrefabList.Count; i++)
        {
            if (textPrefabList[i] != null)
            {
                ChangeTextFontRecursion(textPrefabList[i].gameObject, font, ref successCount);
            }
        }
    }

    private static void ChangeTextFontRecursion(GameObject textPrefab, Font font, ref int successCount)
    {
        if (textPrefab == null)
            return;

        Text tempText = textPrefab.GetComponent<Text>();
        if (tempText != null)
        {
            tempText.font = font;
            successCount++;
        }

        if (textPrefab.transform.childCount > 0)
        {
            for (int i = 0; i < textPrefab.transform.childCount; i++)
            {
                ChangeTextFontRecursion(textPrefab.transform.GetChild(i).gameObject, font, ref successCount);
            }
        }
    }

    public static void ChangeTextFont(List<GameObject> textList, Font font, out int successCount)
    {
        if (textList == null || textList.Count == 0 || font == null)
        {
            successCount = -1;
            return;
        }
        successCount = 0;
        for (int i = 0; i < textList.Count; i++)
        {
            if (textList[i] == null)
                continue;
            Text tempText = textList[i].GetComponent<Text>();
            if (tempText == null)
                continue;

            tempText.font = font;

            //tmpText.fontStyle = fontStyle;,
            //if (fontSize >= 0)
            //    tmpText.fontSize = fontSize;

            successCount++;
        }
    }

    public static void SaveAllPrefab(List<GameObject> allPrefabRootList)
    {
        for (int i = 0; i < allPrefabRootList.Count; i++)
        {
            PrefabUtility.SavePrefabAsset(allPrefabRootList[i]);
        }
        AssetDatabase.SaveAssets();
        //EditorUtility.SetDirty(textList[i]);
    }
}

#endif