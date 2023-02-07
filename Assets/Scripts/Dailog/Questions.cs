using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Questions
{
  public string name;

  [TextArea(3, 10)]
  public string questions;
  [TextArea(3, 10)]
  public string[] answers;

  
}
