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

public class InGameManager : Singleton<InGameManager>
{
    [Header("Characters")]
    public Character Bro;
    public Character[] Dev;
    public Character[] QA;
    public Character[] PM;

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
