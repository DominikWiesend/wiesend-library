﻿#region Project Description [About this]
// =================================================================================
//            The whole Project is Licensed under the MIT License
// =================================================================================
// =================================================================================
//    Wiesend's Dynamic Link Library is a collection of reusable code that 
//    I've written, or found throughout my programming career. 
//
//    I tried my very best to mention all of the original copyright holders. 
//    I hope that all code which I've copied from others is mentioned and all 
//    their copyrights are given. The copied code (or code snippets) could 
//    already be modified by me or others.
// =================================================================================
#endregion of Project Description
#region Original Source Code [Links to all original source code]
// =================================================================================
//          Original Source Code [Links to all original source code]
// =================================================================================
// https://github.com/JaCraig/Craig-s-Utility-Library
// =================================================================================
//    I didn't wrote this source totally on my own, this class could be nearly a 
//    clone of the project of James Craig, I did highly get inspired by him, so 
//    this piece of code isn't totally mine, shame on me, I'm not the best!
// =================================================================================
#endregion of where is the original source code
#region Licenses [MIT Licenses]
#region MIT License [James Craig]
// =================================================================================
//    Copyright(c) 2014 <a href="http://www.gutgames.com">James Craig</a>
//    
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the "Software"), to deal
//    in the Software without restriction, including without limitation the rights
//    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the Software is
//    furnished to do so, subject to the following conditions:
//    
//    The above copyright notice and this permission notice shall be included in all
//    copies or substantial portions of the Software.
//    
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//    SOFTWARE.
// =================================================================================
#endregion of MIT License [James Craig] 
#region MIT License [Dominik Wiesend]
// =================================================================================
//    Copyright(c) 2018 Dominik Wiesend. All rights reserved.
//    
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the "Software"), to deal
//    in the Software without restriction, including without limitation the rights
//    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the Software is
//    furnished to do so, subject to the following conditions:
//    
//    The above copyright notice and this permission notice shall be included in all
//    copies or substantial portions of the Software.
//    
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//    SOFTWARE.
// =================================================================================
#endregion of MIT License [Dominik Wiesend] 
#endregion of Licenses [MIT Licenses]

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Wiesend.Core.IO.FileFormats.Delimited
{
    /// <summary>
    /// Individual row within a delimited file
    /// </summary>
    public class Row : IList<Cell>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Row()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Delimiter">Delimiter to parse the individual cells</param>
        public Row(string Delimiter)
        {
            this.Cells = new List<Cell>();
            this.Delimiter = Delimiter;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Content">Content of the row</param>
        /// <param name="Delimiter">Delimiter to parse the individual cells</param>
        public Row([NotNull] string Content, [NotNull] string Delimiter)
        {
            if (Content == null) throw new ArgumentNullException(nameof(Content));
            if (string.IsNullOrEmpty(Delimiter)) throw new ArgumentNullException(nameof(Delimiter));
            this.Cells = new List<Cell>();
            this.Delimiter = Delimiter;
            var TempSplitter = new Regex(string.Format(CultureInfo.InvariantCulture, "(?<Value>\"(?:[^\"]|\"\")*\"|[^{0}\r\n]*?)(?<Delimiter>{0}|\r\n|\n|$)", Regex.Escape(Delimiter)));
            var Matches = TempSplitter.Matches(Content);
            bool Finished = false;
            foreach (Match Match in Matches.Cast<Match>())
            {
                if (!Finished)
                {
                    Cells.Add(new Cell(Match.Groups["Value"].Value));
                }
                Finished = string.IsNullOrEmpty(Match.Groups["Delimiter"].Value) || Match.Groups["Delimiter"].Value == "\r\n" || Match.Groups["Delimiter"].Value == "\n";
            }
        }

        /// <summary>
        /// Number of Cells
        /// </summary>
        public int Count
        {
            get { return Cells.Count; }
        }

        /// <summary>
        /// Delimiter used
        /// </summary>
        public string Delimiter { get; set; }

        /// <summary>
        /// Is the file read only
        /// </summary>
        public bool IsReadOnly
        {
            get { return Cells.IsReadOnly; }
        }

        /// <summary>
        /// Cells within the row
        /// </summary>
        protected IList<Cell> Cells { get; private set; }

        /// <summary>
        /// Returns a cell within the row
        /// </summary>
        /// <param name="Position">The position of the cell</param>
        /// <returns>The specified cell</returns>
        public Cell this[int Position]
        {
            get { return Cells[Position]; }
            set { Cells[Position] = value; }
        }

        /// <summary>
        /// Adds a Cell to the file
        /// </summary>
        /// <param name="item">Cell to add</param>
        public void Add(Cell item)
        {
            Cells.Add(item);
        }

        /// <summary>
        /// Clears the file
        /// </summary>
        public void Clear()
        {
            Cells.Clear();
        }

        /// <summary>
        /// Determines if the file contains a Cell
        /// </summary>
        /// <param name="item">Cell to check for</param>
        /// <returns>True if it does, false otherwise</returns>
        public bool Contains(Cell item)
        {
            return Cells.Contains(item);
        }

        /// <summary>
        /// Copies the delimited file to an array
        /// </summary>
        /// <param name="array">Array to copy to</param>
        /// <param name="arrayIndex">Index to start at</param>
        public void CopyTo(Cell[] array, int arrayIndex)
        {
            Cells.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the enumerator for the delimited file
        /// </summary>
        /// <returns>The enumerator for this file</returns>
        public IEnumerator<Cell> GetEnumerator()
        {
            return Cells.GetEnumerator();
        }

        /// <summary>
        /// Index of a specific Cell
        /// </summary>
        /// <param name="item">Cell to search for</param>
        /// <returns>The index of a specific Cell</returns>
        public int IndexOf(Cell item)
        {
            return Cells.IndexOf(item);
        }

        /// <summary>
        /// Inserts a Cell at a specific index
        /// </summary>
        /// <param name="index">Index to insert at</param>
        /// <param name="item">Cell to insert</param>
        public void Insert(int index, Cell item)
        {
            Cells.Insert(index, item);
        }

        /// <summary>
        /// Removes a Cell from the file
        /// </summary>
        /// <param name="item">Cell to remove</param>
        /// <returns>True if it is removed, false otherwise</returns>
        public bool Remove(Cell item)
        {
            return Cells.Remove(item);
        }

        /// <summary>
        /// Removes a Cell at a specific index
        /// </summary>
        /// <param name="index">Index of the Cell to remove</param>
        public void RemoveAt(int index)
        {
            Cells.RemoveAt(index);
        }

        /// <summary>
        /// Gets the enumerator for the delimited file
        /// </summary>
        /// <returns>The enumerator for this file</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Cells.GetEnumerator();
        }

        /// <summary>
        /// To string function
        /// </summary>
        /// <returns>The content of the row in string form</returns>
        public override string ToString()
        {
            var Builder = new StringBuilder();
            string Seperator = "";
            foreach (Cell CurrentCell in Cells)
            {
                Builder.Append(Seperator).Append(CurrentCell);
                Seperator = Delimiter;
            }
            return Builder.Append(System.Environment.NewLine).ToString();
        }
    }
}