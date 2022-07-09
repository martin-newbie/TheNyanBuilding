using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Bro,
    Developer,
    QualityAssureance,
    ProductManager
}

public enum GridType
{
    Locked,
    Filled,
    Empty
}

public class InGameManager : Singleton<InGameManager>
{
    [Header("Characters")]
    public Character Bro;
    public Character[] Dev;
    public Character[] QA;
    public Character[] PM;

    [Header("Grid")]
    public int x, y;
    public int gridSize;
    public Vector2 GridOffset;
    public GridType[,] AbleGrid;

    private void Awake()
    {
        AbleGrid = new GridType[y, x];
        UnlockGrid(0);

        InitCharacters();
        SpawnCharacter(CharacterType.Bro);
    }

    public void UnlockGrid(int floor)
    {
        for (int y = 0; y < AbleGrid.GetLength(0); y++)
        {
            for (int x = 0; x < AbleGrid.GetLength(1); x++)
            {
                if (y == floor) AbleGrid[y, x] = GridType.Empty;
                else AbleGrid[y, x] = GridType.Locked;
            }
        }
    }

    public void InitCharacters()
    {
        Bro = Resources.Load<Character>("Characters/Bro");
        Dev = Resources.LoadAll<Character>("Characters/DEV");
        QA = Resources.LoadAll<Character>("Characters/QA");
        PM = Resources.LoadAll<Character>("Characters/PM");
    }


    public void SpawnCharacter(CharacterType type, int idx = 0)
    {

        Vector2 spawnPos = new Vector2();

        switch (type)
        {
            case CharacterType.Bro:
                SpawnCharacter(Bro, spawnPos);
                break;
            case CharacterType.Developer:
                SpawnCharacter(Dev[idx], spawnPos);
                break;
            case CharacterType.QualityAssureance:
                SpawnCharacter(QA[idx], spawnPos);
                break;
            case CharacterType.ProductManager:
                SpawnCharacter(PM[idx], spawnPos);
                break;
        }
    }

    public Character SpawnCharacter(Character prefab, Vector2 pos)
    {
        Character spawnTmp = Instantiate(prefab, pos, Quaternion.identity);
        return spawnTmp;
    }

    public Vector2 GetGridPos(Vector2 inputPos)
    {
        int x = Mathf.RoundToInt(inputPos.x);
        int y = Mathf.RoundToInt(inputPos.y);

        Vector2 retVec = new Vector2(x, y) * (gridSize / 2) + GridOffset;
        return retVec;
    }
}

public interface ITouchAble
{
    public abstract void OnTouch();
}

public interface IGauge
{
    public abstract void OnIdle();
}

public interface IBuff
{
    public abstract void OnBuff();
}
