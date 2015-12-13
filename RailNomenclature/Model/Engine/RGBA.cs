using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;

namespace RailNomenclature
{
    [Serializable]
    public class RGBA : IDeserializationCallback
    {
        public static readonly RGBA Transparent = new RGBA(0, 0);
        public static readonly RGBA White = new RGBA(255, 255);
        public static readonly RGBA Black = new RGBA(0, 255);
        public static readonly RGBA DarkRed = new RGBA(139, 0, 0, 255);
        public static readonly RGBA BrightOrange = new RGBA(255, 160, 60, 255);
        public static readonly RGBA Green = new RGBA(0, 240, 60);
        public static readonly RGBA Red = Color.Red;
        public static readonly RGBA Orange = Color.Orange;
        public static readonly RGBA Gray = Color.Gray;

        private int _r, _g, _b, _alpha;

        [NonSerialized]
        private Color _color;

        public RGBA(int r, int g, int b, int a = 255)
        {
            _r = r;
            _g = g;
            _b = b;
            _alpha = a;
            _color = new Color(r, g, b, a);
        }

        public RGBA(int rgb, int a = 255)
        {
            _r = _g = _b = rgb;
            _alpha = a;
            _color = new Color(rgb, rgb, rgb, a);
        }

        public RGBA(RGBA c)
        {
            _r = c.R;
            _g = c.G;
            _b = c.B;
            _alpha = c.Alpha;
            _color = new Color(_r, _g, _b, _alpha);
        }

        public RGBA(Color c)
        {
            _r = c.R;
            _g = c.G;
            _b = c.B;
            _alpha = c.A;
            _color = c;
        }

        public Color Color { get { return _color; } }

        public int R { get { return _r; } }
        public int G { get { return _g; } }
        public int B { get { return _b; } }

        public int Alpha
        {
            get { return _alpha; }

            set
            {
                _alpha = value;
                _color = new Color(_r, _g, _b, _alpha);
            }
        }

        public static bool operator ==(RGBA c1, Color c2)
        {
            return (c1._color.R == c2.R && c1._color.G == c2.G && c1._color.B == c2.B && c1._color.A == c2.A);
        }

        public static bool operator !=(RGBA c1, Color c2)
        {
            return !(c1 == c2);
        }

        public static bool operator ==(RGBA c1, RGBA c2)
        {
            return (Object.ReferenceEquals(c1, c2) && c1.R == c2.R && c1.G == c2.G && c1.B == c2.B && c1.Alpha == c2.Alpha);
        }

        public static bool operator !=(RGBA c1, RGBA c2)
        {
            return !(c1 == c2);
        }

        public override bool Equals(object obj)
        {
            try
            {
                return (this == (RGBA)obj);
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return _alpha * 16777216 + _r * 65536 + _g * 256 + _b;
        }

        public static implicit operator RGBA(Color c)
        {
            return new RGBA(c);
        }

        void IDeserializationCallback.OnDeserialization(Object sender)
        {
            _color = new Color(_r, _g, _b, _alpha);
        }

        public int Brightness()
        {
            return (int)Math.Sqrt(
                R * R * 0.241f +
                G * G * 0.691f +
                B * B * 0.068f
            );
        }
    }
}
