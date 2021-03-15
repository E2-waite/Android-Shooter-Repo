using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Structure;

public partial class Templates
{
    public Template blank = new Template
    {
        layout = new int[5, 5]
        {
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0},
            {0,0,0,0,0}
        }
    };

    public List<Template> template = new List<Template>()
    {
        new Template
        {
            layout = new int[5,5]
            {
                {1,1,1,1,1},
                {1,1,1,1,1},
                {1,1,1,1,1},
                {1,1,1,1,1},
                {1,1,1,1,1}
            }
        },
        new Template
        {
            layout = new int[5,5]
            {
                {1,1,1,1,1},
                {1,1,1,1,1},
                {1,1,1,1,1},
                {1,1,1,1,1},
                {1,1,1,1,1}
            }
        },
        new Template
        {
            layout = new int[5,5]
            {
                {1,1,1,1,1},
                {1,1,1,1,1},
                {1,1,1,1,1},
                {1,1,1,1,1},
                {1,1,1,1,1}
            }
        },
        new Template
        {
            layout = new int[5,5]
            {
                {1,1,1,1,1},
                {1,2,2,2,1},
                {1,2,2,2,1},
                {2,2,2,2,1},
                {2,2,1,1,1}
            }
        },
        new Template
        {
            layout = new int[5,5]
            {
                {1,1,1,1,1},
                {1,2,2,2,1},
                {1,2,2,2,1},
                {1,2,2,2,1},
                {1,1,1,1,1}
            }
        },
        new Template
        {
            layout = new int[5,5]
            {
                {1,1,1,2,2},
                {1,1,1,1,2},
                {1,1,1,1,1},
                {2,1,1,1,1},
                {2,2,1,1,1}
            }
        },
        new Template
        {
            layout = new int[5,5]
            {
                {2,1,1,1,2},
                {1,1,2,1,1},
                {1,2,2,2,1},
                {1,1,2,1,1},
                {2,1,1,1,2}
            }
        },
    };
}
