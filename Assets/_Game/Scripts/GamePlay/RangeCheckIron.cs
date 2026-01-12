using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCheckIron : MonoBehaviour
{
    private static RangeCheckIron instance;
    public static RangeCheckIron Instance => instance;


    public Level level;
    private bool isWin = false;
    private bool endGame = false;
    public bool isEndGame => endGame;

    public delegate void IronDropClaim(int numbCounter);
    public event IronDropClaim OnIronDropClaim = delegate { };
    float counterTimeCheck = -10;

    private void Awake()
    {
        instance = this;

    }
    private void Start()
    {
        Vector2 localScale = transform.localScale;
        localScale.x = 100;
        transform.localScale = localScale;
    }
    /// <summary>
    /// Reset data
    /// </summary>
    public void ResetData()
    {
        isWin = false;
        counterTimeCheck = -10;
        endGame = false;
        OnIronDropClaim = null;
    }

    public void DropIron(int numbIron)
    {
        if (OnIronDropClaim != null)
        {
            OnIronDropClaim(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constant.TAG_IRON))
        {
            IBaseUnitUndo iron = collision.gameObject.GetComponent<IBaseUnitUndo>();
            if (iron != null)
            {
                Iron ironCollide = collision.GetComponent<Iron>();
                if (ironCollide != null)
                {
                    if (OnIronDropClaim != null)
                    {
                        OnIronDropClaim(1);
                    }
                }
                // Bracket Event

                ironCollide.isTrigger = true;

                DG.Tweening.DOVirtual.DelayedCall(.1f, () =>
                {
                    Vector2 closePoint = collision.transform.position;
                    if (closePoint.x < -3.5f)
                    {
                        closePoint.x = -3.5f;
                    }
                    if (closePoint.x > 3.5f)
                    {
                        closePoint.x = 3.5f;
                    }
                    closePoint.y = -6.5f;
                    GamePlay.Ins.PlayFXWoodClaim(closePoint);
                });
            }
        }
    }
    public bool IsVisibleFrom(Renderer renderer, Camera camera)
    {
        if (renderer == null)
            return false;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
