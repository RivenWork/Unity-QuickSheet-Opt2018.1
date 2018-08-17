using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class ActionDataData
{
  [SerializeField]
  string idname;
  public string Idname { get {return idname; } set { idname = value;} }
  
  [SerializeField]
  string name;
  public string Name { get {return name; } set { name = value;} }
  
  [SerializeField]
  int actiontype;
  public int Actiontype { get {return actiontype; } set { actiontype = value;} }
  
  [SerializeField]
  float colddown;
  public float Colddown { get {return colddown; } set { colddown = value;} }
  
  [SerializeField]
  float distance;
  public float Distance { get {return distance; } set { distance = value;} }
  
  [SerializeField]
  bool loop;
  public bool Loop { get {return loop; } set { loop = value;} }
  
  [SerializeField]
  int targettype;
  public int Targettype { get {return targettype; } set { targettype = value;} }
  
  [SerializeField]
  int rangetype;
  public int Rangetype { get {return rangetype; } set { rangetype = value;} }
  
  [SerializeField]
  int rangesize;
  public int Rangesize { get {return rangesize; } set { rangesize = value;} }
  
  [SerializeField]
  int rangemaxnumber;
  public int Rangemaxnumber { get {return rangemaxnumber; } set { rangemaxnumber = value;} }
  
  [SerializeField]
  int breakpoint;
  public int Breakpoint { get {return breakpoint; } set { breakpoint = value;} }
  
  [SerializeField]
  float nextactionavailabletimebegin;
  public float Nextactionavailabletimebegin { get {return nextactionavailabletimebegin; } set { nextactionavailabletimebegin = value;} }
  
  [SerializeField]
  float nextactionavailabletimeend;
  public float Nextactionavailabletimeend { get {return nextactionavailabletimeend; } set { nextactionavailabletimeend = value;} }
  
  [SerializeField]
  string animatorstatename;
  public string Animatorstatename { get {return animatorstatename; } set { animatorstatename = value;} }
  
  [SerializeField]
  string[] timeeventlist = new string[0];
  public string[] Timeeventlist { get {return timeeventlist; } set { timeeventlist = value;} }
  
  [SerializeField]
  string[] updateeventlist = new string[0];
  public string[] Updateeventlist { get {return updateeventlist; } set { updateeventlist = value;} }
  
  [SerializeField]
  string[] interruptevent = new string[0];
  public string[] Interruptevent { get {return interruptevent; } set { interruptevent = value;} }
  
}