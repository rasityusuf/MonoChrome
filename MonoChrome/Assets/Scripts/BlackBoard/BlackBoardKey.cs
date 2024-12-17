using System;
using System.Collections;
using System.Collections.Generic;

public class BlackBoardKey : IEquatable<BlackBoardKey>
{
    public string name { get; private set; }

    public BlackBoardKey(string name)
    {
        this.name = name;
    }

    public override bool Equals(object obj) => obj is BlackBoardKey passedValue && Equals(passedValue);

    public bool Equals(BlackBoardKey other) => other == this;

    public override string ToString() => this.name;

    public override int GetHashCode() => base.GetHashCode();

    public static bool operator ==(BlackBoardKey a, BlackBoardKey b) => a.name == b.name;
    public static bool operator !=(BlackBoardKey a, BlackBoardKey b) => !(a == b);
}
