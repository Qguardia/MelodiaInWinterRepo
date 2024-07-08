using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sheet Ability", menuName = "SheetMusic")]
public class BaseSheetMusic : ScriptableObject
{
    public new string name;
    public string description;

    public int SheetDuration;
    public int SoundRange;

}
