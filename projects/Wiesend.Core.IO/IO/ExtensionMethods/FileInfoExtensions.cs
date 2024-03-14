#region Project Description [About this]
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
using System.ComponentModel;
using System.IO;
using System.Text;
using Wiesend.Core.DataTypes;

namespace Wiesend.Core.IO
{
    /// <summary>
    /// File extensions
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class FileInfoExtensions
    {
        /// <summary>
        /// Reads the file in as a string
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="File"/> is null.</exception>
        /// <returns>The file content as a string if it exists; otherwise an empty string.</returns>
        public static string Read([NotNull] this FileInfo File)
        {
            if (File is null) throw new ArgumentNullException(nameof(File));
            File.Refresh();
            if (!File.Exists)
                return "";
            using StreamReader Reader = File.OpenText();
            return Reader.ReadToEnd();
        }

        /// <summary>
        /// Reads a file as binary
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="File"/> is null.</exception>
        /// <returns>The file content as a byte array if it exists; otherwise an empty byte array.</returns>
        public static byte[] ReadBinary([NotNull] this FileInfo File)
        {
            if (File is null) throw new ArgumentNullException(nameof(File));
            File.Refresh();
            if (!File.Exists)
            {
#if NET45
                return new byte[0];
#else
                return Array.Empty<byte>();
#endif
            }
            using FileStream Reader = File.OpenRead();
            byte[] Buffer = new byte[1024];
            using MemoryStream Temp = new();
            while (true)
            {
                var Count = Reader.Read(Buffer, 0, Buffer.Length);
                if (Count <= 0)
                    return Temp.ToArray();
                Temp.Write(Buffer, 0, Count);
            }
        }

        /// <summary>
        /// Writes content to the file
        /// </summary>
        /// <param name="File">File to write to</param>
        /// <param name="Content">Content to write</param>
        /// <param name="Mode">Mode to open the file as</param>
        /// <param name="Encoding">Encoding to use for the content</param>
        /// <exception cref="ArgumentNullException"><paramref name="File"/> is null.</exception>
        /// <returns>The content written to the file as string.</returns>
        public static string Write([NotNull] this FileInfo File, string Content, System.IO.FileMode Mode = FileMode.Create, Encoding Encoding = null)
        {
            if (File is null) throw new ArgumentNullException(nameof(File));
            Content ??= "";
            Encoding ??= new ASCIIEncoding();
            return File.Write(Encoding.GetBytes(Content), Mode).ToString(Encoding);
        }

        /// <summary>
        /// Writes content to the file
        /// </summary>
        /// <param name="File">File to write to</param>
        /// <param name="Content">Content to write</param>
        /// <param name="Mode">Mode to open the file as</param>
        /// <exception cref="ArgumentNullException"><paramref name="File"/> is null.</exception>
        /// <returns>The content written to the file as byte array.</returns>
        public static byte[] Write([NotNull] this FileInfo File, byte[] Content, System.IO.FileMode Mode = FileMode.Create)
        {
            if (File is null) throw new ArgumentNullException(nameof(File));
            if (Content == null)
            {
#if NET45
                Content = new byte[0];
#else
                Content = Array.Empty<byte>();
#endif
            }
            File.Directory.Create();
            using (FileStream Writer = File.Open(Mode, FileAccess.Write))
                Writer.Write(Content, 0, Content.Length);
            File.Refresh();
            return Content;
        }
    }
}