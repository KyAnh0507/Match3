#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CreateLeveManager : MonoBehaviour
{
    public static CreateLeveManager ins;

    [PropertyOrder(-1)]
    [Button("Apply Bullets")]
    public void ApplyBullets()
    {
        /*foreach (Snake snake in snakes)
        {
            foreach (SnakeBulletPreset preset in snakeBulletPresets)
            {
                if (snake.curCaroIndexs.Count == preset.length)
                {
                    snake.bullet = preset.bullet;
                    snake.txt_bullet.text = snake.bullet.ToString();
                    if (snake.coConThu2) snake.bulletConThu2 = preset.bullet;
                    break;
                }
            }
        }*/
    }



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
    public Image img_hole;
    public Image img_snake;
    public Image img_wall;
    public Image img_tunnel;
    public ColorType colorType;
    public TMP_Dropdown dropdown;

    public BodyPart headPrefab;
    public BodyPart bodyPrefab;
    public BodyPart tailPrefab;
    public Image currentColorImage;
    public TMP_Text txt_snakeCount;

    public List<Iron> ironTemplate;
    public Hole1Iron hole1Prefab;

    public Transform tfLevel;
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

    public float sizeBroad = 0.6f;
    public LayerMask ironLayerMask;
    public IEnumerator CreateLevel()
    {
        int soLayer = int.Parse(inputField_soLayer.text);
        int soIron1Layer = int.Parse(inputField_soIron1Layer.text);
        int soColor = int.Parse(inputField_soColor.text);

        irons.Clear();
        hole1Irons.Clear();

        for (int i = 0; i < soLayer; i++)
        {
            for (int j = 0; j < soIron1Layer; j++)
            {
                int m = -1, n = -1;
                do
                {
                    int m = Random.Range(0, 9) - 4;
                    int n = Random.Range(0, 9) - 4;
                    Iron iron = Instantiate(ironPrefabs[Random.Range(0, ironPrefabs.Count)], tfLevel);

                    for (int k = 0; k < iron.hole1Irons.Count; k++)
                    {
                        this.hole1Irons.Add(iron.hole1Irons[i]);
                    }
                }while (true);
                
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.1f);
        }

        Dictionary<int, int> randomColors = new Dictionary<int, int>();

        for (int i = 0; i < colors.Count; i++)
        {
            int r = -1;
            int l = 0;
            do
            {
                l++;
                r = Random.Range(0, colors.Count);
            } while (randomColors.ContainsKey(r) && l < 500);
            if (l >= 500) Debug.LogError("Không tạo được colorrrrrrrr");
            randomColors.Add(r, hole1Irons.Count / soColor);

        }
        yield return new WaitForSeconds(0.1f);
        if (hole1Irons.Count % 3 != 0)
        {
            Debug.LogError("So dinh khong chia het cho 3!!!!!!!!!!!");
        }
    }

    public void Btn_hole()
    {
        img_hole.color = Color.yellow;
        img_snake.color = Color.white;
        img_wall.color = Color.white;
        img_tunnel.color = Color.white;
    }

    public void Btn_snake()
    {
        img_hole.color = Color.white;
        img_snake.color = Color.yellow;
        img_wall.color = Color.white;
        img_tunnel.color = Color.white;
    }

    public void Btn_wall()
    {
        img_hole.color = Color.white;
        img_snake.color = Color.white;
        img_wall.color = Color.yellow;
        img_tunnel.color = Color.white;
    }

    public void Btn_tunnel()
    {
        img_hole.color = Color.white;
        img_snake.color = Color.white;
        img_wall.color = Color.white;
        img_tunnel.color = Color.yellow;
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

    public void CreateSnake()
    {
        /*if (previousCaro == null)
        {
            currentSnake.colorType = colorType;
            currentSnake.transform.position = caro_CreateLevel.transform.position;
            BodyPart head = Instantiate(headPrefab, currentSnake.transform);
            head.transform.position = caro_CreateLevel.transform.position;
            currentSnake.bodyParts.Add(head);
            head.renderer1.material = dic_colorMaterials[colorType]._material;
            head.renderer2.material = dic_colorMaterials[colorType]._material;

            Material[] mats = head.renderer2.materials; // tạo bản sao array material hiện tại
            if (mats.Length > 1)
            {
                mats[1] = dic_colorMaterials[colorType]._material; // gán vào Element 1
                head.renderer2.materials = mats; // gán lại mảng material
            }

            currentSnake.curCaroIndexs = new List<int>();
        }
        else
        {
            BodyPart tailPart = Instantiate(tailPrefab, currentSnake.transform);
            tailPart.transform.position = caro_CreateLevel.transform.position;
            currentSnake.bodyParts.Add(tailPart);
            tailPart.transform.LookAt(previousCaro.transform.position);
            tailPart.renderer1.material = dic_colorMaterials[colorType]._material;
            tailPart.renderer2.material = dic_colorMaterials[colorType]._material;

            Material[] mats = tailPart.renderer1.materials; // tạo bản sao array material hiện tại
            if (mats.Length > 1)
            {
                mats[1] = dic_colorMaterials[colorType]._material; // gán vào Element 1
                tailPart.renderer1.materials = mats; // gán lại mảng material
            }

            currentSnake.bodyParts[currentSnake.bodyParts.Count - 2].transform.LookAt(2 * previousCaro.transform.position - caro_CreateLevel.transform.position);

            if (currentSnake.bodyParts.Count > 2)
            {
                BodyPart bodyPart = Instantiate(bodyPrefab, currentSnake.transform);
                bodyPart.transform.position = currentSnake.bodyParts[currentSnake.bodyParts.Count - 2].transform.position;
                bodyPart.transform.eulerAngles = currentSnake.bodyParts[currentSnake.bodyParts.Count - 2].transform.eulerAngles;
                GameObject waitDes = currentSnake.bodyParts[currentSnake.bodyParts.Count - 2].gameObject;
                currentSnake.bodyParts[currentSnake.bodyParts.Count - 2] = bodyPart;
                Destroy(waitDes);

                Material[] mats1 = bodyPart.renderer1.materials; // tạo bản sao array material hiện tại
                Material[] mats2 = bodyPart.renderer2.materials; // tạo bản sao array material hiện tại
                                                                 // if (mats.Length > 1)
                                                                 // {
                mats1[1] = dic_colorMaterials[colorType]._material; //gán vào Element 1
                mats2[1] = dic_colorMaterials[colorType]._material; // gán vào Element 1
                bodyPart.renderer1.materials = mats1; // gán lại mảng material
                bodyPart.renderer2.materials = mats2;
            }
        }

        previousCaro = caro_CreateLevel;

        currentSnake.curCaroIndexs.Add(caros.IndexOf(caro_CreateLevel));*/
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

    string folder = "Assets/0_Game/_level_SO";
    string assetName = "lv 1";

    public void Btn_save()
    {
        /*if (inputField_level.text == "") return;

        assetName = "lv " + inputField_level.text;
        Level_SO level = FindOrCreateAsset<Level_SO>(folder, assetName);

        level.width = int.Parse(inputField_soCot.text);
        level.height = int.Parse(inputField_soHang.text);

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
        
        }

        

        // Đánh dấu thay đổi và lưu lại
        EditorUtility.SetDirty(level);
        AssetDatabase.SaveAssets();*/
    }

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

    public void btn_loadLevel()
    {
        Btn_clear();

        if (inputField_level.text == "") return;
        assetName = "lv " + inputField_level.text;
        LevelGameModel level = FindOrCreateAsset<LevelGameModel>(folder, assetName);

        inputField_soLayer.text = level.levelModel.soLayer.ToString();

        ironCount = level.levelModel.ironModes.Length;

        //txt_snakeCount.text = "snake: " + level.snakeDatas.Count;

        // tạo caro
        Btn_create();


        // spawn rắn
        for (int i = 0; i < level.levelModel.ironModes.Length; i++)
        {
            Iron iron = Instantiate(ironTemplate[level.levelModel.ironModes[i].id], tfLevel);
            irons.Add(iron);
            iron.id = level.levelModel.ironModes[i].id;
            iron.layer = level.levelModel.ironModes[i].layer;
            iron.hasIce = level.levelModel.ironModes[i].hasIce;
            iron.polygonCollider = iron .gameObject.AddComponent<PolygonCollider2D>();
            iron.polygonCollider.pathCount = level.levelModel.ironModes[i].polygonColliderPoints.Length;
            iron.polygonCollider.SetPath(0, level.levelModel.ironModes[i].polygonColliderPoints);
            iron.transform.position = level.levelModel.ironModes[i].transModel.position;
            iron.transform.rotation = Quaternion.Euler(level.levelModel.ironModes[i].transModel.rotation);
            iron.transform.localScale = level.levelModel.ironModes[i].transModel.localScale;

            for (int j = 0; j < level.levelModel.ironModes[i].holeModels.Length; j++)
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
}

[System.Serializable]
public class SnakeBulletPreset
{
    public int length;
    public int bullet;
}

#endif