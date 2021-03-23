using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Structure
{
    public class Template
    {
        public int[,] layout;
    }

    public enum Type : int
    {
        space = 0,
        dirt = 1,
        stone = 2,
        floor = 3
    }

    public enum Dir
    {
        N = 1, 
        E = 2, 
        S = 4, 
        W = 8
    }
}
