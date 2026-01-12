//#if UNITY_EDITOR

using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CreateLeveManager : MonoBehaviour
{
    public static CreateLeveManager ins;

    [Header("Property")]
    public List<Iron> irons = new List<Iron>();
    public List<Hole1Iron> hole1Irons = new List<Hole1Iron>();
    public List<Color> colors = new List<Color>();
    public List<Iron> ironPrefabs = new List<Iron>();

    public TMP_InputField inputField_soLayer;
    public TMP_InputField inputField_soIron1Layer;
    public TMP_InputField inputField_soColor;
    public TMP_InputField inputField_level;

    public Camera mainCamera;
    public int ironCount = 0;
    public ColorType colorType;
    public TMP_Dropdown dropdown;

    public Image currentColorImage;

    public List<Iron> ironTemplate;
    public Hole1Iron hole1Prefab;
    public Screw screwPrefab;

    public Transform tfLevel;
    public Transform tfScrew;
    private void Awake()
    {
        ins = this;
    }

    void Start()
    {
        /*for (int i = 0; i < colorAssets.Length; i++)
        {
            dic_colorMaterials[colorAssets[i]._colorType] = colorAssets[i];
        }

        img_hole.color = Color.yellow;
        img_snake.color = Color.white;
        OnChangeColor();*/

    }

    public void Btn_create()
    {
        StartCoroutine(CreateLevel());
    }

    public float sizeBroad = 20f;
    public LayerMask ironLayerMask;

    [Button]
    public void CreateLevelRandom()
    {
        StartCoroutine(CreateLevel());
    }
    public int currentLayer = 0;
    public IEnumerator CreateLevel()
    {
        int soLayer = int.Parse(inputField_soLayer.text);
        int soIron1Layer = int.Parse(inputField_soIron1Layer.text);
        int soColor = int.Parse(inputField_soColor.text);

        irons.Clear();
        hole1Irons.Clear();

        for (int i = 0; i < soLayer; i++)
        {
            currentLayer = i;
            for (int j = 0; j < soIron1Layer; j++)
            {
                int m = -1, n = -1, l = 0;
                do
                {
                    l++;
                    int indexIron = Random.Range(0, ironTemplate.Count);
                    m = Random.Range(0, 9) - 4;
                    n = Random.Range(0, 9) - 4;
                    Iron iron = Instantiate(ironTemplate[indexIron], tfLevel);
                    iron.transform.localPosition = new Vector3((float)m * sizeBroad, (float)n * sizeBroad, 0);
                    iron.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 12) * 30));

                    yield return new WaitForSeconds(0.1f);
                    if (iron.nIronVaCham > 0)
                    {
                        Destroy(iron.gameObject);
                        continue;
                    }
                    iron.id = indexIron;
                    irons.Add(iron);
                    for (int k = 0; k < iron.hole1Irons.Count; k++)
                    {
                        this.hole1Irons.Add(iron.hole1Irons[k]);
                    }
                    break;
                } while (l < 70);
                
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.1f);
        }

        if (hole1Irons.Count % 3 == 1)
        {
            currentLayer = soLayer;
            int m = -1, n = -1, l = 0;
            do
            {
                l++;
                m = Random.Range(0, 9) - 4;
                n = Random.Range(0, 9) - 4;
                Iron iron = Instantiate(ironTemplate[Random.Range(0, ironTemplate.Count)], tfLevel);
                iron.transform.localPosition = new Vector3((float)m * sizeBroad, (float)n * sizeBroad, 0);
                iron.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 12) * 30));

                yield return new WaitForSeconds(0.1f);
                if (iron.nIronVaCham > 0 || iron.hole1Irons.Count != 2)
                {
                    Destroy(iron.gameObject);
                    continue;
                }
                irons.Add(iron);
                for (int k = 0; k < iron.hole1Irons.Count; k++)
                {
                    this.hole1Irons.Add(iron.hole1Irons[k]);
                }
                break;
            } while (l < 100);
        }else if (hole1Irons.Count % 3 == 2)
        {
            currentLayer = soLayer;
            int m = -1, n = -1, l = 0;
            do
            {
                l++;
                m = Random.Range(0, 9) - 4;
                n = Random.Range(0, 9) - 4;
                Iron iron = Instantiate(ironTemplate[Random.Range(0, ironTemplate.Count)], tfLevel);
                iron.transform.localPosition = new Vector3((float)m * sizeBroad, (float)n * sizeBroad, 0);
                iron.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 12) * 30));

                yield return new WaitForSeconds(0.1f);
                if (iron.nIronVaCham > 0 || iron.hole1Irons.Count != 1)
                {
                    Destroy(iron.gameObject);
                    continue;
                }
                irons.Add(iron);
                for (int k = 0; k < iron.hole1Irons.Count; k++)
                {
                    this.hole1Irons.Add(iron.hole1Irons[k]);
                }
                break;
            } while (l < 100);
        }

        Dictionary<int, int> randomColors = new Dictionary<int, int>();

        for (int i = 0; i < soColor; i++)
        {
            int r = -1;
            int l = 0;
            do
            {
                l++;
                r = Random.Range(0, colors.Count);
            } while (randomColors.ContainsKey(r) && l < 500);
            if (l >= 500) Debug.LogError("Không tạo được colorrrrrrrr");
            randomColors.Add(r, (hole1Irons.Count / 3 / soColor + (i < ((hole1Irons.Count/3) % soColor) ? 1 : 0)) * 3);
        }
        List<int> rcolor = new List<int>();
        foreach (int aaa in randomColors.Keys)
        {
            for (int i = 0; i < randomColors[aaa];i++)
            {
                rcolor.Add(aaa);
            }
        }
        int nn = rcolor.Count;
        while (nn > 1)
        {
            nn--;
            int t = Random.Range(0, nn);
            int tmp = rcolor[nn];
            rcolor[nn] = rcolor[t];
            rcolor[t] = tmp;
        }
        yield return new WaitForSeconds(0.1f);
        if (hole1Irons.Count % 3 != 0)
        {
            Debug.LogError("So dinh khong chia het cho 3!!!!!!!!!!!");
        }


        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < hole1Irons.Count; i++)
        {
            hole1Irons[i].screwType = rcolor[i];
            hole1Irons[i].OnInit();
        }
    }

    public void Btn_hole()
    {
        
    }

    public void OnChangeColor()
    {
        /*colorType = (ColorType)dropdown.value;
        currentColorImage.color = dic_colorMaterials[colorType]._material.color;*/
    }

    public void OnChangeColor(int i)
    {
        /*colorType = (ColorType)i;
        currentColorImage.color = dic_colorMaterials[colorType]._material.color;*/
    }

    public void Btn_createSnake()
    {
        /*currentSnake = Instantiate(snakePrefab);
        previousCaro = null;
        snakes.Add(currentSnake);

        txt_snakeCount.text = "snake: " + snakes.Count;*/
    }

    [Button]
    public void RandomIndex()
    {
        /*List<int> list = new List<int>();
        for (int i = 1; i <= snakes.Count; i++)
        {
            list.Add(i);
        }

        // Fisher–Yates shuffle
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i);
            int temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        for (int i = 0; i < snakes.Count; i++)
        {
            snakes[i].index = list[i];
        }*/
    }

    public void Btn_clear()
    {
        /*for (int i = 0; i < caros.Count; i++)
        {
            Destroy(caros[i].gameObject);
        }

        caros.Clear();

        for (int i = 0; i < snakes.Count; i++)
        {
            Destroy(snakes[i].gameObject);
        }

        snakes.Clear();

        currentSnake = null;
        previousCaro = null;
        txt_snakeCount.text = "";*/
    }

    string folder = "Assets/_Game/_level_SO";
    string assetName = "lv 1";
#if UNITY_EDITOR
    [Button]
    public void Btn_save()
    {
        if (inputField_level.text != "") assetName = "lv " + inputField_level.text;
        else assetName = "lv " + "2000";

        LevelGameModel level = FindOrCreateAsset<LevelGameModel>(folder, assetName);

        level.levelModel.soLayer = int.Parse(inputField_soLayer.text);
        /*level.levelModel.hole2Models.Clear();
        level.levelModel.ironModes.Clear();
        level.levelModel.keyModels.Clear();*/
        level.levelModel.ironModes = new List<IronMode>();

        for (int i = 0; i < irons.Count; i++)
        {
            IronMode ironData = new IronMode();
            ironData.id = irons[i].id;
            ironData.layer = irons[i].layer;
            ironData.hasIce = irons[i].hasIce;
            ItemTransModel trans = new ItemTransModel(irons[i].transform.position, irons[i].transform.localScale, irons[i].transform.rotation.eulerAngles);
            ironData.transModel = trans;
            ironData.polygonColliderPoints = new List<Vector2>();
            ironData.holeModels = new List<Hole1Model>();

            for (int j = 0; j < irons[i].polygonCollider.GetPath(0).Length; j++)
            {
                ironData.polygonColliderPoints.Add(irons[i].polygonCollider.GetPath(0)[j]);
            }

            for (int j = 0;j < irons[i].hole1Irons.Count; j++)
            {
                Hole1Model hole1Model = new Hole1Model();
                ItemTransModel h = new ItemTransModel(irons[i].hole1Irons[j].transform.localPosition, irons[i].hole1Irons[j].transform.localScale, irons[i].hole1Irons[j].transform.localRotation.eulerAngles);
                hole1Model.transModel = h;
                hole1Model.screwType = irons[i].hole1Irons[j].screwType;
                hole1Model.hasScrew = irons[i].hole1Irons[j].hasScrew;
                hole1Model.hasLock = irons[i].hole1Irons[j].hasLock;

                ironData.holeModels.Add(hole1Model);
            }
            level.levelModel.ironModes.Add(ironData);
        }
        // Đánh dấu thay đổi và lưu lại
        EditorUtility.SetDirty(level);
        AssetDatabase.SaveAssets();
    }
#endif
#if UNITY_EDITOR
    public void Btn_save(int lv)
    {
        /*assetName = "lv " + lv.ToString();
        Level_SO level = FindOrCreateAsset<Level_SO>(folder, assetName);

        level.width = b.h2;
        level.height = b.h1;

        level.snakeDatas.Clear();
        level.caroDatas.Clear();

        snakes.Sort((a, b) => a.index.CompareTo(b.index)); // sắp xếp lại theo index


        for (int i = 0; i < snakes.Count; i++)
        {
            SnakeData snakeData = new SnakeData();
            snakeData.colorType = snakes[i].colorType;
            snakeData.curCaroIndexs = snakes[i].curCaroIndexs;
            snakeData.index = snakes[i].index;
            snakeData.bullet = snakes[i].bullet;

            snakeData.coConThu2 = snakes[i].coConThu2;
            snakeData.colorConthu2 = snakes[i].colorConthu2;
            snakeData.indexConThu2 = snakes[i].indexConThu2;
            snakeData.bulletConThu2 = snakes[i].bulletConThu2;

            snakeData.isLock = snakes[i].numLock > 0;
            snakeData.numLock = snakes[i].numLock;
            snakeData.numIce = snakes[i].numIce;
            snakeData.numCocoon = snakes[i].numCocoon;

            level.snakeDatas.Add(snakeData);
        }

        //level.snakeDatas.Sort((a, b) => a.index.CompareTo(b.index)); // sắp xếp lại theo index

        for (int i = 0; i < caros.Count; i++)
        {
            CaroData caroData = new CaroData();
            caroData.isWall = caros[i].isWall;
            caroData.isHole = caros[i].isHole;
            caroData.isTunnel = caros[i].isTunnel;
            caroData.holeColor = caros[i].holeColor;
            if (caroData.isTunnel)
            {
                caroData.queueSnakes = caros[i].tunnel.queueSnakes;
                caroData.indexTempSnake = snakes.IndexOf(caros[i].tunnel.tempSnake); // chỉ số rắn tạm thời nếu có
            }
            caroData.isSpecialHole = caros[i].isSpecialHole;

            level.caroDatas.Add(caroData);

        }*/

        // Đánh dấu thay đổi và lưu lại
        //EditorUtility.SetDirty(level);
        AssetDatabase.SaveAssets();
    }
#endif

#if UNITY_EDITOR
    public T FindOrCreateAsset<T>(string folderPath, string assetName) where T : ScriptableObject
    {
        // Đảm bảo folder tồn tại
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            string[] parts = folderPath.Split('/');
            string current = "Assets";
            for (int i = 1; i < parts.Length; i++)
            {
                string next = $"{current}/{parts[i]}";
                if (!AssetDatabase.IsValidFolder(next))
                {
                    AssetDatabase.CreateFolder(current, parts[i]);
                }
                current = next;
            }
        }

        string assetPath = $"{folderPath}/{assetName}.asset";

        // Thử tìm asset đã tồn tại
        T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
        if (asset == null)
        {
            // Nếu chưa có thì tạo mới
            asset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(asset, assetPath);
            Debug.Log($"🆕 Created new {typeof(T).Name} at: {assetPath}");
        }
        else
        {
            Debug.Log($"✅ Found existing {typeof(T).Name} at: {assetPath}");
        }

        return asset;
    }

    [Button]
    public void btn_loadLevel()
    {
        Btn_clear();

        if (inputField_level.text == "") assetName = "lv " + "2000";
        else assetName = "lv " + inputField_level.text;
        LevelGameModel level = FindOrCreateAsset<LevelGameModel>(folder, assetName);

        inputField_soLayer.text = level.levelModel.soLayer.ToString();

        ironCount = level.levelModel.ironModes.Count;

        //txt_snakeCount.text = "snake: " + level.snakeDatas.Count;

        // tạo caro
        //Btn_create();


        // spawn rắn
        for (int i = 0; i < level.levelModel.ironModes.Count; i++)
        {
            Iron iron = Instantiate(ironTemplate[level.levelModel.ironModes[i].id], tfLevel);
            irons.Add(iron);
            iron.id = level.levelModel.ironModes[i].id;
            iron.layer = level.levelModel.ironModes[i].layer;
            iron.hasIce = level.levelModel.ironModes[i].hasIce;
            iron.polygonCollider = iron .gameObject.AddComponent<PolygonCollider2D>();
            iron.polygonCollider.SetPath(0, level.levelModel.ironModes[i].polygonColliderPoints);
            iron.transform.position = level.levelModel.ironModes[i].transModel.position;
            iron.transform.rotation = Quaternion.Euler(level.levelModel.ironModes[i].transModel.rotation);
            iron.transform.localScale = level.levelModel.ironModes[i].transModel.localScale;

            for (int j = 0; j < level.levelModel.ironModes[i].holeModels.Count; j++)
            {
                Hole1Iron hole1Iron = Instantiate(hole1Prefab, iron.transform);
                hole1Iron.trans.localPosition = level.levelModel.ironModes[i].holeModels[j].transModel.position;
                hole1Iron.trans.localRotation = Quaternion.Euler(level.levelModel.ironModes[i].holeModels[j].transModel.rotation);
                hole1Iron.trans.localScale = level.levelModel.ironModes[i].holeModels[j].transModel.localScale;
                hole1Iron.hasScrew = level.levelModel.ironModes[i].holeModels[j].hasScrew;
                hole1Iron.screwType = level.levelModel.ironModes[i].holeModels[j].screwType;

                hole1Irons.Add(hole1Iron);

            }

        }

        

        for (int i = 0; i < hole1Irons.Count; i++)
        {
            hole1Irons[i].OnInit();
        }

        if (hole1Irons.Count % 3  != 0)
        {
            Debug.LogError("So dinh khong chia het cho 3!!!!!!!!!!!");
        }
    }
#endif
}

[System.Serializable]
public class SnakeBulletPreset
{
    public int length;
    public int bullet;
}

//#endif