using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SelectionBank<T> {

    List<Tuple<T, bool>> picks;
    public SelectionBank(T[] choices) {
        picks = new List<Tuple<T, bool>>();
        foreach(T item in choices) {
            picks.Add(new Tuple<T, bool>(item, true));
        }
    }


    public T GetNext(T o) {
        int index = picks.IndexOf(picks.First(x => x.Item1.Equals(o)));

        for(int i = index; i < picks.Count; i++) {
            if(picks[i].Item2) {
                picks[index] = new Tuple<T, bool>(picks[index].Item1, true);

                picks[i] = new Tuple<T, bool>(picks[i].Item1, false);
                return picks[i].Item1;
            }
        }
        return picks[index].Item1;
    }

    public T GetPrevious(T o) {
        int index = picks.IndexOf(picks.First(x => x.Item1.Equals(o)));

        for(int i = index; i >= 0; i--) {
            if(picks[i].Item2) {
                picks[index] = new Tuple<T, bool>(picks[index].Item1, true);

                picks[i] = new Tuple<T, bool>(picks[i].Item1, false);
                return picks[i].Item1;
            }
        }
        return picks[index].Item1;
    }

    public T GetClosest(int index) {

        for(int i = index; i < picks.Count; i++) {
            if(picks[i].Item2) {
                picks[i] = new Tuple<T, bool>(picks[i].Item1, false);
                return picks[i].Item1;
            }
        }
        picks[index] = new Tuple<T, bool>(picks[index].Item1, false);
        return picks[index].Item1;
    }

    public void Free(T o) {
        int index = picks.IndexOf(picks.First(x => x.Item1.Equals(o)));
        picks[index] = new Tuple<T, bool>(picks[index].Item1, true);
    }

}
