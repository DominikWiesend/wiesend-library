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

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Wiesend.Core.Media.Procedural
{
    /// <summary>
    /// Helper class for generating cracks by midpoint displacement
    /// </summary>
    public static class MidpointDisplacement
    {
        /// <summary>
        /// Generates an image that contains cracks
        /// </summary>
        /// <param name="Width">Image width</param>
        /// <param name="Height">Image height</param>
        /// <param name="NumberOfCracks">Number of cracks</param>
        /// <param name="Iterations">Iterations</param>
        /// <param name="MaxChange">Maximum height change of the cracks</param>
        /// <param name="MaxLength">Maximum length of the cracks</param>
        /// <param name="Seed">Random seed</param>
        /// <returns>An image containing "cracks"</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public static SwiftBitmap Generate(int Width, int Height, int NumberOfCracks, int Iterations,
            int MaxChange, int MaxLength, int Seed)
        {
            if (!(NumberOfCracks >= 0)) throw new ArgumentException("Number of cracks should be greater than 0", nameof(NumberOfCracks));
            if (!(Width >= 0)) throw new ArgumentException("Width must be greater than or equal to 0", nameof(Width));
            if (!(Height >= 0)) throw new ArgumentException("Height must be greater than or equal to 0", nameof(Height));
            var ReturnValue = new SwiftBitmap(Width, Height);
            var Lines = GenerateLines(Width, Height, NumberOfCracks, Iterations, MaxChange, MaxLength, Seed);
            using (Graphics ReturnGraphic = Graphics.FromImage(ReturnValue.InternalBitmap))
            {
                foreach (Line Line in Lines)
                    foreach (Line SubLine in Line.SubLines)
                        ReturnGraphic.DrawLine(Pens.White, SubLine.X1, SubLine.Y1, SubLine.X2, SubLine.Y2);
            }
            return ReturnValue;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "<Pending>")]
        private static List<Line> GenerateLines(int Width, int Height, int NumberOfCracks, int Iterations, int MaxChange, int MaxLength, int Seed)
        {
            if (!(NumberOfCracks >= 0 && Width >= 0)) throw new ArgumentException($"Condition not met: [{nameof(NumberOfCracks)} >= 0 && {nameof(Width)} >= 0]", nameof(NumberOfCracks));
            var Lines = new List<Line>();
            var Generator = new System.Random(Seed);
            for (int x = 0; x < NumberOfCracks; ++x)
            {
                Line TempLine = null;
                int LineLength = 0;
                do
                {
                    TempLine = new Line(Generator.Next(0, Width), Generator.Next(0, Width),
                        Generator.Next(0, Height), Generator.Next(0, Height));
                    LineLength = (int)System.Math.Sqrt((double)((TempLine.X1 - TempLine.X2) * (TempLine.X1 - TempLine.X2))
                        + ((TempLine.Y1 - TempLine.Y2) * (TempLine.Y1 - TempLine.Y2)));
                } while (LineLength > MaxLength && LineLength <= 0);
                Lines.Add(TempLine);
                var TempLineList = new List<Line> { TempLine };
                for (int y = 0; y < Iterations; ++y)
                {
                    Line LineUsing = TempLineList[Generator.Next(0, TempLineList.Count)];
                    int XBreak = Generator.Next(LineUsing.X1, LineUsing.X2) + Generator.Next(-MaxChange, MaxChange);
                    int YBreak = 0;
                    if (LineUsing.Y1 > LineUsing.Y2)
                        YBreak = Generator.Next(LineUsing.Y2, LineUsing.Y1) + Generator.Next(-MaxChange, MaxChange);
                    else
                        YBreak = Generator.Next(LineUsing.Y1, LineUsing.Y2) + Generator.Next(-MaxChange, MaxChange);
                    var LineA = new Line(LineUsing.X1, XBreak, LineUsing.Y1, YBreak);
                    var LineB = new Line(XBreak, LineUsing.X2, YBreak, LineUsing.Y2);
                    TempLineList.Remove(LineUsing);
                    TempLineList.Add(LineA);
                    TempLineList.Add(LineB);
                }
                TempLine.SubLines = TempLineList;
            }
            return Lines;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1852:Seal internal types", Justification = "<Pending>")]
    internal class Line
    {
        public List<Line> SubLines = new();
        public int X1;
        public int X2;
        public int Y1;
        public int Y2;

        public Line()
        {
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0180:Use tuple to swap values", Justification = "<Pending>")]
        public Line(int X1, int X2, int Y1, int Y2)
        {
            if (X1 > X2)
            {
                int Holder = X1;
                X1 = X2;
                X2 = Holder;
                Holder = Y1;
                Y1 = Y2;
                Y2 = Holder;
            }
            this.X1 = X1;
            this.X2 = X2;
            this.Y1 = Y1;
            this.Y2 = Y2;
        }
    }
}