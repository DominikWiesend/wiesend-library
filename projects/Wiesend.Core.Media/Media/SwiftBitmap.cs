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
using System.Collections.Concurrent;
using System.Collections.Generic;
using JetBrains.Annotations;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wiesend.Core.DataTypes;
using Wiesend.Core.DataTypes.Patterns.BaseClasses;

namespace Wiesend.Core.Media
{
    /// <summary>
    /// Bitmap wrapper. Helps make Bitmap access faster and a bit simpler.
    /// </summary>
    public class SwiftBitmap : SafeDisposableBaseClass, ICloneable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SwiftBitmap"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public SwiftBitmap([NotNull] string fileName)
            : this(new Bitmap(fileName))
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException(nameof(fileName));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwiftBitmap"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public SwiftBitmap(int width, int height)
            : this(new Bitmap(width, height))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwiftBitmap"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public SwiftBitmap([NotNull] Stream stream)
            : this(new Bitmap(stream))
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwiftBitmap"/> class.
        /// </summary>
        /// <param name="image">The image.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public SwiftBitmap([NotNull] Image image)
            : this(new Bitmap(image))
        {
            if (image == null) throw new ArgumentNullException(nameof(image));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwiftBitmap"/> class.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0016:Use 'throw' expression", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public SwiftBitmap([NotNull] Bitmap bitmap)
        {
            if (bitmap == null) throw new ArgumentNullException(nameof(bitmap));
            InternalBitmap = bitmap;
            Height = InternalBitmap.Height;
            Width = InternalBitmap.Width;
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height { get; private set; }

        /// <summary>
        /// Gets a palette listing in HTML string format
        /// </summary>
        /// <value>A list containing HTML color values (ex: #041845)</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1065:Do not raise exceptions in unexpected locations", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public IEnumerable<string> HTMLPalette
        {
            get
            {
                if (InternalBitmap == null) throw new NullReferenceException($"Condition not met: [{nameof(InternalBitmap)} != null]");
                if (htmlPalette_ != null)
                    return htmlPalette_;
                if (InternalBitmap.Palette != null && InternalBitmap.Palette.Entries.Length > 0)
                {
                    return InternalBitmap.Palette.Entries.Distinct((x, y) => x == y).Select(x => ColorTranslator.ToHtml(x));
                }
                var ReturnArray = new ConcurrentDictionary<string, int>();
                Lock();
                Parallel.For(0, Width, x =>
                {
                    for (int y = 0; y < Height; ++y)
                    {
                        ReturnArray.AddOrUpdate(ColorTranslator.ToHtml(GetPixel(x, y)),
                            z => 0,
                            (z, a) => 0);
                    }
                });
                Unlock();
                htmlPalette_ = ReturnArray.Keys;
                return htmlPalette_;
            }
        }

        /// <summary>
        /// Gets the internal Bitmap.
        /// </summary>
        /// <value>The internal Bitmap.</value>
        public Bitmap InternalBitmap { get; private set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The data.</value>
        protected BitmapData Data { get; private set; }

        /// <summary>
        /// Gets the data pointer.
        /// </summary>
        /// <value>The data pointer.</value>
        [CLSCompliant(false)]
        protected unsafe byte* DataPointer { get; private set; }

        /// <summary>
        /// Gets the pixel size (in bytes)
        /// </summary>
        /// <value>The size of the pixel.</value>
        protected int PixelSize { get; private set; }

        private IEnumerable<string> htmlPalette_;

        /// <summary>
        /// Implements the operator &amp;.
        /// </summary>
        /// <param name="Image1">The first image.</param>
        /// <param name="Image2">The second image</param>
        /// <returns>The result of the operator.</returns>
        public static SwiftBitmap operator &([NotNull] SwiftBitmap Image1, [NotNull] SwiftBitmap Image2)
        {
            if (Image1 == null) throw new ArgumentNullException(nameof(Image1));
            if (Image2 == null) throw new ArgumentNullException(nameof(Image2));
            Image1.Lock();
            Image2.Lock();
            var Result = new SwiftBitmap(Image1.Width, Image1.Height);
            Result.Lock();
            Parallel.For(0, Result.Width, x =>
            {
                for (int y = 0; y < Result.Height; ++y)
                {
                    var Pixel1 = Image1.GetPixel(x, y);
                    var Pixel2 = Image2.GetPixel(x, y);
                    Result.SetPixel(x, y,
                        Color.FromArgb(Pixel1.R & Pixel2.R,
                            Pixel1.G & Pixel2.G,
                            Pixel1.B & Pixel2.B));
                }
            });
            Image2.Unlock();
            Image1.Unlock();
            return Result.Unlock();
        }

        /// <summary>
        /// Implements the operator ^.
        /// </summary>
        /// <param name="Image1">The first image.</param>
        /// <param name="Image2">The second image</param>
        /// <returns>The result of the operator.</returns>
        public static SwiftBitmap operator ^([NotNull] SwiftBitmap Image1, [NotNull] SwiftBitmap Image2)
        {
            if (Image1 == null) throw new ArgumentNullException(nameof(Image1));
            if (Image2 == null) throw new ArgumentNullException(nameof(Image2));
            Image1.Lock();
            Image2.Lock();
            var Result = new SwiftBitmap(Image1.Width, Image1.Height);
            Result.Lock();
            Parallel.For(0, Result.Width, x =>
            {
                for (int y = 0; y < Result.Height; ++y)
                {
                    var Pixel1 = Image1.GetPixel(x, y);
                    var Pixel2 = Image2.GetPixel(x, y);
                    Result.SetPixel(x, y,
                        Color.FromArgb(Pixel1.R ^ Pixel2.R,
                            Pixel1.G ^ Pixel2.G,
                            Pixel1.B ^ Pixel2.B));
                }
            });
            Image2.Unlock();
            Image1.Unlock();
            return Result.Unlock();
        }

        /// <summary>
        /// Implements the operator |.
        /// </summary>
        /// <param name="Image1">The first image.</param>
        /// <param name="Image2">The second image</param>
        /// <returns>The result of the operator.</returns>
        public static SwiftBitmap operator |([NotNull] SwiftBitmap Image1, [NotNull] SwiftBitmap Image2)
        {
            if (Image1 == null) throw new ArgumentNullException(nameof(Image1));
            if (Image2 == null) throw new ArgumentNullException(nameof(Image2));
            Image1.Lock();
            Image2.Lock();
            var Result = new SwiftBitmap(Image1.Width, Image1.Height);
            Result.Lock();
            Parallel.For(0, Result.Width, x =>
            {
                for (int y = 0; y < Result.Height; ++y)
                {
                    var Pixel1 = Image1.GetPixel(x, y);
                    var Pixel2 = Image2.GetPixel(x, y);
                    Result.SetPixel(x, y,
                        Color.FromArgb(Pixel1.R | Pixel2.R,
                            Pixel1.G | Pixel2.G,
                            Pixel1.B | Pixel2.B));
                }
            });
            Image2.Unlock();
            Image1.Unlock();
            return Result.Unlock();
        }

        /// <summary>
        /// Applies the matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>This</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public SwiftBitmap ApplyColorMatrix([NotNull] System.Drawing.Imaging.ColorMatrix matrix)
        {
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));
            if (InternalBitmap == null) throw new NullReferenceException($"Condition not met: [{nameof(InternalBitmap)} != null]");
            Unlock();
            using (Graphics NewGraphics = Graphics.FromImage(InternalBitmap))
            {
                using ImageAttributes Attributes = new();
                Attributes.SetColorMatrix(matrix);
                NewGraphics.DrawImage(InternalBitmap,
                    new System.Drawing.Rectangle(0, 0, Width, Height),
                    0, 0, Width, Height,
                    GraphicsUnit.Pixel,
                    Attributes);
            }
            return this;
        }

        /// <summary>
        /// Applies the matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>This</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public SwiftBitmap ApplyColorMatrix([NotNull] float[][] matrix)
        {
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));
            if (InternalBitmap == null) throw new NullReferenceException($"Condition not met: [{nameof(InternalBitmap)} != null]");
            return ApplyColorMatrix(new System.Drawing.Imaging.ColorMatrix(matrix));
        }

        /// <summary>
        /// Applies the convolution filter to the image
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="absolute">if set to <c>true</c> then the absolute value is used</param>
        /// <param name="offset">The offset to use for each pixel</param>
        /// <returns>Returns the image with the filter applied</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
        public SwiftBitmap ApplyConvolutionFilter(int[][] filter, bool absolute = false, int offset = 0)
        {
            if (InternalBitmap == null) throw new NullReferenceException($"Condition not met: [{nameof(InternalBitmap)} != null]");
            using SwiftBitmap Result = new(Width, Height);
            Lock();
            Result.Lock();
            Parallel.For(0, Width, x =>
            {
                for (int y = 0; y < Height; ++y)
                {
                    int RValue = 0;
                    int GValue = 0;
                    int BValue = 0;
                    int Weight = 0;
                    int XCurrent = -filter[0].Length / 2;
                    for (int x2 = 0; x2 < filter[0].Length; ++x2)
                    {
                        if (XCurrent + x < Width && XCurrent + x >= 0)
                        {
                            int YCurrent = -filter.Length / 2;
                            for (int y2 = 0; y2 < filter.Length; ++y2)
                            {
                                if (YCurrent + y < Height && YCurrent + y >= 0)
                                {
                                    var Pixel = GetPixel(XCurrent + x, YCurrent + y);
                                    RValue += filter[x2][y2] * Pixel.R;
                                    GValue += filter[x2][y2] * Pixel.G;
                                    BValue += filter[x2][y2] * Pixel.B;
                                    Weight += filter[x2][y2];
                                }
                                ++YCurrent;
                            }
                        }
                        ++XCurrent;
                    }
                    var MeanPixel = GetPixel(x, y);
                    if (Weight == 0)
                        Weight = 1;
                    if (Weight > 0)
                    {
                        if (absolute)
                        {
                            RValue = System.Math.Abs(RValue);
                            GValue = System.Math.Abs(GValue);
                            BValue = System.Math.Abs(BValue);
                        }
                        RValue = (RValue / Weight) + offset;
                        RValue = RValue.Clamp(255, 0);
                        GValue = (GValue / Weight) + offset;
                        GValue = GValue.Clamp(255, 0);
                        BValue = (BValue / Weight) + offset;
                        BValue = BValue.Clamp(255, 0);
                        MeanPixel = Color.FromArgb(RValue, GValue, BValue);
                    }
                    Result.SetPixel(x, y, MeanPixel);
                }
            });
            Unlock();
            Result.Unlock();
            return Copy(Result);
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public object Clone()
        {
            Unlock();
            return new SwiftBitmap((Bitmap)InternalBitmap.Clone());
        }

        /// <summary>
        /// Copies the image from one image to this one.
        /// </summary>
        /// <param name="SwiftBitmap">The SwiftBitmap to copy from.</param>
        /// <returns>This</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public unsafe SwiftBitmap Copy(SwiftBitmap SwiftBitmap)
        {
            if (InternalBitmap == null) throw new NullReferenceException($"Condition not met: [{nameof(InternalBitmap)} != null]");
            if (SwiftBitmap == null)
                return this;
            Unlock();
            InternalBitmap.Dispose();
            InternalBitmap = (Bitmap)SwiftBitmap.InternalBitmap.Clone();
            return this;
        }

        /// <summary>
        /// Crops the image by the specified width/height
        /// </summary>
        /// <param name="Width">The width.</param>
        /// <param name="Height">The height.</param>
        /// <param name="VAlignment">The v alignment.</param>
        /// <param name="HAlignment">The h alignment.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public SwiftBitmap Crop(int Width, int Height, Align VAlignment, Align HAlignment)
        {
            if (InternalBitmap == null) throw new NullReferenceException($"Condition not met: [{nameof(InternalBitmap)} != null]");
            Unlock();
            var TempRectangle = new System.Drawing.Rectangle { Height = Height, Width = Width, Y = VAlignment == Align.Top ? 0 : this.Height - Height };
            if (TempRectangle.Y < 0)
                TempRectangle.Y = 0;
            TempRectangle.X = HAlignment == Align.Left ? 0 : this.Width - Width;
            if (TempRectangle.X < 0)
                TempRectangle.X = 0;
            var TempHolder = InternalBitmap.Clone(TempRectangle, InternalBitmap.PixelFormat);
            InternalBitmap.Dispose();
            InternalBitmap = TempHolder;
            this.Width = Width;
            this.Height = Height;
            return this;
        }

        /// <summary>
        /// Draws the path specified
        /// </summary>
        /// <param name="pen">The pen to use.</param>
        /// <param name="path">The path to draw</param>
        /// <returns>This</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public SwiftBitmap DrawPath([NotNull] Pen pen, [NotNull] GraphicsPath path)
        {
            if (pen == null) throw new ArgumentNullException(nameof(pen));
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (InternalBitmap == null) throw new NullReferenceException($"Condition not met: [{nameof(InternalBitmap)} != null]");
            Unlock();
            using (Graphics NewGraphics = Graphics.FromImage(InternalBitmap))
            {
                NewGraphics.DrawPath(pen, path);
            }
            return this;
        }

        /// <summary>
        /// Draws the text specified
        /// </summary>
        /// <param name="TextToDraw">The text to draw.</param>
        /// <param name="FontToUse">The font to use.</param>
        /// <param name="BrushUsing">The brush to use.</param>
        /// <param name="BoxToDrawWithin">The box to draw within.</param>
        /// <returns>This</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public SwiftBitmap DrawText(string TextToDraw, [NotNull] Font FontToUse, [NotNull] Brush BrushUsing, RectangleF BoxToDrawWithin)
        {
            if (FontToUse == null) throw new ArgumentNullException(nameof(FontToUse));
            if (BrushUsing == null) throw new ArgumentNullException(nameof(BrushUsing));
            if (InternalBitmap == null) throw new NullReferenceException($"Condition not met: [{nameof(InternalBitmap)} != null]");
            Unlock();
            using (Graphics TempGraphics = Graphics.FromImage(InternalBitmap))
            {
                TempGraphics.DrawString(TextToDraw, FontToUse, BrushUsing, BoxToDrawWithin);
            }
            return this;
        }

        /// <summary>
        /// Fills the image with the specified color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>This</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
        public SwiftBitmap Fill(Color color)
        {
            if (Data == null) throw new NullReferenceException($"Condition not met: [{nameof(Data)} != null]");
            SetPixels(0, 0, (Width * Height).Times(x => color).ToArray());
            return this;
        }

        /// <summary>
        /// Gets the pixel.
        /// </summary>
        /// <param name="x">The x position</param>
        /// <param name="y">The y position</param>
        /// <returns>This</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public unsafe Color GetPixel(int x, int y)
        {
            if (Data == null) throw new NullReferenceException($"Condition not met: [{nameof(Data)} != null]");
            byte* TempPointer = DataPointer + (y * Data.Stride) + (x * PixelSize);
            return (PixelSize == 3) ?
                Color.FromArgb(TempPointer[2], TempPointer[1], TempPointer[0]) :
                Color.FromArgb(TempPointer[3], TempPointer[2], TempPointer[1], TempPointer[0]);
        }

        /// <summary>
        /// Gets the pixel.
        /// </summary>
        /// <param name="position">The position in the image</param>
        /// <returns>This</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
        public unsafe Color GetPixel(int position)
        {
            if (Data == null) throw new NullReferenceException($"Condition not met: [{nameof(Data)} != null]");
            byte* TempPointer = DataPointer + (position * PixelSize);
            return (PixelSize == 3) ?
                Color.FromArgb(TempPointer[2], TempPointer[1], TempPointer[0]) :
                Color.FromArgb(TempPointer[3], TempPointer[2], TempPointer[1], TempPointer[0]);
        }

        /// <summary>
        /// Locks this instance.
        /// </summary>
        /// <returns>This</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public unsafe SwiftBitmap Lock()
        {
            if (InternalBitmap == null) throw new NullReferenceException($"Condition not met: [{nameof(InternalBitmap)} != null]");
            if (Data != null)
                return this;
            Data = InternalBitmap.LockBits(new Rectangle(0, 0, InternalBitmap.Width, InternalBitmap.Height),
                                            ImageLockMode.ReadWrite,
                                            InternalBitmap.PixelFormat);
            PixelSize = GetPixelSize();
            DataPointer = (byte*)Data.Scan0;
            return this;
        }

        /// <summary>
        /// Resizes an SwiftBitmap to a certain height
        /// </summary>
        /// <param name="Width">New width for the final image</param>
        /// <param name="Height">New height for the final image</param>
        /// <param name="Quality">Quality of the resizing</param>
        /// <returns>This</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public SwiftBitmap Resize(int Width, int Height, Quality Quality = Quality.Low)
        {
            if (InternalBitmap == null) throw new NullReferenceException($"Condition not met: [{nameof(InternalBitmap)} != null]");
            Unlock();
            var TempBitmap = new Bitmap(Width, Height);
            using (Graphics NewGraphics = Graphics.FromImage(TempBitmap))
            {
                if (Quality == Quality.High)
                {
                    NewGraphics.CompositingQuality = CompositingQuality.HighQuality;
                    NewGraphics.SmoothingMode = SmoothingMode.HighQuality;
                    NewGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                }
                else
                {
                    NewGraphics.CompositingQuality = CompositingQuality.HighSpeed;
                    NewGraphics.SmoothingMode = SmoothingMode.HighSpeed;
                    NewGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                }
                NewGraphics.DrawImage(InternalBitmap, new System.Drawing.Rectangle(0, 0, Width, Height));
            }
            InternalBitmap.Dispose();
            InternalBitmap = TempBitmap;
            this.Width = Width;
            this.Height = Height;
            return this;
        }

        /// <summary>
        /// Rotates and/or flips the image
        /// </summary>
        /// <param name="flipType">Type of flip/rotation to do</param>
        /// <returns>This</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public SwiftBitmap Rotate(RotateFlipType flipType)
        {
            if (InternalBitmap == null) throw new NullReferenceException($"Condition not met: [{nameof(InternalBitmap)} != null]");
            Unlock();
            InternalBitmap.RotateFlip(flipType);
            return this;
        }

        /// <summary>
        /// Rotates an image
        /// </summary>
        /// <param name="DegreesToRotate">Degrees to rotate the image</param>
        /// <returns>This</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public SwiftBitmap Rotate(float DegreesToRotate)
        {
            if (InternalBitmap == null) throw new NullReferenceException($"Condition not met: [{nameof(InternalBitmap)} != null]");
            Unlock();
            var TempBitmap = new Bitmap(Width, Height);
            using (Graphics NewGraphics = Graphics.FromImage(TempBitmap))
            {
                NewGraphics.TranslateTransform((float)Width / 2.0f, (float)Height / 2.0f);
                NewGraphics.RotateTransform(DegreesToRotate);
                NewGraphics.TranslateTransform(-(float)Width / 2.0f, -(float)Height / 2.0f);
                NewGraphics.DrawImage(InternalBitmap,
                    new System.Drawing.Rectangle(0, 0, Width, Height),
                    new System.Drawing.Rectangle(0, 0, Width, Height),
                    GraphicsUnit.Pixel);
            }
            InternalBitmap.Dispose();
            InternalBitmap = TempBitmap;
            return this;
        }

        /// <summary>
        /// Saves to the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>This</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public SwiftBitmap Save([NotNull] string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException(nameof(fileName));
            if (InternalBitmap == null) throw new NullReferenceException($"Condition not met: [{nameof(InternalBitmap)} != null]");
            Unlock();
            InternalBitmap.Save(fileName, GetImageFormat(fileName));
            return this;
        }

        /// <summary>
        /// Sets the pixel.
        /// </summary>
        /// <param name="x">The x position</param>
        /// <param name="y">The y position</param>
        /// <param name="pixelColor">Color of the pixel.</param>
        /// <returns>This</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public unsafe SwiftBitmap SetPixel(int x, int y, Color pixelColor)
        {
            if (Data == null) throw new NullReferenceException($"Condition not met: [{nameof(Data)} != null]");
            byte* TempPointer = DataPointer + (y * Data.Stride) + (x * PixelSize);
            if (PixelSize == 3)
            {
                TempPointer[2] = pixelColor.R;
                TempPointer[1] = pixelColor.G;
                TempPointer[0] = pixelColor.B;
                return this;
            }
            TempPointer[3] = pixelColor.A;
            TempPointer[2] = pixelColor.R;
            TempPointer[1] = pixelColor.G;
            TempPointer[0] = pixelColor.B;
            return this;
        }

        /// <summary>
        /// Sets the pixels starting at the x and y coordinate specified.
        /// </summary>
        /// <param name="x">The beginning x coordinate</param>
        /// <param name="y">The beginning y coordinate</param>
        /// <param name="pixels">The pixels to set</param>
        /// <returns>This</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public unsafe SwiftBitmap SetPixels(int x, int y, Color[] pixels)
        {
            if (Data == null) throw new NullReferenceException($"Condition not met: [{nameof(Data)} != null]");
            if (pixels == null)
                return this;
            byte* TempPointer = DataPointer + (y * Data.Stride) + (x * PixelSize);
            for (int z = 0; z < pixels.Length; ++z)
            {
                if (PixelSize == 3)
                {
                    TempPointer[2] = pixels[z].R;
                    TempPointer[1] = pixels[z].G;
                    TempPointer[0] = pixels[z].B;
                }
                else
                {
                    TempPointer[3] = pixels[z].A;
                    TempPointer[2] = pixels[z].R;
                    TempPointer[1] = pixels[z].G;
                    TempPointer[0] = pixels[z].B;
                }
                TempPointer += PixelSize;
            }
            return this;
        }

        /// <summary>
        /// Converts an SwiftBitmap to a base64 string and returns it
        /// </summary>
        /// <param name="desiredFormat">Desired SwiftBitmap format (defaults to Jpeg)</param>
        /// <returns>The SwiftBitmap in base64 string format</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0090:Use 'new(...)'", Justification = "<Pending>")]
        public string ToString(ImageFormat desiredFormat)
        {
            if (InternalBitmap == null) throw new NullReferenceException($"Condition not met: [{nameof(InternalBitmap)} != null]");
            desiredFormat = desiredFormat.Check(ImageFormat.Jpeg);
            using MemoryStream Stream = new MemoryStream();
            InternalBitmap.Save(Stream, desiredFormat);
            return Stream.ToArray().ToString(Base64FormattingOptions.None);
        }

        /// <summary>
        /// Unlocks this SwiftBitmap
        /// </summary>
        /// <returns>This</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public unsafe SwiftBitmap Unlock()
        {
            if (InternalBitmap == null) throw new NullReferenceException($"Condition not met: [{nameof(InternalBitmap)} != null]");
            if (Data == null)
                return this;
            InternalBitmap.UnlockBits(Data);
            Data = null;
            DataPointer = null;
            return this;
        }

        /// <summary>
        /// Function to override in order to dispose objects
        /// </summary>
        /// <param name="Managed">
        /// If true, managed and unmanaged objects should be disposed. Otherwise unmanaged objects only.
        /// </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        protected override void Dispose(bool Managed)
        {
            if (Data != null)
            {
                Unlock();
            }
            if (InternalBitmap != null)
            {
                InternalBitmap.Dispose();
                InternalBitmap = null;
            }
        }

        /// <summary>
        /// Returns the SwiftBitmap format this file is using
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The image format</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        private static ImageFormat GetImageFormat(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return ImageFormat.Bmp;
            if (fileName.EndsWith("jpg", StringComparison.InvariantCultureIgnoreCase) || fileName.EndsWith("jpeg", StringComparison.InvariantCultureIgnoreCase))
                return ImageFormat.Jpeg;
            if (fileName.EndsWith("png", StringComparison.InvariantCultureIgnoreCase))
                return ImageFormat.Png;
            if (fileName.EndsWith("tiff", StringComparison.InvariantCultureIgnoreCase))
                return ImageFormat.Tiff;
            if (fileName.EndsWith("ico", StringComparison.InvariantCultureIgnoreCase))
                return ImageFormat.Icon;
            if (fileName.EndsWith("gif", StringComparison.InvariantCultureIgnoreCase))
                return ImageFormat.Gif;
            return ImageFormat.Bmp;
        }

        /// <summary>
        /// Gets the size of the pixel.
        /// </summary>
        /// <returns>The size of the pixel in bytes</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        private int GetPixelSize()
        {
            if (Data == null) throw new NullReferenceException($"Condition not met: [{nameof(Data)} != null]");
            if (Data.PixelFormat == PixelFormat.Format24bppRgb)
                return 3;
            else if (Data.PixelFormat == PixelFormat.Format32bppArgb
                || Data.PixelFormat == PixelFormat.Format32bppPArgb
                || Data.PixelFormat == PixelFormat.Format32bppRgb)
                return 4;
            return 0;
        }
    }
}