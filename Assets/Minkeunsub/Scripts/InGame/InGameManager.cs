using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

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
    public List<Character> AllCharacterPrefabs = new List<Character>();

    [Header("Grid")]
    public int x;
    public int y;
    public int gridSize_x;
    public int gridSize_y;
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
    public GameObject Background;
    public GameObject LooftTop;
    public GameObject SmokeEffect;
    public RewardTextBox TextBox;

    [Header("Characters")]
    public List<ITouchAble> TouchAbleCharacters = new List<ITouchAble>();
    public List<IAuto> AutoIdlCharacters = new List<IAuto>();
    public List<IBuff> QACharacters = new List<IBuff>();
    public List<IBuff> PMCharacters = new List<IBuff>();
    public List<IGauge> GaugeCharacters = new List<IGauge>();
    public List<IBuff> BuffCharacters = new List<IBuff>();

    [Header("Inventory")]
    public List<Character> CharacterInventory = new List<Character>();
    public List<int> CharacterLevel = new List<int>(new int[9]);
    public List<int> CharacterIdxInventory = new List<int>();

    [Header("Values")]
    public int curFloor;
    public bool isDragging;
    public bool isUImoving;

    private void Awake()
    {
        AbleGrid = new GridType[y, x];
        CharacterGridInfo = new Character[y, x];
        Grid = new SpriteRenderer[y, x];
        InitGrid();

        InitGridPos();
        InitLock();
        SetLock();

        UnlockFloor();

        InitCharacters();
        GainCharacter(0);
        SpawnCharacterAsIndex(0, new Vector2(0f, -4f));
        SoundManager.Instance.PlayBackground("keyboard");
    }

    public void GainCharacter(int idx)
    {
        if (!CharacterIdxInventory.Contains(idx))
        {
            CharacterIdxInventory.Add(idx);

            Character temp = null;
            foreach (var item in AllCharacterPrefabs)
            {
                if (item.info.idx == idx)
                {
                    temp = item;
                    break;
                }
            }

            CharacterInventory.Add(temp);

        }
        else
        {
            CharacterLevel[idx]++;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="idx">index of character inventory</param>
    /// <returns></returns>
    public Character SpawnCharacterAsIndex(int idx, Vector2 inputPos)
    {

        Character spawn = null;
        foreach (var item in CharacterInventory)
        {
            if (item.info.idx == idx) spawn = item;
        }

        if (spawn == null) return null;

        Vector2Int pos = GetPosIdx(inputPos);

        SpawnTypeCharacter(spawn.characterType, pos, idx);
        return spawn;
    }

    public void UnlockFloor()
    {
        UnlockGrid(curFloor);
        curFloor++;
        SetLock();
    }

    void InitGrid()
    {
        for (int y = 0; y < Grid.GetLength(0); y++)
        {
            int order = 4;
            for (int x = 0; x < Grid.GetLength(1); x++)
            {
                SpriteRenderer node = Instantiate(Node);
                node.sortingOrder = order;
                order++;
                node.transform.position = GetGridPos(x, y);
                Grid[y, x] = node;
            }
        }
    }

    private void Update()
    {
        if (AutoIdlCharacters.Count > 0) AutoCharacterLogic();

        if (!isUImoving)
        {
            if (Input.GetMouseButtonDown(0)) OnTouch();
            else if (Input.GetMouseButton(0) && curDrag != null) OnDrag();
            else if (Input.GetMouseButtonUp(0) && curDrag != null) EndDrag();

            if (curDrag != null && curDrag is CharacterBuff)
            {
                CharacterBuff buff = curDrag.GetComponent<CharacterBuff>();
                InitGridColor();
                ShowBuffRange(buff);
            }
        }
    }

    public void ShowBuffRange(CharacterBuff buff)
    {

        Color color = buff.buffType == BuffType.FailDecrease ? Color.red : Color.green;

        foreach (var item in buff.BuffList)
        {
            int _x = item.x + buff.thisPosIdx.x;
            int _y = item.y + buff.thisPosIdx.y;

            if (_x < 0 || _y < 0 || _x >= x || _y >= y) continue;

            Grid[item.y + buff.thisPosIdx.y, item.x + buff.thisPosIdx.x].color = color;
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
        Vector2Int posidx = GetPosIdx(touchPos);

        if (GetAblePos(posidx.x, posidx.y, GridType.Filled))
        {
            Character nearby = GetTouchCharacter(touchPos);

            if (nearby != null)
            {
                SoundManager.Instance.PlayUISound("click");
                curDrag = nearby;
                nearby.isDrag = true;
                curDrag.BodySR.sortingOrder = 9;
                curDrag.HeadSR.sortingOrder = 10;
                isDragging = true;
            }
        }
        else if (GetAblePos(posidx.x, posidx.y, GridType.Locked))
        {
            TryUnlock();
        }
    }

    void TryUnlock()
    {
        // UI
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

        if (!GetAblePos(posIdx.x, posIdx.y, GridType.Locked))
        {
            Vector2Int cIdx = curDrag.thisPosIdx;
            Character posChar = GetIndexCharacter(posIdx.x, posIdx.y);

            if (posChar == null)
            {//move
                SoundManager.Instance.PlayUISound("add");
                AbleGrid[cIdx.y, cIdx.x] = GridType.Empty;
                CharacterGridInfo[cIdx.y, cIdx.x] = null;

                curDrag.transform.position = GetGridPos(posIdx.x, posIdx.y);

                AbleGrid[posIdx.y, posIdx.x] = GridType.Filled;
                CharacterGridInfo[posIdx.y, posIdx.x] = curDrag;
                curDrag.thisPosIdx = posIdx;
            }
            else
            {//swap
                SoundManager.Instance.PlayUISound("meow");
                CharacterGridInfo[cIdx.y, cIdx.x] = posChar;
                posChar.transform.position = GetGridPos(cIdx.x, cIdx.y);
                posChar.thisPosIdx = cIdx;

                curDrag.transform.position = GetGridPos(posIdx.x, posIdx.y);
                CharacterGridInfo[posIdx.y, posIdx.x] = curDrag;
                curDrag.thisPosIdx = posIdx;
            }
        }
        else
        {
            Debug.Log("not able");
            curDrag.transform.position = GetGridPos(curDrag.thisPosIdx.x, curDrag.thisPosIdx.y);
        }

        curDrag.BodySR.sortingOrder = 2;
        curDrag.HeadSR.sortingOrder = 3;
        curDrag = null;
        isDragging = false;

        InitGridColor();
        SetBuffCharacter();
    }

    public void GaugeReward(Character character, float failDecrease = 0f)
    {
        int idx = character.info.idx;
        var data = StaticDataManager.GetCatData(idx);

        if (GetRandom(data.devRates[0] - (data.devRates[0] * failDecrease * GameManager.Instance.failedRate)))
        {//실패
            SoundManager.Instance.PlayUISound("hammer");
            Destroy( Instantiate(SmokeEffect, character.transform.position, Quaternion.identity), 0.4f);
            GetReward(data.rewardRates[0], character.transform);
        }
        else if (GetRandom(data.devRates[2] + (data.devRates[0] * GameManager.Instance.successRate)))
        {//대성공
            SoundManager.Instance.PlayUISound("buff");
            SoundManager.Instance.PlayUISound("coin");
            GetReward(data.rewardRates[2], character.transform);
        }
        else
        {//성공
            SoundManager.Instance.PlayUISound("coin");
            GetReward(data.rewardRates[1], character.transform);
        }

        GameManager.Instance.gold += 1;
    }

    void GetReward(float value, Transform target)
    {
        Debug.Log(GameManager.Instance.rewardUp);
        GameManager.Instance.can += value + (value * GameManager.Instance.rewardUp);

        RewardTextBox temp = Instantiate(TextBox);
        temp.Init(string.Format("{0:0}", value), target.position + new Vector3(1, 1));
    }

    bool GetRandom(float value)
    {
        float rand = Random.Range(0f, 1f);
        if (rand <= value) return true;
        else
            return false;
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
            GameObject back = Instantiate(Background);

            back.transform.position = GetGridPos(1, y) + new Vector2(0, 1.5f);
            temp.transform.position = GetGridPos(1, y);
            LockList.Add(temp);

        }

        Instantiate(LooftTop, GetGridPos(1, AbleGrid.GetLength(0)) + new Vector2(-3.5f, -1.3f), Quaternion.identity);
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
                GridPos[y, x] = new Vector2(x * gridSize_x, y * gridSize_y) + GridOffset;
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

        AllCharacterPrefabs.Add(Bro);
        AllCharacterPrefabs.AddRange(Dev);
        AllCharacterPrefabs.AddRange(QA);
        AllCharacterPrefabs.AddRange(PM);

        var order = from ch in AllCharacterPrefabs
                    select ch;

        AllCharacterPrefabs = order.ToList().OrderBy(ch => ch.info.idx).ToList();
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

    public Character SpawnTypeCharacter(CharacterType type, Vector2Int pos, int idx = 0)
    {
        Vector2 spawnPos = GetGridPos(pos.x, pos.y);
        Character spawn = SpawnCharacter(AllCharacterPrefabs[idx], spawnPos);

        switch (type)
        {
            case CharacterType.Bro:

                Gauge gauge = Instantiate(gaugeObj, canvas.transform);
                gauge.transform.SetAsFirstSibling();

                ((CharacterTouchAble)spawn).InitGauge(gauge);

                TouchAbleCharacters.Add(spawn.GetComponent<ITouchAble>());
                GaugeCharacters.Add(spawn.GetComponent<IGauge>());

                break;
            case CharacterType.Developer:

                Gauge gauge1 = Instantiate(gaugeObj, canvas.transform);
                gauge1.transform.SetAsFirstSibling();

                ((CharacterIdle)spawn).InitGauge(gauge1);

                AutoIdlCharacters.Add(spawn.GetComponent<IAuto>());
                GaugeCharacters.Add(spawn.GetComponent<IGauge>());

                break;
            case CharacterType.QualityAssureance:

                IBuff qaBuff = spawn.GetComponent<IBuff>();

                qaBuff.Init(pos);
                QACharacters.Add(qaBuff);
                BuffCharacters.Add(qaBuff);

                break;
            case CharacterType.ProductManager:

                IBuff buff = spawn.GetComponent<IBuff>();
                buff.Init(pos);
                PMCharacters.Add(buff);
                BuffCharacters.Add(buff);

                break;
        }
        spawn.thisPosIdx = pos;

        CharacterGridInfo[pos.y, pos.x] = spawn;
        AbleGrid[pos.y, pos.x] = GridType.Filled;
        SetBuffCharacter();

        return spawn;
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
        Vector2 retVec = (new Vector2(x * gridSize_x, y * gridSize_y)) + GridOffset;
        return retVec;
    }

    public bool GetAblePos(int x, int y, GridType grid = GridType.Empty)
    {
        if (x < 0 || y < 0 || x >= AbleGrid.GetLength(1) || y >= AbleGrid.GetLength(0))
            return false;

        GridType type = AbleGrid[y, x];
        if (type == grid)
        {
            return true;
        }
        else return false;
    }

    public bool GetAblePos(Vector2 pos, GridType grid = GridType.Empty)
    {
        Vector2Int posIdx = GetPosIdx(pos);
        int x = posIdx.x;
        int y = posIdx.y;

        if (x < 0 || y < 0 || x >= AbleGrid.GetLength(1) || y >= AbleGrid.GetLength(0))
            return false;

        GridType type = AbleGrid[y, x];
        if (type == grid)
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

