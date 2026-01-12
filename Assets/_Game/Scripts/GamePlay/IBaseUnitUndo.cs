using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseUnitUndo
{
    public void AddDataUndo(UndoModel undoModel);
    public void Undo(Screw screw, UndoModel undoModel, int n);
}
