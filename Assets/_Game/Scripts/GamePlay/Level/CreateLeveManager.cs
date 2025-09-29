#if UNITY_EDITOR

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
    public List<SnakeBulletPreset> snakeBulletPresets = new List<SnakeBulletPreset>();


    public TMP_InputField inputField_soCot;
    public TMP_InputField inputField_soHang;
    public TMP_InputField inputField_level;

    public Camera mainCamera;
    public int caroStatus = 0; // 0 là hole, 1 là snake, 2 là wall
    public int realSnakeCount = 0;
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
        /*int soCot = int.Parse(inputField_soCot.text);
        int soHang = int.Parse(inputField_soHang.text);

        if (soCot <= 0 || soHang <= 0)
        {
            Debug.LogError("Số cột và số hàng phải lớn hơn 0");
            return;
        }

        caros.Clear();

        Vector3 pos = Vector3.zero;
        for (int j = 0; j < soHang; j++)
        {
            for (int i = 0; i < soCot; i++)
            {
                Caro_createLevel newCaro = Instantiate(caroPrefab);
                newCaro.x = i;
                newCaro.y = j;
                caros.Add(newCaro);
                newCaro.transform.position = new Vector3(i, 0, -j); // đặt vị trí caro
                pos += newCaro.transform.position;
            }
        }

        pos /= (soCot * soHang);
        mainCamera.transform.position = new Vector3(pos.x, 20, pos.z + 2);*/
    }

    public void Btn_create(int a1, int a2)
    {
        /*int soCot = a2;
        int soHang = a1;

        if (soCot <= 0 || soHang <= 0)
        {
            Debug.LogError("Số cột và số hàng phải lớn hơn 0");
            return;
        }

        caros.Clear();

        Vector3 pos = Vector3.zero;
        for (int j = 0; j < soHang; j++)
        {
            for (int i = 0; i < soCot; i++)
            {
                Caro_createLevel newCaro = Instantiate(caroPrefab);
                newCaro.x = i;
                newCaro.y = j;
                caros.Add(newCaro);
                newCaro.transform.position = new Vector3(i, 0, -j); // đặt vị trí caro
                pos += newCaro.transform.position;
            }
        }

        pos /= (soCot * soHang);
        mainCamera.transform.position = new Vector3(pos.x, 20, pos.z + 2);*/
    }

    public void Btn_hole()
    {
        caroStatus = 0;
        img_hole.color = Color.yellow;
        img_snake.color = Color.white;
        img_wall.color = Color.white;
        img_tunnel.color = Color.white;
    }

    public void Btn_snake()
    {
        caroStatus = 1;
        img_hole.color = Color.white;
        img_snake.color = Color.yellow;
        img_wall.color = Color.white;
        img_tunnel.color = Color.white;
    }

    public void Btn_wall()
    {
        caroStatus = 2;
        img_hole.color = Color.white;
        img_snake.color = Color.white;
        img_wall.color = Color.yellow;
        img_tunnel.color = Color.white;
    }

    public void Btn_tunnel()
    {
        caroStatus = 3;
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

    /*public T FindOrCreateAsset<T>(string folderPath, string assetName) where T : ScriptableObject
    {
        *//*// Đảm bảo folder tồn tại
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

        return asset;*//*
    }*/

    public void btn_loadLevel()
    {
        /*Btn_clear();

        if (inputField_level.text == "") return;
        assetName = "lv " + inputField_level.text;
        Level_SO level = FindOrCreateAsset<Level_SO>(folder, assetName);

        inputField_soCot.text = level.width.ToString();
        inputField_soHang.text = level.height.ToString();

        realSnakeCount = level.snakeDatas.Count;

        //txt_snakeCount.text = "snake: " + level.snakeDatas.Count;

        // tạo caro
        Btn_create();


        // spawn rắn
        for (int i = 0; i < level.snakeDatas.Count; i++)
        {
            Snake _snake = Instantiate(snakePrefab);
            snakes.Add(_snake);
            _snake.index = level.snakeDatas[i].index;
            _snake.bullet = level.snakeDatas[i].bullet;
            _snake.colorType = level.snakeDatas[i].colorType;
            _snake.curCaroIndexs = level.snakeDatas[i].curCaroIndexs;

            _snake.coConThu2 = level.snakeDatas[i].coConThu2;
            _snake.colorConthu2 = level.snakeDatas[i].colorConthu2;
            _snake.indexConThu2 = level.snakeDatas[i].indexConThu2;
            _snake.bulletConThu2 = level.snakeDatas[i].bulletConThu2;

            if (_snake.coConThu2)
            {
                realSnakeCount += 1;
            }

            _snake.numLock = level.snakeDatas[i].numLock;
            _snake.numIce = level.snakeDatas[i].numIce;
            _snake.numCocoon = level.snakeDatas[i].numCocoon;


            for (int j = 0; j < level.snakeDatas[i].curCaroIndexs.Count; j++)
            {
                if (j == 0) // đầu
                {
                    BodyPart headPart = Instantiate(headPrefab, _snake.transform);
                    headPart.renderer1.material = dic_colorMaterials[_snake.colorType]._material;
                    headPart.renderer2.material = dic_colorMaterials[_snake.colorType]._material;
                    _snake.transform.position = caros[level.snakeDatas[i].curCaroIndexs[j]].transform.position;
                    headPart.transform.position = caros[level.snakeDatas[i].curCaroIndexs[j]].transform.position;
                    headPart.transform.LookAt(2 * caros[level.snakeDatas[i].curCaroIndexs[j]].transform.position - caros[level.snakeDatas[i].curCaroIndexs[j + 1]].transform.position);
                    _snake.bodyParts.Add(headPart);

                    Material[] mats = headPart.renderer2.materials; // tạo bản sao array material hiện tại
                    if (mats.Length > 1)
                    {
                        mats[1] = dic_colorMaterials[_snake.colorType]._material; // gán vào Element 1
                        headPart.renderer2.materials = mats; // gán lại mảng material
                    }
                }
                else if (j == level.snakeDatas[i].curCaroIndexs.Count - 1) // đuôi
                {
                    BodyPart tailPart = Instantiate(tailPrefab, _snake.transform);
                    tailPart.renderer1.material = dic_colorMaterials[_snake.colorType]._material;
                    tailPart.renderer2.material = dic_colorMaterials[_snake.colorType]._material;
                    tailPart.transform.position = caros[level.snakeDatas[i].curCaroIndexs[j]].transform.position;
                    tailPart.transform.LookAt(caros[level.snakeDatas[i].curCaroIndexs[j - 1]].transform.position);
                    _snake.bodyParts.Add(tailPart);

                    Material[] mats = tailPart.renderer1.materials; // tạo bản sao array material hiện tại
                    if (mats.Length > 1)
                    {
                        mats[1] = dic_colorMaterials[_snake.colorType]._material; // gán vào Element 1
                        tailPart.renderer1.materials = mats; // gán lại mảng material
                    }
                }
                else  // thân
                {
                    BodyPart bodyPart = Instantiate(bodyPrefab, _snake.transform);
                    bodyPart.renderer1.material = dic_colorMaterials[_snake.colorType]._material;
                    bodyPart.renderer2.material = dic_colorMaterials[_snake.colorType]._material;
                    bodyPart.transform.position = caros[level.snakeDatas[i].curCaroIndexs[j]].transform.position;
                    bodyPart.transform.LookAt(2 * caros[level.snakeDatas[i].curCaroIndexs[j]].transform.position - caros[level.snakeDatas[i].curCaroIndexs[j + 1]].transform.position);
                    _snake.bodyParts.Add(bodyPart);

                    Material[] mats1 = bodyPart.renderer1.materials; // tạo bản sao array material hiện tại
                    Material[] mats2 = bodyPart.renderer2.materials; // tạo bản sao array material hiện tại
                                                                     // if (mats.Length > 1)
                                                                     // {
                    mats1[1] = dic_colorMaterials[_snake.colorType]._material; // gán vào Element 1
                    mats2[1] = dic_colorMaterials[_snake.colorType]._material; // gán vào Element 1
                    bodyPart.renderer1.materials = mats1; // gán lại mảng material
                    bodyPart.renderer2.materials = mats2; // gán lại mảng material
                }
            }

            if (_snake.numLock > 0)
            {
                _snake.bodyParts[0].obj_lock.SetActive(true);
                _snake.bodyParts[0].txt_lock.text = _snake.numLock.ToString();
                _snake.bodyParts[0].txt_lock.transform.eulerAngles = new Vector3(90, 0, 0);
            }

            _snake.txt_index.text = _snake.index.ToString();
            _snake.txt_bullet.text = _snake.bullet.ToString();

            if (_snake.coConThu2) _snake.txt_index.text += "*";

        }

        for (int i = 0; i < level.caroDatas.Count; i++)
        {
            if (level.caroDatas[i].isWall)
            {
                caros[i].isWall = true;
                caros[i].obj_wall.SetActive(true);
                caros[i].spriteRenderer.color = Color.gray;
            }
            else if (level.caroDatas[i].isHole)
            {
                caros[i].isHole = true;
                caros[i].hole.gameObject.SetActive(true);
                caros[i].holeColor = level.caroDatas[i].holeColor;
                caros[i].isSpecialHole = level.caroDatas[i].isSpecialHole;

                caros[i].hole.SetSkin(dic_colorMaterials[level.caroDatas[i].holeColor].skinHole, level.caroDatas[i].holeColor);
                // caros[i].hole._renderer.material = dic_colorMaterials[level.caroDatas[i].holeColor];
            }
            else if (level.caroDatas[i].isTunnel)
            {
                caros[i].isTunnel = true;
                caros[i].tunnel = Instantiate(tunnelPrefab, caros[i].transform);
                caros[i].tunnel.queueSnakes = level.caroDatas[i].queueSnakes;
                caros[i].tunnel.tempSnake = snakes[level.caroDatas[i].indexTempSnake];
            }
        }


        txt_snakeCount.text = "snake: " + realSnakeCount;

        b.SetDictionaryItem();*/
    }
}

[System.Serializable]
public class SnakeBulletPreset
{
    public int length;
    public int bullet;
}

#endif