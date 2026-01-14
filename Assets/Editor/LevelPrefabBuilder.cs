using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelPrefabBuilder : EditorWindow
{
    public static GameObject levelPrefab;
    public static GameObject rowPencilPrefab;

    public static int count = 10;        // số lượng
    public static float spacing = 1.5f;  // khoảng cách
    public static int minPencil = 1;
    public static int maxPencil = 8;

    public static string rowsRootName = "";
    public static Transform rowsRootInPrefab;
    public static bool clearExisting = true;

    public static void BuildIntoPrefab()
    {
        string levelPath = AssetDatabase.GetAssetPath(levelPrefab);
        if (string.IsNullOrEmpty(levelPath) || !levelPath.EndsWith(".prefab"))
        {
            Debug.LogError("Level Prefab không hợp lệ.");
            return;
        }

        GameObject levelRoot = PrefabUtility.LoadPrefabContents(levelPath);

        // Xác định parent để spawn (mặc định là root prefab)
        Transform parent = levelRoot.transform;

        // Nếu bạn muốn dùng 1 object con làm root (ví dụ 'RowsRoot'),
        // thì có thể tìm theo tên thay vì kéo thả rowsRootInPrefab:
        // var found = levelRoot.transform.Find("RowsRoot"); if (found != null) parent = found;

        // Nếu bạn muốn kéo-thả rowsRootInPrefab từ scene/inspector thì nó sẽ không cùng instance với prefab contents,
        // nên cách “kéo thả Transform” chỉ ổn khi bạn đổi sang “tìm theo tên”.
        // Vì vậy mình khuyên dùng Find("RowsRoot") như comment ở trên.

        if (clearExisting)
        {
            for (int i = parent.childCount - 1; i >= 0; i--)
                Object.DestroyImmediate(parent.GetChild(i).gameObject);
        }

        List<int> colors = new List<int>();
        int l = 0;
        while (colors.Count < count && l < 1000)
        {
            l++;
            int r = Random.Range(0, count);
            int p = 0;
            while (colors.Contains(r))
            {
                p++;
                r = Random.Range(0, count);
                if (p > 1000)
                {
                    Debug.LogError("không tạo được màu");
                    break;
                }
            }
            colors.Add(r);
        }
        LevelColorPencil lv = levelRoot.GetComponent<LevelColorPencil>();
        lv.rowPencils.Clear();
        for (int i = 0; i < count; i++)
        {
            GameObject row = (GameObject)PrefabUtility.InstantiatePrefab(rowPencilPrefab);
            row.transform.SetParent(parent, worldPositionStays: false);
            row.transform.localPosition = new Vector3(0f, i * spacing, 0f);
            row.transform.localRotation = Quaternion.identity;
            row.transform.localScale = Vector3.one;
            RowPencil rp = row.GetComponent<RowPencil>();
            rp.nRow = i;
            rp.colorType = (ColorType)colors[i];
            rp.numberBox = Random.Range(minPencil, maxPencil + 1);

            row.name = $"RowPencil_{i}";
            lv.rowPencils.Add(rp);
        }

        PrefabUtility.SaveAsPrefabAsset(levelRoot, levelPath);
        PrefabUtility.UnloadPrefabContents(levelRoot);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Done: Build {count} RowPencil vào {levelPath}");
    }


    public void Build()
    {
        // 1) Bạn chọn Level prefab trong Project trước
        var levelPrefab = Selection.activeObject as GameObject;
        if (levelPrefab == null)
        {
            Debug.LogError("Hãy chọn prefab Level trong Project trước.");
            return;
        }

        // 2) Tìm đường dẫn prefab Level
        string levelPath = AssetDatabase.GetAssetPath(levelPrefab);
        if (string.IsNullOrEmpty(levelPath) || !levelPath.EndsWith(".prefab"))
        {
            Debug.LogError("Object đang chọn không phải prefab (.prefab).");
            return;
        }

        // 3) Bạn cần chỉ định RowPencil prefab (kéo thả bằng cách: đổi sang cách dùng Serialized/Window,
        //    hoặc nhanh gọn: dùng ObjectField qua popup, nhưng ở đây demo theo đường dẫn)
        //    => Cách đơn giản: sửa đúng path RowPencil của bạn:
        string rowPencilPath = "Assets/_GameColorPencil/Prefabs/RowPencil.prefab"; // <-- ĐỔI PATH
        var rowPencilPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(rowPencilPath);
        if (rowPencilPrefab == null)
        {
            Debug.LogError("Không load được RowPencil prefab. Kiểm tra lại rowPencilPath.");
            return;
        }

        // 4) Load nội dung prefab Level để sửa trực tiếp asset
        GameObject levelRoot = PrefabUtility.LoadPrefabContents(levelPath);

        // (Tuỳ chọn) Xoá con cũ (để build lại cho sạch)
        // Nếu muốn chỉ xoá những object RowPencil thôi, bạn có thể filter theo name/tag/component
        for (int i = levelRoot.transform.childCount - 1; i >= 0; i--)
        {
            Object.DestroyImmediate(levelRoot.transform.GetChild(i).gameObject);
        }

        List<int> colors = new List<int>();
        int l = 0;
        while (colors.Count < count && l < 1000)
        {
            l++;
            int r = Random.Range(0, count);
            int p = 0;
            while (colors.Contains(r)){
                p++;
                r = Random.Range(0, count);
                if (p > 1000)
                {
                    Debug.LogError("không tạo được màu");
                    break;
                }
            }
            colors.Add(r);
        }

        // 5) Tạo RowPencil làm con của Level trong prefab asset
        for (int i = 0; i < count; i++)
        {
            GameObject row = (GameObject)PrefabUtility.InstantiatePrefab(rowPencilPrefab);
            row.transform.SetParent(levelRoot.transform, worldPositionStays: false);
            row.transform.localPosition = new Vector3(0f, i * spacing, 0f);
            row.transform.localRotation = Quaternion.identity;
            row.transform.localScale = Vector3.one;
            RowPencil rp = row.GetComponent<RowPencil>();
            rp.nRow = i;
            rp.colorType = (ColorType)colors[i];
            rp.numberBox = Random.Range(minPencil, maxPencil + 1);

            row.name = $"RowPencil_{i}";
        }

        // 6) Save lại thành prefab asset
        PrefabUtility.SaveAsPrefabAsset(levelRoot, levelPath);
        PrefabUtility.UnloadPrefabContents(levelRoot);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Đã build {count} RowPencil vào prefab Level: {levelPath}");
    }
}
