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

public enum BuffType
{
    SpeedUp,
    FailDecrease
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
    public SpriteRenderer Node;
    public SpriteRenderer[,] Grid;

    [Header("Characters")]
    public List<ITouchAble> TouchAbleCharacters = new List<ITouchAble>();
    public List<IAuto> AutoIdlCharacters = new List<IAuto>();
    public List<IBuff> QACharacters = new List<IBuff>();
    public List<IBuff> PMCharacters = new List<IBuff>();
    public List<IGauge> GaugeCharacters = new List<IGauge>();
    public List<IBuff> BuffCharacters = new List<IBuff>();

    private void Awake()
    {
        AbleGrid = new GridType[y, x];
        CharacterGridInfo = new Character[y, x];
        Grid = new SpriteRenderer[y, x];
        InitGrid();

        InitGridPos();
        UnlockGrid(0);
        UnlockGrid(1);
        InitLock();
        SetLock();

        InitCharacters();
        SpawnCharacter(CharacterType.Bro, new Vector2Int(0, 0));
        SpawnCharacter(CharacterType.Developer, new Vector2Int(1, 0));
        SpawnCharacter(CharacterType.ProductManager, new Vector2Int(2, 0));
    }

    void InitGrid()
    {
        for (int x = 0; x < Grid.GetLength(1); x++)
        {
            for (int y = 0; y < Grid.GetLength(0); y++)
            {
                SpriteRenderer node = Instantiate(Node);
                node.transform.position = GetGridPos(x, y);
                Grid[y, x] = node;
            }
        }
    }

    private void Update()
    {
        if (AutoIdlCharacters.Count > 0) AutoCharacterLogic();

        if (Input.GetMouseButtonDown(0)) OnTouch();
        else if (Input.GetMouseButton(0) && curDrag != null) OnDrag();
        else if (Input.GetMouseButtonUp(0) && curDrag != null) EndDrag();

        if (curDrag != null && curDrag is CharacterBuff)
        {
            CharacterBuff buff = curDrag.GetComponent<CharacterBuff>();
            InitGridColor();
            foreach (var item in buff.BuffList)
            {
                int _x = item.x + buff.thisPosIdx.x;
                int _y = item.y + buff.thisPosIdx.y;

                if (_x < 0 || _y < 0 || _x >= x || _y >= y) continue;

                Grid[item.y + buff.thisPosIdx.y, item.x + buff.thisPosIdx.x].color = Color.green;
            }
        }
    }

    void InitGridColor()
    {
        for (int x = 0; x < Grid.GetLength(1); x++)
        {
            for (int y = 0; y < Grid.GetLength(0); y++)
            {
                Grid[y, x].color = Color.white;
            }
        }
    }

    Character curDrag;

    void OnTouch()
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Character nearby = GetTouchCharacter(touchPos);

        if (nearby != null)
        {
            curDrag = nearby;

            nearby.isDrag = true;

            curDrag.BodySR.sortingOrder = 9;
            curDrag.HeadSR.sortingOrder = 10;
        }
    }

    void OnDrag()
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        curDrag.transform.position = touchPos;
    }

    void EndDrag()
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int posIdx = GetPosIdx(touchPos);

        if (GetAblePos(posIdx.x, posIdx.y))
        {
            Vector2Int cIdx = curDrag.thisPosIdx;

            AbleGrid[cIdx.y, cIdx.x] = GridType.Empty;
            CharacterGridInfo[cIdx.y, cIdx.x] = null;

            curDrag.transform.position = GetGridPos(posIdx.x, posIdx.y);

            AbleGrid[posIdx.y, posIdx.x] = GridType.Filled;
            CharacterGridInfo[posIdx.y, posIdx.x] = curDrag;
            curDrag.thisPosIdx = posIdx;

        }
        else
        {
            curDrag.transform.position = GetGridPos(curDrag.thisPosIdx.x, curDrag.thisPosIdx.y);
        }

        curDrag.BodySR.sortingOrder = 2;
        curDrag.HeadSR.sortingOrder = 3;
        curDrag = null;

        InitGridColor();
        SetBuffCharacter();
    }


    public void GaugeChargeEvent(float bad, float better, float best)
    {
        float rand = Random.Range(0f, 1f);

        if (rand <= bad)
        {
            //bad
        }
        else if (rand <= best)
        {
            //best
        }
        else
        {
            //better
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
            LockList.Add(temp);

        }
    }

    void SetLock()
    {
        for (int x = 0; x < AbleGrid.GetLength(1); x++)
        {
            for (int y = 0; y < AbleGrid.GetLength(0); y++)
            {
                LockList[y].SetActive(AbleGrid[y, 0] == GridType.Locked);

                Grid[y, x].gameObject.SetActive(AbleGrid[y, x] != GridType.Locked);
            }
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

    public Character GetTouchCharacter(Vector2 inputPos)
    {

        if (inputPos.y < GridOffset.y) return null;

        float distance = 100f;
        Character nearby = null;

        for (int _x = 0; _x < GridPos.GetLength(1); _x++)
        {
            for (int _y = 0; _y < GridPos.GetLength(0); _y++)
            {
                float _d = Vector3.Distance(GridPos[_y, _x], inputPos);
                if (_d < distance)
                {
                    distance = _d;
                    nearby = CharacterGridInfo[_y, _x];
                }
            }
        }

        return nearby;
    }

    public Vector2Int GetPosIdx(Vector2 inputPos)
    {
        float distance = 100f;
        Vector2Int nearby = new Vector2Int();

        for (int _x = 0; _x < GridPos.GetLength(1); _x++)
        {
            for (int _y = 0; _y < GridPos.GetLength(0); _y++)
            {
                float _d = Vector3.Distance(GridPos[_y, _x], inputPos);
                if (_d < distance)
                {
                    distance = _d;
                    nearby = new Vector2Int(_x, _y);
                }
            }
        }

        return nearby;
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
                bro.thisPosIdx = pos;
                ((CharacterTouchAble)bro).InitGauge(gauge);

                TouchAbleCharacters.Add(bro.GetComponent<ITouchAble>());
                GaugeCharacters.Add(bro.GetComponent<IGauge>());
                CharacterGridInfo[pos.y, pos.x] = bro;

                break;
            case CharacterType.Developer:

                Gauge gauge1 = Instantiate(gaugeObj, canvas.transform);
                gauge1.transform.SetAsFirstSibling();

                Character dev = SpawnCharacter(Dev[idx], spawnPos);
                dev.thisPosIdx = pos;
                ((CharacterIdle)dev).InitGauge(gauge1);

                AutoIdlCharacters.Add(dev.GetComponent<IAuto>());
                GaugeCharacters.Add(dev.GetComponent<IGauge>());
                CharacterGridInfo[pos.y, pos.x] = dev;

                break;
            case CharacterType.QualityAssureance:
                Character qa = SpawnCharacter(QA[idx], spawnPos);

                IBuff qaBuff = qa.GetComponent<IBuff>();

                qaBuff.Init(pos);
                QACharacters.Add(qaBuff);
                BuffCharacters.Add(qaBuff);

                CharacterGridInfo[pos.y, pos.x] = qa;
                break;
            case CharacterType.ProductManager:
                Character pm = SpawnCharacter(PM[idx], spawnPos);

                IBuff buff = pm.GetComponent<IBuff>();
                buff.Init(pos);
                QACharacters.Add(buff);
                BuffCharacters.Add(buff);

                CharacterGridInfo[pos.y, pos.x] = pm;
                break;
        }

        AbleGrid[pos.y, pos.x] = GridType.Filled;
        SetBuffCharacter();
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

    public Character GetIndexCharacter(int x, int y)
    {
        if (x < 0 || y < 0 || x >= this.x || y >= this.y) return null;

        Character temp = CharacterGridInfo[y, x];
        return temp;
    }

    public void SetBuffCharacter()
    {
        foreach (var item in BuffCharacters)
        {
            item.OnBuff();
        }

        foreach (var item in GaugeCharacters)
        {
            item.GetValue();
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

public interface IGauge
{
    public abstract void SetBuff(IBuff buff);
    public abstract void GetValue();
}

public interface IBuff
{
    public float value { get; }
    public BuffType type { get; }
    public abstract void OnBuff();
    public abstract void Init(Vector2Int idx);
}

