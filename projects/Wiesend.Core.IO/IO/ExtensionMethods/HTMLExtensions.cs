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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Wiesend.Core.DataTypes;

namespace Wiesend.Core.IO
{
    /// <summary>
    /// Defines the type of data that is being minified
    /// </summary>
    public enum MinificationType
    {
        /// <summary>
        /// CSS
        /// </summary>
        CSS,

        /// <summary>
        /// Javascript
        /// </summary>
        JavaScript,

        /// <summary>
        /// HTML
        /// </summary>
        HTML
    }

    /// <summary>
    /// Extensions dealing with minification of data
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class HTMLExtensions
    {
        /// <summary>
        /// Combines and minifies various files
        /// </summary>
        /// <param name="Input">input strings (file contents)</param>
        /// <param name="Type">Type of minification</param>
        /// <returns>A minified/packed string</returns>
        public static string Minify([NotNull] this IEnumerable<string> Input, MinificationType Type = MinificationType.HTML)
        {
            if (Input == null) throw new ArgumentNullException(nameof(Input));
            return Minify(Input.ToString(x => x, System.Environment.NewLine), Type);
        }

        /// <summary>
        /// Combines and minifies various files
        /// </summary>
        /// <param name="Input">input strings (file contents)</param>
        /// <param name="Type">Type of minification</param>
        /// <returns>A minified/packed string</returns>
        public static string Minify([NotNull] this IEnumerable<FileInfo> Input, MinificationType Type = MinificationType.HTML)
        {
            if (Input == null) throw new ArgumentNullException(nameof(Input));
            return Minify(Input.Where(x => x.Exists).ToString(x => x.Read(), System.Environment.NewLine), Type);
        }

        /// <summary>
        /// Minifies the file based on the data type specified
        /// </summary>
        /// <param name="Input">Input text</param>
        /// <param name="Type">Type of minification to run</param>
        /// <returns>A stripped file</returns>
        public static string Minify(this string Input, MinificationType Type = MinificationType.HTML)
        {
            if (string.IsNullOrEmpty(Input))
                return "";
            if (Type == MinificationType.CSS)
                return CSSMinify(Input);
            if (Type == MinificationType.JavaScript)
                return JavaScriptMinify(Input);
            return HTMLMinify(Input);
        }

        /// <summary>
        /// Minifies the file based on the data type specified
        /// </summary>
        /// <param name="Input">Input file</param>
        /// <param name="Type">Type of minification to run</param>
        /// <returns>A stripped file</returns>
        public static string Minify([NotNull] this FileInfo Input, MinificationType Type = MinificationType.HTML)
        {
            if (Input == null) throw new ArgumentNullException(nameof(Input));
            if (!(Input.Exists)) throw new System.IO.FileNotFoundException(nameof(Input), "Input file does not exist");
            return Input.Read().Minify(Type);
        }

        private static string CSSMinify(string Input)
        {
            if (string.IsNullOrEmpty(Input))
                return "";
            Input = Regex.Replace(Input, @"(/\*\*/)|(/\*[^!][\s\S]*?\*/)", string.Empty);
            Input = Regex.Replace(Input, @"\s+", " ");
            Input = Regex.Replace(Input, @"(\s([\{:,;\}\(\)]))", "$2");
            Input = Regex.Replace(Input, @"(([\{:,;\}\(\)])\s)", "$2");
            Input = Regex.Replace(Input, ":0 0 0 0;", ":0;");
            Input = Regex.Replace(Input, ":0 0 0;", ":0;");
            Input = Regex.Replace(Input, ":0 0;", ":0;");
            Input = Regex.Replace(Input, ";}", "}");
            Input = Regex.Replace(Input, @"(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,}(?=&nbsp;)|(?<=&nbsp;)\s{2,}(?=[<])", string.Empty);
            Input = Regex.Replace(Input, @"([!{}:;>+([,])\s+", "$1");
            Input = Regex.Replace(Input, @"([\s:])(0)(px|em|%|in|cm|mm|pc|pt|ex)", "$1$2");
            Input = Regex.Replace(Input, "background-position:0", "background-position:0 0");
            Input = Regex.Replace(Input, @"(:|\s)0+\.(\d+)", "$1.$2");
            Input = Regex.Replace(Input, @"[^\}]+\{;\}", "");
            return Input;
        }

        private static string Evaluate([NotNull] Match Matcher)
        {
            if (Matcher == null) throw new ArgumentNullException(nameof(Matcher));
            var MyString = Matcher.ToString();
            if (string.IsNullOrEmpty(MyString))
                return "";
            MyString = Regex.Replace(MyString, @"\r\n\s*", "");
            return MyString;
        }

        private static string HTMLMinify(string Input)
        {
            if (string.IsNullOrEmpty(Input))
                return "";
            Input = Regex.Replace(Input, "/// <.+>", "");
            if (string.IsNullOrEmpty(Input))
                return "";
            Input = Regex.Replace(Input, @">[\s\S]*?<", new MatchEvaluator(Evaluate));
            return Input;
        }

        private static string JavaScriptMinify(string Input)
        {
            if (string.IsNullOrEmpty(Input))
                return "";
            var CodeLines = Input.Split(new string[] { System.Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var Builder = new StringBuilder();
            foreach (string Line in CodeLines)
            {
                var Temp = Line.Trim();
                if (Temp.Length > 0 && !Temp.StartsWith("//", StringComparison.InvariantCulture))
                    Builder.AppendLine(Temp);
            }

            Input = Builder.ToString();
            Input = Regex.Replace(Input, @"(/\*\*/)|(/\*[^!][\s\S]*?\*/)", string.Empty);
            Input = Regex.Replace(Input, @"^[\s]+|[ \f\r\t\v]+$", String.Empty);
            Input = Regex.Replace(Input, @"^[\s]+|[ \f\r\t\v]+$", String.Empty);
            Input = Regex.Replace(Input, @"([+-])\n\1", "$1 $1");
            Input = Regex.Replace(Input, @"([^+-][+-])\n", "$1");
            Input = Regex.Replace(Input, @"([^+]) ?(\+)", "$1$2");
            Input = Regex.Replace(Input, @"(\+) ?([^+])", "$1$2");
            Input = Regex.Replace(Input, @"([^-]) ?(\-)", "$1$2");
            Input = Regex.Replace(Input, @"(\-) ?([^-])", "$1$2");
            Input = Regex.Replace(Input, @"\n([{}()[\],<>/*%&|^!~?:=.;+-])", "$1");
            Input = Regex.Replace(Input, @"(\W(if|while|for)\([^{]*?\))\n", "$1");
            Input = Regex.Replace(Input, @"(\W(if|while|for)\([^{]*?\))((if|while|for)\([^{]*?\))\n", "$1$3");
            Input = Regex.Replace(Input, @"([;}]else)\n", "$1 ");
            Input = Regex.Replace(Input, @"(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,}(?=&nbsp;)|(?<=&nbsp;)\s{2,}(?=[<])", String.Empty);

            return Input;
        }
    }
}