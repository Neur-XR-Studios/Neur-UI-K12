using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace K12.UI
{
    public struct CsvTableCoords
    {
        public int row;
        public int column;

        public CsvTableCoords(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        public void Set(int row, int column)
        {
            this.row = row;
            this.column = column;
        }
    }

}
