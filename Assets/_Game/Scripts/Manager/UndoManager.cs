using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UndoManager : Singleton<UndoManager>
{
    public Stack<UndoModel> undoModels = new Stack<UndoModel>();
    public Stack<UndoModel> undoModels2 = new Stack<UndoModel>();
    public List<UndoModel> listUndoModels;
    public Button btn;
    public int currentCountMove = 0;

    public List<IBaseUnitUndo> unitUndos = new List<IBaseUnitUndo>();

    public void OnInit(Level level)
    {
        undoModels.Clear();
        undoModels2.Clear();
        unitUndos.Clear();
        listUndoModels.Clear();
        DOVirtual.DelayedCall(0.2f, () =>
        {
            for (int i = 0; i < level.irons.Count; i++)
            {
                unitUndos.Add(level.irons[i]);
            }
        });
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddDataUndo(Screw screw)
    {
        UndoModel undoModel = new UndoModel();
        undoModel.screwUndo = screw;
        undoModel.sortingOrder = screw.layer;

        for (int i = 0; i < unitUndos.Count; i++)
        {
            unitUndos[i].AddDataUndo(undoModel);
        }
        undoModels.Push(undoModel);
        listUndoModels.Add(undoModel);
    }

    public void Undo(Screw screw)
    {
        if (CheckCanUndo())
        {
            UndoModel undoModel;
            if (undoModels.Count > 0)
            {
                do
                {
                    undoModel = undoModels.Pop();
                    //listUndoModels.RemoveAt(listUndoModels.Count - 1);
                    undoModels2.Push(undoModel);
                } while (undoModel.screwUndo != screw && undoModels.Count > 0);
                if (undoModel.screwUndo != screw && undoModels.Count == 0) return;
            }
            else
            {
                return;
            }
            

            undoModel.screwUndo.Undo();

            for (int i = 0; i < unitUndos.Count; i++)
            {
                unitUndos[i].Undo(screw, undoModel, i);
            }
            if (undoModels2.Count > 0)
            {
                undoModels2.Pop();
            }
            while (undoModels2.Count > 0)
            {
                undoModels.Push(undoModels2.Pop());
            }
        }
    }

    public bool CheckCanUndo()
    {
        return true;//LevelManager.Ins.currentLevel.queueTile.numberScrew > 0;
    }

    public void CheckEnableButton()
    {
        btn.interactable = CheckCanUndo();
    }
}

[System.Serializable]
public class IronUndoModel
{
    public int index;
    public Vector3 position;
    public Quaternion rotation;
    public List<Screw_Hole> screws_holes = new List<Screw_Hole>();
    public bool isTrigger;
    public Vector3 velocity;
}

[System.Serializable]
public class UndoModel
{
    public Screw screwUndo;
    public int sortingOrder;
    public List<IronUndoModel> ironUndoModels = new List<IronUndoModel>();
}