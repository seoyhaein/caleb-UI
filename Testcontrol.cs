using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarfBuzzSharp;

namespace CalebUI
{
    // https://eqsrainy.tistory.com/16

    /// <summary>
    /// 커스텀 도형
    /// </summary>
    public class CustomShape : Shape
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////// Property
        ////////////////////////////////////////////////////////////////////////////////////////// Protected

        /// <summary>
        /// 테스트 변수들
        /// </summary>
        public Point Target;
        public Point Source;
        public Size SourceOffset;
        public Size TargetOffset;
        public Size ArrowSize;
        public ArrowHeadEnds ArrowEnds;
        public ConnectionDirection Direction;
        public double Spacing;

        // ReSharper disable once InconsistentNaming
        private const double _baseOffset = 100d;
        // ReSharper disable once InconsistentNaming
        private const double _offsetGrowthRate = 25d;

        // 주석 처리 하니까 에러 없음. 흠.
        /* public CustomShape(Point s, Point t, Size so, Size to) 
        {
            Source = s;
            Target = t;
            SourceOffset = so;
            TargetOffset = to;
        }*/

 

        private readonly StreamGeometry _geometry = new StreamGeometry();

        #region 정의 지오메트리 - DefiningGeometry

        protected override Geometry? CreateDefiningGeometry()
        {
            return GetGeometry();
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Method
        ////////////////////////////////////////////////////////////////////////////////////////// Private

        #region 지오메트리 구하기 - GetGeometry()
        

        /// <summary>
        /// 지오메트리 구하기
        /// </summary>
        /// <returns>지오메트리</returns>
        private Geometry GetGeometry()
        {
            StreamGeometry streamGeometry = new StreamGeometry();

            using (StreamGeometryContext streamGeometryContext = streamGeometry.Open())
            {

                DrawArrowGeometry(streamGeometryContext, new Point(50.0, 50.0), new Point(75.0, 75.0), ConnectionDirection.Backward);
                
/*                streamGeometryContext.BeginFigure
                (
                    new Point(50.0, 50.0),
                    false
                );

                streamGeometryContext.ArcTo
                (
                    new Point(75.0, 75.0),
                    new Size(10.0, 20.0),
                    0.0,
                    false,
                    SweepDirection.Clockwise
                );

                streamGeometryContext.ArcTo
                (
                    new Point(100.0, 100.0),
                    new Size(10.0, 20.0),
                    0.0,
                    false,
                    SweepDirection.Clockwise
                );*/
            }

            return streamGeometry;
        }


        private Geometry GetGeometry1 ()
        {
            using (StreamGeometryContext context = _geometry.Open())
            {
                (Vector sourceOffset, Vector targetOffset) = GetOffset();
                Point source = Source + sourceOffset;
                Point target = Target + targetOffset;

                (Point arrowSource, Point arrowTarget) = DrawLineGeometry(context, source, target);

                if (ArrowSize.Width != 0d && ArrowSize.Height != 0d)
                {
                    switch (ArrowEnds)
                    {
                        case ArrowHeadEnds.Start:
                            DrawArrowGeometry(context, arrowTarget, arrowSource, ConnectionDirection.Backward);
                            break;
                        case ArrowHeadEnds.End:
                            DrawArrowGeometry(context, arrowSource, arrowTarget, ConnectionDirection.Forward);
                            break;
                        case ArrowHeadEnds.Both:
                            DrawArrowGeometry(context, arrowSource, arrowTarget, ConnectionDirection.Forward);
                            DrawArrowGeometry(context, arrowTarget, arrowSource, ConnectionDirection.Backward);
                            break;
                        case ArrowHeadEnds.None:
                            break;
                        default:
                            break;
                    }
                }
            }

            return _geometry;
        }

        protected virtual (Vector, Vector) GetOffset()
        {
            Vector delta = Target - Source;
            Vector delta2 = Source - Target;

           /* return OffsetMode switch
            {
                ConnectionOffsetMode.Rectangle => (GetRectangleModeOffset(delta, SourceOffset), GetRectangleModeOffset(delta2, TargetOffset)),
                ConnectionOffsetMode.Circle => (GetCircleModeOffset(delta, SourceOffset), GetCircleModeOffset(delta2, TargetOffset)),
                ConnectionOffsetMode.Edge => (GetEdgeModeOffset(delta, SourceOffset), GetEdgeModeOffset(delta2, TargetOffset)),
                ConnectionOffsetMode.None => (ZeroVector, ZeroVector),
                _ => throw new ArgumentOutOfRangeException()
            };*/

            return (GetRectangleModeOffset(delta, SourceOffset), GetRectangleModeOffset(delta2, TargetOffset));
        }

        // 디버깅해서 값을 가져오자.
        private static Vector GetRectangleModeOffset(Vector delta, Size offset)
        {
            if (delta.SquaredLength > 0d)
            {
                delta.Normalize();
            }

            double angle = Math.Atan2(delta.Y, delta.X);

            // 수정 구문 살펴보자.
            // 참고
            // https://sourcegraph.com/github.com/mameolan/Avalonia.ExtendedToolkit/-/blob/Avalonia.Controlz/Controls/TabPanel.cs?L166:20&subtree=true#tab=references
            var result = new Vector();

            if (offset.Width * 2d * Math.Abs(delta.Y) < offset.Height * 2d * Math.Abs(delta.X))
            {
                double X = Math.Sign(delta.X) * offset.Width;
                result = new Vector(X, Math.Tan(angle) * X);

            }
            else
            {
                double Y = Math.Sign(delta.Y) * offset.Height;
                double X = 1.0d / Math.Tan(angle) * Y;

                result = new Vector(X, Y);

            }

            return result;
        }

        private static Vector GetCircleModeOffset(Vector delta, Size offset)
        {
            if (delta.SquaredLength > 0d)
            {
                delta.Normalize();
            }

            return new Vector(delta.X * offset.Width, delta.Y * offset.Height);
        }

        private static Vector GetEdgeModeOffset(Vector delta, Size offset)
        {
            double xOffset = Math.Min(Math.Abs(delta.X) / 2d, offset.Width) * Math.Sign(delta.X);
            double yOffset = Math.Min(Math.Abs(delta.Y) / 2d, offset.Height) * Math.Sign(delta.Y);

            return new Vector(xOffset, yOffset);
        }

        protected (Point ArrowSource, Point ArrowTarget) DrawLineGeometry(StreamGeometryContext context, Point source, Point target)
        {
            double direction = Direction == ConnectionDirection.Forward ? 1d : -1d;
            var spacing = new Vector(Spacing * direction, 0d);
            var arrowOffset = new Vector(ArrowSize.Width * direction, 0d);
            Point endPoint = Spacing > 0 ? target - arrowOffset : target;
            Point startPoint = source + spacing;

            Vector delta = target - source;
            double height = Math.Abs(delta.Y);
            double width = Math.Abs(delta.X);

            // Smooth curve when distance is lower than base offset
            double smooth = Math.Min(_baseOffset, height);
            // Calculate offset based on distance
            double offset = Math.Max(smooth, width / 2d);
            // Grow slowly with distance
            offset = Math.Min(_baseOffset + Math.Sqrt(width * _offsetGrowthRate), offset);

            var controlPoint = new Vector(offset * direction, 0d);

            // TODO 세밀한 부분은 찾아봐야 한다.
            context.BeginFigure(source, false);
            context.LineTo(startPoint);
            // TODO QuadraticBezierTo(Point, Point) 랑 비교해보자.
            context.CubicBezierTo(startPoint + controlPoint, endPoint - controlPoint, endPoint);
            context.LineTo(endPoint);

            return (source, target);
        }
        // https://learn.microsoft.com/ko-kr/dotnet/desktop/wpf/graphics-multimedia/how-to-create-a-shape-using-a-streamgeometry?view=netframeworkdesktop-4.8
        protected virtual void DrawArrowGeometry(StreamGeometryContext context, Point source, Point target, ConnectionDirection arrowDirection = ConnectionDirection.Forward)
        {
            (Point from, Point to) = GetArrowHeadPoints(source, target, arrowDirection);


            // avalonia 에서 찾아보자. 위에랑 비교하고 nodify 랑 비교해야함.
            // WPF 에서도 테스트 해보자.

            // Begin the triangle at the point specified. Notice that the shape is set to
            // be closed so only two lines need to be specified below to make the triangle.
            context.BeginFigure(new Point(10, 100), false /* is filled */);

            // Draw a line to the next specified point.
            context.LineTo(new Point(100, 100));

            // Draw another line to the next specified point.
            context.LineTo(new Point(100, 50));

            //context.BeginFigure(new Point(50.0, 50.0), true);
            //context.LineTo(new Point(100, 50));
            //context.LineTo(new Point(100, 70));

            /*context.BeginFigure(target, true);
            context.LineTo(from);
            context.LineTo(to);*/
        }

        protected virtual (Point From, Point To) GetArrowHeadPoints(Point source, Point target, ConnectionDirection arrowDirection)
        {
            double headWidth = ArrowSize.Width;
            double headHeight = ArrowSize.Height;

            double direction = arrowDirection == ConnectionDirection.Forward ? 1d : -1d;
            var from = new Point(target.X - headWidth * direction, target.Y + headHeight);
            var to = new Point(target.X - headWidth * direction, target.Y - headHeight);
            return (from, to);
        }

        #endregion
    }
}
