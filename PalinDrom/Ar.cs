using System;
using System.Collections;

// Thomas' emulation of JavaScript style arrays in C#
// v 0.2a, September 10, 2013

// Se API at: 
// http://www.nodebite.se/#ar-smarta-arrayer-systemutvecklare-bibliotek

// Changes from 0.1: Filter now works according to ECMA-standards
// returning a new filtered array
// Use FilterReduce for old behaviour: Removing elements 
// from original Array

// Please note that Each, Map, Filter and FilterReduce
// should have a method as an in-parameter
// The method in its turn should take a Dynamic as in-parameter
// and an optional int as second in-parameter
//
// The method you provide for Map should preferable return a Dynamic.
// For Each it should not return anything (void).
// For Filter and FilterReduce it has to return a bool.
//
// These methods will receive an element from the array
// and if an second in-parameter is provided Each and Map
// will receive the index as well.


class Ar : ArrayList
{

    public void Push(params dynamic[] args)
    {
        for (int i = 0; i < args.Length; i++)
        {
            this.Add(args[i]);
        }
    }

    public void Unshift(params dynamic[] args)
    {
        for (int i = 0; i < args.Length; i++)
        {
            this.Insert(0, args[i]);
        }
    }

    public dynamic Pop()
    {
        if (this.Count == 0) return null;
        var x = this[this.Count - 1];
        this.RemoveAt(this.Count - 1);
        return x;
    }

    public dynamic Shift()
    {
        if (this.Count == 0) return null;
        var x = this[0];
        this.RemoveAt(0);
        return x;
    }

    public dynamic Get(params int[] args)
    {
        dynamic result = this;
        for (var i = 0; i < args.Length; i++)
        {
            result = result[args[i]];
        }
        return result;
    }

    public void Set(params dynamic[] args)
    {
        dynamic result = this;
        for (var i = 0; i < args.Length - 2; i++)
        {
            result = result[args[i]];
        }
        result[args[args.Length - 2]] = args[args.Length - 1];
    }

    public int Length()
    {
        return this.Count;
    }

    public void Each(Action<dynamic> method)
    {
        foreach (dynamic item in this)
        {
            method(item);
        }
    }

    public void Each(Action<dynamic, int> method)
    {
        for (int i = 0; i < this.Count; i++)
        {
            method(this[i], i);
        }
    }

    public Ar Map(Func<dynamic, dynamic> method)
    {
        Ar newAr = new Ar();
        foreach (dynamic item in this)
        {
            newAr.Push(method(item));
        }
        return newAr;
    }

    public Ar Map(Func<dynamic, int, dynamic> method)
    {
        Ar newAr = new Ar();
        for (int i = 0; i < this.Count; i++)
        {
            newAr.Push(method(this.Get(i), i));
        }
        return newAr;
    }

    public void FilterReduce(Func<dynamic, bool> method)
    {
        for (int i = this.Count - 1; i >= 0; i--)
        {
            if (!method(this[i])) this.RemoveAt(i);
        }
    }

    public Ar Filter(Func<dynamic, bool> method)
    {
        Ar newAr = new Ar();
        for (int i = this.Count - 1; i >= 0; i--)
        {
            if (method(this[i])) newAr.Push(this[i]);
        }
        return newAr;
    }

    public bool Some(Func<dynamic, bool> method)
    {
        bool ok = false;
        for (int i = this.Count - 1; i >= 0; i--)
        {
            if (method(this[i])) ok = true;
        }
        return ok;
    }

    public string Join()
    {
        return this.Join("");
    }

    public string Join(string glue)
    {
        string x = "";
        foreach (dynamic item in this)
        {
            x += (x != "" ? glue : "") + item;
        }
        return x;
    }

    public bool Every(Func<dynamic, bool> method)
    {
        bool ok = true;
        for (int i = this.Count - 1; i >= 0; i--)
        {
            if (!method(this[i])) ok = false;
        }
        return ok;
    }

    public void Concat(params Ar[] args)
    {
        for (int i = 0; i < args.Length; i++)
        {
            foreach (dynamic item in args[i])
            {
                this.Push(item);
            }
        }
    }

    public Ar Slice(int start, int end)
    {
        Ar newAr = new Ar();
        if (start < 0) start = this.Count - start;
        if (end < 0) end = this.Count - end;
        for (int i = start; i < end; i++)
        {
            newAr.Push(this[i]);
        }
        return newAr;
    }

    public Ar Slice(int start)
    {
        return this.Slice(0, this.Count);
    }

    public Ar Splice(int start, int numberOf, params dynamic[] toAdd)
    {
        Ar newAr = new Ar();
        if (start < 0) start = this.Count + start;
        for (int i = start + numberOf - 1; i >= start; i--)
        {
            if (i >= this.Count) continue;
            newAr.Unshift(this[i]);
            this.RemoveAt(i);
        }
        for (int i = toAdd.Length - 1; i >= 0; i--)
        {
            this.Insert(start, toAdd[i]);
        }
        return newAr;
    }


    public Ar(params dynamic[] args)
        : base()
    {
        for (int i = 0; i < args.Length; i++)
        {
            this.Push(args[i]);
        }
    }

}