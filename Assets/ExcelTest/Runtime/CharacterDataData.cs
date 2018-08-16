using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class CharacterDataData
{
  [SerializeField]
  string characteruniquename;
  public string Characteruniquename { get {return characteruniquename; } set { characteruniquename = value;} }
  
  [SerializeField]
  string avatarobjectprefab;
  public string Avatarobjectprefab { get {return avatarobjectprefab; } set { avatarobjectprefab = value;} }
  
  [SerializeField]
  float horizontalspeed;
  public float Horizontalspeed { get {return horizontalspeed; } set { horizontalspeed = value;} }
  
  [SerializeField]
  float verticalspeed;
  public float Verticalspeed { get {return verticalspeed; } set { verticalspeed = value;} }
  
  [SerializeField]
  string idleactionkey;
  public string Idleactionkey { get {return idleactionkey; } set { idleactionkey = value;} }
  
  [SerializeField]
  string deathactionkey;
  public string Deathactionkey { get {return deathactionkey; } set { deathactionkey = value;} }
  
  [SerializeField]
  string walkactionkey;
  public string Walkactionkey { get {return walkactionkey; } set { walkactionkey = value;} }
  
  [SerializeField]
  string runactionkey;
  public string Runactionkey { get {return runactionkey; } set { runactionkey = value;} }
  
  [SerializeField]
  string battleactionlist;
  public string Battleactionlist { get {return battleactionlist; } set { battleactionlist = value;} }
  
}