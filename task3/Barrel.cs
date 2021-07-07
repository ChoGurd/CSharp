using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace task3
{
    class Barrel
    {
        private int maxLiter;
        public int MaxLiter
        {
            get
            {
                return maxLiter;
            }
            set
            {
                maxLiter = value;
            }
        }

        private int curLiter;
        public int CurLiter
        {
            get
            {
                return curLiter;
            }
            set
            {
                curLiter = value;
            }
        }

        public bool TopUp(int value) //заливаем воду
        {
            if (MaxLiter >= curLiter + value)
                CurLiter += value;
            else
                return false;
            return true;
        }

        public bool Drop(int value) //черпаем воду
        {
            if (curLiter - value >= 0)
                CurLiter -= value;
            else
                return false;
            return true;
        }
    }
}
