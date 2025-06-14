﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core
{
  public class KeyEqualityComparer<T> : IEqualityComparer<T>
  {
    private readonly Func<T, object> keyExtractor;

    public KeyEqualityComparer(Func<T, object> keyExtractor)
    {
      this.keyExtractor = keyExtractor;
    }

    public bool Equals(T x, T y)
    {
      return this.keyExtractor(x).Equals(this.keyExtractor(y));
    }

    public int GetHashCode(T obj)
    {
      return this.keyExtractor(obj).GetHashCode();
    }
  }
}
