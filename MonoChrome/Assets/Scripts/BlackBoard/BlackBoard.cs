
using System.Collections.Generic;


public class BlackBoard 
{
    Dictionary<string, BlackBoardKey> storedKeys = new();
    Dictionary<BlackBoardKey,object> storedValues = new();

    private BlackBoardKey RegisterOrGetKey(string name)
    {
        if(!storedKeys.TryGetValue(name,out BlackBoardKey key))
        {
            key = new BlackBoardKey(name);

            storedKeys[name] = key;
        }

        return key;
         
    }
    
    public void UnRegisterKey(string name)
    {
        if (storedKeys.ContainsKey(name))
        {
            return;
        }

        storedKeys.Remove(name);
    }

    public void SetValue<T>(string key,T value)
    {
        BlackBoardKey bKey = RegisterOrGetKey(key); 
        if(storedValues.TryGetValue(bKey,out object equalVal) && equalVal is BlackBoardEntry<T> passedVal)
        {
            storedValues[bKey] = passedVal.Value;
        }
        else
        {
            BlackBoardEntry<T> newEntry = new BlackBoardEntry<T>(bKey, value);
            storedValues[bKey] = newEntry;
        }
    }

    public bool GetValue<T>(string key, out T value)
    {
        BlackBoardKey bKey = RegisterOrGetKey(key);

        if (storedValues.TryGetValue(bKey,out object equalVal) && equalVal is BlackBoardEntry<T> passedVal)
        {
            value = passedVal.Value;
            return true;
        }

        value = default;
        return false;

    }

}


