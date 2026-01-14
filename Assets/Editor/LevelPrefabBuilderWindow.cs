using UnityEngine;
using UnityEditor;

public class LevelPrefabBuilderWindow : EditorWindow
{
    [MenuItem("Tools/Level/Prefab Builder")]
    public static void Open()
    {
        GetWindow<LevelPrefabBuilderWindow>("Level Prefab Builder");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("LEVEL PREFAB BUILDER", EditorStyles.boldLabel);
        EditorGUILayout.Space(6);

        // Object fields
        LevelPrefabBuilder.levelPrefab =
            (GameObject)EditorGUILayout.ObjectField("Level Prefab", LevelPrefabBuilder.levelPrefab, typeof(GameObject), false);

        LevelPrefabBuilder.rowPencilPrefab =
            (GameObject)EditorGUILayout.ObjectField("RowPencil Prefab", LevelPrefabBuilder.rowPencilPrefab, typeof(GameObject), false);

        EditorGUILayout.Space(8);

        // Parameters
        LevelPrefabBuilder.count = EditorGUILayout.IntField("Count", LevelPrefabBuilder.count);
        LevelPrefabBuilder.spacing = EditorGUILayout.FloatField("Spacing", LevelPrefabBuilder.spacing);

        EditorGUILayout.Space(8);

        LevelPrefabBuilder.minPencil = EditorGUILayout.IntField("Min Pencil", LevelPrefabBuilder.minPencil);
        LevelPrefabBuilder.maxPencil = EditorGUILayout.IntField("Max Pencil", LevelPrefabBuilder.maxPencil);

        EditorGUILayout.Space(8);

        LevelPrefabBuilder.rowsRootName = EditorGUILayout.TextField("Rows Root Name", LevelPrefabBuilder.rowsRootName);
        LevelPrefabBuilder.clearExisting = EditorGUILayout.Toggle("Clear Existing", LevelPrefabBuilder.clearExisting);

        EditorGUILayout.Space(12);

        using (new EditorGUI.DisabledScope(LevelPrefabBuilder.levelPrefab == null || LevelPrefabBuilder.rowPencilPrefab == null))
        {
            if (GUILayout.Button("BUILD INTO PREFAB", GUILayout.Height(32)))
            {
                LevelPrefabBuilder.BuildIntoPrefab();
            }
        }

        EditorGUILayout.Space(6);
        EditorGUILayout.HelpBox(
            "Rows Root Name: để trống = spawn vào root của Level.\n" +
            "Nếu bạn muốn spawn vào object con (vd: RowsRoot) thì nhập đúng tên object đó trong prefab Level.",
            MessageType.Info
        );
    }
}