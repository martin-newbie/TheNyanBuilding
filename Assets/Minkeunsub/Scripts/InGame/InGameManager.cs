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
    public int x;
    public int y;
    public int gridSize;
    public Vector2 GridOffset;
    public GridType[,] AbleGrid;
    public Vector2[,] GridPos;
    public Character[,] CharacterGridInfo;

    [Header("UI")]
    public Canvas canvas;

    [Header("Objects")]
    public GameObject LockObj;
    public List<GameObject> LockList = new List<GameObject>();
    public Gauge gaugeObj;

    [Header("Characters")]
    public List<ITouchAble> TouchAbleCharacters = new List<ITouchAble>();
    public List<IAuto> AutoIdlCharacters = new List<IAuto>();

    private void Awake()
    {
        AbleGrid = new GridType[y, x];
        CharacterGridInfo = new Character[y, x];
        InitGridPos();
        UnlockGrid(0);
        InitLock();


        InitCharacters();
        SpawnCharacter(CharacterType.Bro, new Vector2Int(0, 0));
        SpawnCharacter(CharacterType.Developer, new Vector2Int(1, 0));
    }

    private void Update()
    {
        if (AutoIdlCharacters.Count > 0) AutoCharacterLogic();
    }

    public void GaugeChargeEvent(float bad, float better, float best)
    {
        float rand = Random.Range(0f, 1f);

        if (rand <= bad)
        {
            //bad
        }
        else if (rand <= better)
        {
            //better
        }
        else if (rand <= best)
        {
            //best
        }
    }

    void AutoCharacterLogic()
    {
        foreach (var item in AutoIdlCharacters)
        {
            item.OnIdle();
        }
    }

    void InitLock()
    {
        for (int y = 0; y < AbleGrid.GetLength(0); y++)
        {
            GameObject temp = Instantiate(LockObj);
            temp.transform.position = GetGridPos(1, y);
            temp.SetActive(AbleGrid[y, 0] == GridType.Locked);
        }
    }

    void InitGridPos()
    {
        GridPos = new Vector2[y, x];
        for (int x = 0; x < GridPos.GetLength(1); x++)
        {
            for (int y = 0; y < GridPos.GetLength(0); y++)
            {
                GridPos[y, x] = new Vector2(x * gridSize, y * gridSize) + GridOffset;
            }
        }
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
        Bro = Resources.Load<Character>("Characters/BroNyan");
        Dev = Resources.LoadAll<Character>("Characters/DEV");
        QA = Resources.LoadAll<Character>("Characters/QA");
        PM = Resources.LoadAll<Character>("Characters/PM");
    }


    public void SpawnCharacter(CharacterType type, Vector2Int pos, int idx = 0)
    {
        Vector2 spawnPos = GetGridPos(pos.x, pos.y);

        switch (type)
        {
            case CharacterType.Bro:

                Gauge gauge = Instantiate(gaugeObj, canvas.transform);
                gauge.transform.SetAsFirstSibling();

                Character bro = SpawnCharacter(Bro, spawnPos);
                ((CharacterTouchAble)bro).InitGauge(gauge);

                TouchAbleCharacters.Add(bro.GetComponent<ITouchAble>());
                CharacterGridInfo[pos.y, pos.x] = bro;
                break;
            case CharacterType.Developer:

                Gauge gauge1 = Instantiate(gaugeObj, canvas.transform);
                gauge1.transform.SetAsFirstSibling();

                Character dev = SpawnCharacter(Dev[idx], spawnPos);
                ((CharacterIdle)dev).InitGauge(gauge1);

                AutoIdlCharacters.Add(dev.GetComponent<IAuto>());
                CharacterGridInfo[pos.y, pos.x] = dev;
                break;
            case CharacterType.QualityAssureance:
                SpawnCharacter(QA[idx], spawnPos);
                break;
            case CharacterType.ProductManager:
                SpawnCharacter(PM[idx], spawnPos);
                break;
        }
    }

    public void TouchCharacter()
    {
        foreach (var item in TouchAbleCharacters)
        {
            item.OnTouch();
        }
    }

    public Character SpawnCharacter(Character prefab, Vector2 pos)
    {
        Character spawnTmp = Instantiate(prefab, pos, Quaternion.identity);
        return spawnTmp;
    }

    public Vector2 GetGridPos(Vector2 inputPos)
    {
        float distance = 100f;
        Vector2 pos = new Vector2();

        for (int _x = 0; _x < GridPos.GetLength(1); _x++)
        {
            for (int _y = 0; _y < GridPos.GetLength(0); _y++)
            {
                float _d = Vector3.Distance(GridPos[_y, _x], inputPos);
                if (_d < distance)
                {
                    distance = _d;
                    pos = GridPos[_y, _x];
                }
            }
        }

        return pos;
    }

    public Vector2 GetGridPos(int x, int y)
    {
        Vector2 retVec = (new Vector2(x, y) * gridSize) + GridOffset;
        return retVec;
    }

    public bool GetAblePos(int x, int y)
    {
        if (x < 0 || y < 0 || x >= AbleGrid.GetLength(1) || y >= AbleGrid.GetLength(0))
            return false;

        GridType type = AbleGrid[y, x];
        if (type == GridType.Empty)
        {
            return true;
        }
        else return false;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
            for (int x = 0; x < AbleGrid.GetLength(1); x++)
            {
                for (int y = 0; y < AbleGrid.GetLength(0); y++)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawCube(GetGridPos(x, y), new Vector2(gridSize - 0.2f, gridSize - 0.2f));
                }
            }
    }
}

public interface ITouchAble
{
    public abstract void OnTouch();
}

public interface IAuto
{
    public abstract void OnIdle();
}

public interface IBuff
{
    public abstract void OnBuff();
}
