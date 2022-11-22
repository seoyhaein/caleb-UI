using Avalonia.Controls.Shapes;
using Avalonia;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace caleb_UI
{
    // https://docs.avaloniaui.net/misc/wpf/uielement-frameworkelement-and-control
    // http://reference.avaloniaui.net/api/Avalonia/Vector/
    // http://reference.avaloniaui.net/api/Avalonia.Media/StreamGeometry/

    // TODO
    // https://stackoverflow.com/questions/66386895/avaloniaui-capture-mouse-button-up-down-globally
    // mouse button down and up event 
    // shape 에서 특정 컨트롤에 해당 이벤트를 넣고 이것을 처리하는 방식을 간단히 테스트 해본다.

    /// <summary>
    /// Specifies the offset type that can be applied to a <see cref="BaseConnection"/> using the <see cref="BaseConnection.SourceOffset"/> and the <see cref="BaseConnection.TargetOffset"/> values.
    /// </summary>
    public enum ConnectionOffsetMode
    {
        /// <summary>
        /// No offset applied.
        /// </summary>
        None,

        /// <summary>
        /// The offset is applied in a circle around the point.
        /// </summary>
        Circle,

        /// <summary>
        /// The offset is applied in a rectangle shape around the point.
        /// </summary>
        Rectangle,

        /// <summary>
        /// The offset is applied in a rectangle shape around the point, perpendicular to the edges.
        /// </summary>
        Edge,
    }


    public abstract class BaseConnection : Shape
    {
        /// <summary>
        /// Gets or sets the start point of this connection.
        /// </summary>
        public Point Source
        {
            get => (Point)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        /// <summary>
        /// Gets or sets the end point of this connection.
        /// </summary>
        public Point Target
        {
            get => (Point)GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }


        /// <summary>
        /// Gets or sets the <see cref="ConnectionOffsetMode"/> to apply when drawing the connection.
        /// </summary>
        public ConnectionOffsetMode OffsetMode
        {
            get => (ConnectionOffsetMode)GetValue(OffsetModeProperty);
            set => SetValue(OffsetModeProperty, value);
        }

        /// <summary>
        /// Gets a vector that has its coordinates set to 0.
        /// </summary>
        protected static readonly Vector ZeroVector = new Vector(0d, 0d);

        // TODO 상세하게 살펴보자.
        private readonly StreamGeometry _geometry = new StreamGeometry
        {
            //FillRule = FillRule.EvenOdd
        };

        // geometry 설정하기.

        protected override Geometry CreateDefiningGeometry()
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

        /// <summary>
        /// Gets the resulting offset after applying the <see cref="OffsetMode"/>.
        /// </summary>
        /// <returns></returns>
        protected virtual (Vector SourceOffset, Vector TargetOffset) GetOffset()
        {
            Vector delta = Target - Source;
            Vector delta2 = Source - Target;

            return OffsetMode switch
            {
                ConnectionOffsetMode.Rectangle => (GetRectangleModeOffset(delta, SourceOffset), GetRectangleModeOffset(delta2, TargetOffset)),
                ConnectionOffsetMode.Circle => (GetCircleModeOffset(delta, SourceOffset), GetCircleModeOffset(delta2, TargetOffset)),
                ConnectionOffsetMode.Edge => (GetEdgeModeOffset(delta, SourceOffset), GetEdgeModeOffset(delta2, TargetOffset)),
                ConnectionOffsetMode.None => (ZeroVector, ZeroVector),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
