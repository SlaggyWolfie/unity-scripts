using System;

using Slaggy.Unity.Singletons;

public sealed class GameSettings : SingletonStandalone<GameSettings>
{
    private void Start()  { }
    private void Update() { }
}

[Serializable]
public class PrefsField
{
    public string prefName = string.Empty;

    public PrefsField() { }
    public PrefsField(string name) => prefName = name;
}

[Serializable]
public class IntPref : PrefsField { }
