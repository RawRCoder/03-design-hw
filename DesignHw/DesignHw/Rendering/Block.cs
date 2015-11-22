using System.Drawing;

namespace DesignHw.Rendering
{
    public class Block<T>
    {
        public T Data { get; set; }
        public PointF LeftTop { get; set; }
        public SizeF Size { get; set; }
        public PointF RightBottom
        {
            set { LeftTop = value + new SizeF(1, 1) - Size; }
            get { return LeftTop + Size - new SizeF(1, 1); }
        }

        public Block(PointF pos, SizeF size)
        {
            LeftTop = pos;
            Size = size;
        }
        public Block(PointF pos, int width, int height)
            : this(pos, new SizeF(width, height)) { }
        public Block(SizeF size)
            : this(Point.Empty, size)
        { }
        public Block(int width, int height)
            : this(new SizeF(width, height))
        { }

        public bool IsInside(Block<T> container)
            => container.LeftTop.X <= LeftTop.X && 
            container.LeftTop.Y <= LeftTop.Y && 
            container.RightBottom.X >= RightBottom.X && 
            container.RightBottom.Y >= RightBottom.Y;

        public bool Contains(Block<T> other)
            => other.IsInside(this);
    }
}
