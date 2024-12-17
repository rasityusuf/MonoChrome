using System;
using System.Collections;
using System.Collections.Generic;


public class BlackBoardEntry<T>
{
    public BlackBoardKey key { get; set; }
    public T Value { get; set; }
    public Type valueType { get; set; }
    public BlackBoardEntry(BlackBoardKey key, T value)
    {
        this.key = key;
        this.Value = value;
        this.valueType = Value.GetType();
    }

    public override bool Equals(object obj) => obj is BlackBoardEntry<T> value && value.key == this.key;
    public override int GetHashCode() => key.GetHashCode();


}
